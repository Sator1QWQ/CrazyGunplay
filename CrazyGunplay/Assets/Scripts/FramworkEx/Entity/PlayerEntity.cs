using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;
using XLua;
using GameFramework.Event;
using System;

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
    public BuffData BuffData { get; private set; }

    public Transform WeaponRoot { get; private set; }

    /// <summary>
    /// 玩家看的方向
    /// </summary>
    public Vector3 LookDirection { get; set; }
	public PlayerController Controller { get; private set; }
    public StateMachine<PlayerEntity> Machine { get; private set; }
    public Animator Anim { get; private set; }
    public Config_CharacterData Config { get; private set; }
    public BuffManager BuffManager { get; private set; }
    public WeaponManager WeaponManager { get; private set; }
    public SkillManager SkillManager { get; private set; }

    public AudioSource AudioSource { get; private set; }


    private event Action<PlayerEntity> buffDataChange = _ => { };
    /// <summary>
    /// buff改变事件
    /// 受PlayerEntity控制的模块，才注册这个事件
    /// </summary>
    public event Action<PlayerEntity> OnBuffDataChange
    {
        add
        {
            buffDataChange += value;
        }

        remove
        {
            buffDataChange -= value;
        }
    }

    private bool isPauseControl;   //是否暂停控制
    private SimpleGravity mGravity;
    private Vector3 initPos = Vector3.up * 10;

    //userData为playerid
    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        LuaTable table = (LuaTable)userData;
        PlayerId = table.Get<int>("playerId");
        Data = Module.PlayerData.GetData(PlayerId);
        BuffData = Module.PlayerData.GetBuffData(PlayerId);
        WeaponRoot = transform.Find("WeaponRoot");
        Entity.transform.forward = Vector3.right;
        Anim = GetComponent<Animator>();
        Config = Config<Config_CharacterData>.Get("CharacterData", Data.heroId);
        BuffManager = new BuffManager(PlayerId);
        WeaponManager = new WeaponManager(this);
        SkillManager = new SkillManager(this);
        AudioSource = GetComponent<AudioSource>();

        InitController();
        InitStateMachine();
        InitWeapon();
        InitSkill();
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
        Module.Event.Subscribe(SyncBuffDataEventArgs.EventId, OnSyncBuffData);
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
        Module.Event.Unsubscribe(ChangeStateEventArgs<PlayerEntity>.EventId, OnChangeState);
        Module.Event.Unsubscribe(BattleStateChangeArgs.EventId, OnBattleStateChange);
        Module.Event.Unsubscribe(SyncBuffDataEventArgs.EventId, OnSyncBuffData);
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
        WeaponManager.OnUpdate();
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
        mGravity.OnIsAirChange += IsAirChange;
        if (PlayerId == 1)
        {
            Controller = new OnePController(this, mGravity);
        }
        else
        {
            Controller = new TwoPController(this, mGravity);
        }

        Config_CharacterData data = Config<Config_CharacterData>.Get("CharacterData", Data.heroId);
        float speed = data.speed;
        float jumpSpeed = data.jump;
        Controller.AddControlAction(new MoveController(speed, this));
        Controller.AddControlAction(new JumpController(jumpSpeed));
        Controller.AddControlAction(new DushController());
        Controller.AddControlAction(new NormalAttackController());
        Controller.AddControlAction(new ChangeWeaponController());
        Controller.AddControlAction(new UseSkillController());
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
        Machine.AddState(StateLayer.Passive, new GetHitFlyState());
    }

    //初始化玩家武器
    private void InitWeapon()
    {
        Module.Event.Subscribe(ShowEntitySuccessEventArgs.EventId, OnShowEntity);
        Config_Character data = Config<Config_Character>.Get("Character", Data.heroId);
        Data.weaponId = data.initWeapon;
        WeaponManager.InitWeapon(Data.weaponId);
    }

    //初始化技能
    private void InitSkill()
    {
        Config_Character data = Config<Config_Character>.Get("Character", Data.heroId);
        List<int> skillList = data.initSkill;
        for(int i = 0; i < skillList.Count; i++)
        {
            SkillManager.AddSkill(skillList[i]);
        }
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
        if(playerId != Data.id)
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
        switch (type)
        {
            case GetHitType.BeatBack:
                BeatBack(vector);
                break;
            case GetHitType.HitToFly:
                BeatFly(vector);
                break;
        }
    }

    public void PlaySkill(int skillId)
    {
        Anim.SetInteger("skillId", skillId);
        Anim.SetTrigger("skillAnimTigger");
    }

    private void BeatBack(Vector3 vector)
    {
        Entity.transform.position = new Vector3(Entity.transform.position.x, Entity.transform.position.y, 0);   //重置坐标

        mGravity.AddForce("Force", vector * Data.beatBackValue, 0.3f);
        Debug.Log("击退值为==" + Data.beatBackValue + ", 击退百分比为：" + Data.beatBackPercent);
    }

    /// <summary>
    /// 击飞
    /// </summary>
    private void BeatFly(Vector3 vector)
    {
        Entity.transform.position = new Vector3(Entity.transform.position.x, Entity.transform.position.y, 0);   //重置坐标
        if(vector.y != 0)
        {
            mGravity.AddForce("Force", vector * Data.beatBackValue);
        }
        else
        {
            mGravity.AddForce("Force", vector * Data.beatBackValue, 0.3f);
        }

        PlayerBeatFlyEventArgs args = PlayerBeatFlyEventArgs.Create(PlayerId, vector);
        Module.Event.FireNow(this, args);
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

    private void IsAirChange(bool b)
    {
        Anim.SetBool("isAir", b);
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

    private void OnSyncBuffData(object sender, GameEventArgs e)
    {
        SyncBuffDataEventArgs args = e as SyncBuffDataEventArgs;
        if(args.playerId != PlayerId)
        {
            return;
        }
        buffDataChange(this);
    }

    //hide时会调用这个函数，重置数据
    protected override void OnRecycle()
    {
        base.OnRecycle();
        isPauseControl = false;
    }
}
