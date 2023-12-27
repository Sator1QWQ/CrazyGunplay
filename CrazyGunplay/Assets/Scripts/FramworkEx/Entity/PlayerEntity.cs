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

	private PlayerController mController;
    private SimpleGravity mGravity;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        PlayerId = ((int[])userData)[0];
        HeroId = ((int[])userData)[1];
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
        childEntity.transform.SetParent(parentTransform, false);
    }

    //初始化控制器
    private void InitController()
    {
        mGravity = GetComponent<SimpleGravity>();
        mController = new OnePController(this, mGravity);
        LuaTable data = Config.Get("CharacterData", HeroId);
        //Module.Lua.Env.DoString("require 'Configs.Config.CharacterData'");
        //LuaTable config = Module.Lua.Env.Global.Get<LuaTable>("CharacterData");
        //LuaTable data;
        //config.Get(HeroId, out data);

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
        //if((int)userData != WeaponId)
        //{
        //    return;
        //}
        //ShowEntitySuccessEventArgs showEvent = e as ShowEntitySuccessEventArgs;
        //Module.Entity.AttachEntity(showEvent.Entity, Entity);
        //Module.Event.Unsubscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntity);
    }
}
