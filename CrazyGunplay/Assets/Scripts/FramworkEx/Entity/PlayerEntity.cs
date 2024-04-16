﻿using System.Collections;
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
    public Animator Anim { get; private set; }

    private bool isPauseControl;   //是否暂停控制
    private SimpleGravity mGravity;
    private Vector3 initPos = Vector3.up * 10;
    private Weapon mWeapon;

    //userData为playerid
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        LuaTable table = (LuaTable)userData;
        PlayerId = table.Get<int>("playerId");
        Data = Module.PlayerData.GetData(PlayerId);
        WeaponRoot = transform.Find("WeaponRoot");
        Entity.transform.forward = Vector3.right;
        Anim = GetComponent<Animator>();
        InitController();
        InitStateMachine();
        InitWeapon();
        Entity.transform.position = initPos;
        if(PlayerId == 2)
        {
            Entity.transform.position = Vector3.up * 10 + Vector3.right * 10;
        }
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        Module.Event.Subscribe(ChangeStateEventArgs<PlayerEntity>.EventId, OnChangeState);
        Module.Event.Subscribe(BattleStateChangeArgs.EventId, OnBattleStateChange);
        mWeapon = Module.Weapon.GetWeapon(PlayerId);    //武器可能会变化
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
        Module.Event.Unsubscribe(ChangeStateEventArgs<PlayerEntity>.EventId, OnChangeState);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        if(isPauseControl)
        {
            return;
        }
        Machine.OnUpdate();
        Controller.OnUpdate();
        mWeapon.OnUpdate();
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
        Module.Weapon.ShowWeapon("Weapon", Data.PlayerId, Data.WeaponId);
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

        WeaponEntity entity = showEvent.Entity.Logic as WeaponEntity;
        Weapon weapon = showEvent.UserData as Weapon;
        int playerId = weapon.PlayerId;
        if(playerId != Data.PlayerId)
        {
            return;
        }
        Module.Entity.AttachEntity(entity.Entity, Entity);
        entity.SetPlayerEntity(this);
        entity.Entity.transform.SetParent(WeaponRoot, false);
        entity.Entity.transform.localPosition = Vector3.zero;
        Module.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntity);
    }

    /// <summary>
    /// 受伤
    /// </summary>
    /// <param name="type"></param>
    /// <param name="beatBackValue">此次攻击产生的击退值</param>
    /// <param name="vector">受击方向</param>
    public void GetDamage(GetHitType type, Vector3 vector)
    {
        switch(type)
        {
            case GetHitType.BeatBack:
                BeatBack(vector);
                break;
            case GetHitType.HitToFly:
                BeatBack(vector);
                break;
        }
    }

    private void BeatBack(Vector3 vector)
    {
        Entity.transform.position = new Vector3(Entity.transform.position.x, Entity.transform.position.y, 0);   //重置坐标

        mGravity.AddForce("Force", vector * Data.BeatBackValue, 0.3f);
        Debug.Log("击退值为==" + Data.BeatBackValue + ", 击退百分比为：" + Data.BeatBackPercent);
    }

    //控制动画
    private void OnChangeState(object sender, GameEventArgs e)
    {
        ChangeStateEventArgs<PlayerEntity> args = e as ChangeStateEventArgs<PlayerEntity>;
        if(!args.Owner.Equals(this))
        {
            return;
        }

        switch(args.Layer)
        {
            case StateLayer.Control:
                Anim.SetInteger("controlState", (int)args.CurState);
                break;
        }
    }

    private void OnBattleStateChange(object sender, GameEventArgs e)
    {
        BattleStateChangeArgs args = e as BattleStateChangeArgs;
        switch(args.State)
        {
            //结束时，所有玩家不可移动
            case BattleState.TeamDead:
                Controller.IsPause = true;
                Machine.IsPause = true;
                break;
            case BattleState.Timeout:
                Controller.IsPause = true;
                Machine.IsPause = true;
                break;
        }
    }

    //hide时会调用这个函数，重置数据
    protected override void OnRecycle()
    {
        base.OnRecycle();
        isPauseControl = false;
    }
}
