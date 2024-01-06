using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using XLua;

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
    public int HeroId { get; private set; }
    public int WeaponId { get; private set; }
    public Transform WeaponRoot { get; private set; }

    /// <summary>
    /// 玩家看的方向
    /// </summary>
    public Vector3 LookDirection { get; set; }
	private PlayerController mController;
    private SimpleGravity mGravity;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        PlayerId = ((int[])userData)[0];
        HeroId = ((int[])userData)[1];
        WeaponRoot = transform.Find("WeaponRoot");
        InitController();
        InitWeapon();
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        mController.OnUpdate();
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
            mController = new OnePController(this, mGravity);
        }
        else
        {
            mController = new TwoPController(this, mGravity);
        }
        
        LuaTable data = Config.Get("CharacterData", HeroId);

        float speed = data.Get<float>("speed");
        float jumpSpeed = data.Get<float>("jump");
        mController.AddControlAction(new MoveController(speed));
        mController.AddControlAction(new JumpController(jumpSpeed));
        mController.AddControlAction(new DushController());
        mController.AddControlAction(new NormalAttackController());
    }

    //初始化玩家武器
    private void InitWeapon()
    {
        Module.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntity);
        LuaTable data = Config.Get("Character", HeroId);
        WeaponId = data.Get<int>("initWeapon");
        Module.Weapon.ShowWeapon("Weapon", WeaponId);
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
        if(weaponId != WeaponId)
        {
            return;
        }
        Module.Entity.AttachEntity(showEvent.Entity, Entity);
        (showEvent.Entity.Logic as WeaponEntity).SetPlayerEntity(this);
        Module.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntity);
    }
}
