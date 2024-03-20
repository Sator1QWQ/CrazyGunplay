﻿using System.Collections;
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
    public class BattleData
    {
        private int heroId;
        public int HeroId { get => heroId; set { heroId = value; SyncToLua(); } }

        private int weaponId;
        public int WeaponId { get => weaponId; set { weaponId = value; SyncToLua(); } }

        private int life;   //生命数
        public int Life { get => life; set { life = value; SyncToLua(); } }

        public void SyncToLua()
        {
            Module.Lua.Env.Global.Get<LuaTable>("MPlayer").Get<LuaTable>("Instance").Get<LuaFunction>("SyncData").Call(this);
        }
    }

    /// <summary>
    /// 玩家id
    /// </summary>
    public int PlayerId { get; private set; }
    public BattleData Data { get; private set; }
    public Transform WeaponRoot { get; private set; }

    /// <summary>
    /// 玩家看的方向
    /// </summary>
    public Vector3 LookDirection { get; set; }
	public PlayerController Controller { get; private set; }
    public StateMachine<PlayerEntity> Machine { get; private set; }
    private SimpleGravity mGravity;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        Data = new BattleData();
        PlayerId = ((int[])userData)[0];
        Data.HeroId = ((int[])userData)[1];
        WeaponRoot = transform.Find("WeaponRoot");
        InitController();
        InitStateMachine();
        InitWeapon();
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

    public void LifeChange(int change)
    {
        Data.Life += change;
    }
}
