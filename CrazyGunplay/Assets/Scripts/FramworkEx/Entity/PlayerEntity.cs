using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using XLua;
using GameFramework.Event;

/*
* 作者：
* 日期：
* 描述：
	玩家实体		
*/
public class PlayerEntity : CharacterEntity
{
    /// <summary>
    /// 玩家id
    /// </summary>
    public int PlayerId { get; private set; }
    public PlayerBattleData Data { get; private set; }
    public Transform WeaponRoot { get; private set; }

    /// <summary>
    /// 玩家看的方向
    /// </summary>
    public Vector3 LookDirection { get; set; }
	public PlayerController Controller { get; private set; }
    public StateMachine<PlayerEntity> Machine { get; private set; }

    private SimpleGravity mGravity;
    private Vector3 initPos = Vector3.up * 11;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        PlayerId = ((int[])userData)[0];
        Data = Module.PlayerData.GetData(PlayerId);
        WeaponRoot = transform.Find("WeaponRoot");
        InitController();
        InitStateMachine();
        InitWeapon();
        Entity.transform.position = initPos;
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        Controller.OnUpdate();
        Machine.OnUpdate();
    }

    protected override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
    {
        base.OnAttached(childEntity, parentTransform, userData);
        childEntity.transform.SetParent(WeaponRoot, false);
    }

    //初始化控制器
    private void InitController()
    {
        mGravity = GetComponent<SimpleGravity>();
        if(PlayerId == 1)
        {
            Controller = new OnePController(this, mGravity);
        }
        else
        {
            Controller = new TwoPController(this, mGravity);
        }
        
        LuaTable data = Config.Get("CharacterData", Data.HeroId);

        float speed = data.Get<float>("speed");
        float jumpSpeed = data.Get<float>("jump");
        Controller.AddControlAction(new MoveController(speed));
        Controller.AddControlAction(new JumpController(jumpSpeed));
        Controller.AddControlAction(new DushController());
        Controller.AddControlAction(new NormalAttackController());
    }

    //初始化状态机
    private void InitStateMachine()
    {
        //可改为配置表
        Machine = new StateMachine<PlayerEntity>(this);
        Machine.AddState(StateLayer.Control, new ControlIdleState());
        Machine.AddState(StateLayer.Control, new MoveState());
        Machine.AddState(StateLayer.Control, new JumpState());
        Machine.AddState(StateLayer.Control, new DushState());

        Machine.AddState(StateLayer.Passive, new PassiveIdleState());
        Machine.AddState(StateLayer.Passive, new DieState());
        Machine.AddState(StateLayer.Passive, new RespawnState());
    }

    //初始化玩家武器
    private void InitWeapon()
    {
        Module.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntity);
        LuaTable data = Config.Get("Character", Data.HeroId);
        Data.WeaponId = data.Get<int>("initWeapon");
        Module.Weapon.ShowWeapon("Weapon", Data.WeaponId);
    }

    private void OnShowEntity(object userData, GameFrameworkEventArgs e)
    {
        ShowEntitySuccessEventArgs showEvent = e as ShowEntitySuccessEventArgs;
        if(!showEvent.EntityLogicType.Equals(typeof(WeaponEntity)))
        {
            return;
        }
        if(!(showEvent.UserData is Weapon))
        {
            return;
        }

        int weaponId = (showEvent.UserData as Weapon).Id;
        if(weaponId != Data.WeaponId)
        {
            return;
        }
        Module.Entity.AttachEntity(showEvent.Entity, Entity);
        showEvent.Entity.transform.localPosition = Vector3.zero;
        (showEvent.Entity.Logic as WeaponEntity).SetPlayerEntity(this);
        Module.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntity);
    }

    public void BeatBack(Vector3 vector)
    {
        //mGravity.AddVelocity("BeatBack", vector, 0.1f, true);
        mGravity.AddForce("Force",  Vector3.right * 5);
        //mGravity.AddVelocity("BeatBack", Vector3.up*10, 0.5f, true);
        //mGravity.Jump((Vector3.up)*10);
    }
}
