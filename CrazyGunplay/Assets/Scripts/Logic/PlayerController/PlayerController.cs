using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
	玩家控制器
*/
public abstract class PlayerController
{
    /// <summary>
    /// 玩家实体
    /// </summary>
    public PlayerEntity Entity { get; private set; }

    public WeaponEntity Weapon { get; private set; }

    /// <summary>
    /// 重力模拟不用unity的
    /// </summary>
    public SimpleGravity Gravity { get; private set; }

    /// <summary>
    /// 是否使用重力
    /// </summary>
    public bool UseGravity { get; set; }

    /// <summary>
    /// 是否暂停控制
    /// </summary>
    public bool IsPause { get; set; }

    /// <summary>
    /// 突进距离
    /// </summary>
    public float DushDistance { get; private set; }
    
    /// <summary>
    /// 可突进次数
    /// </summary>
    public int CanDushNum { get; private set; }

    /// <summary>
    /// 空中可跳跃次数
    /// </summary>
    public int CanAirJumpNum { get; private set; }

    /// <summary>
    /// 控制器字典，移动、跳跃这些
    /// </summary>
    private Dictionary<ControllerType, ControlActionBase> mControllerDic = new Dictionary<ControllerType, ControlActionBase>();
    private int mDushCount; //dush次数 在空中时才计数
    private int mJumpCount; //jump计数 在空中时才计数

    public PlayerController(PlayerEntity entity, SimpleGravity gravity)
    {
        Entity = entity;
        Gravity = gravity;
        Config_CharacterData cfg = Config<Config_CharacterData>.Get("CharacterData", Entity.Data.heroId);
        DushDistance = cfg.dushDistance;
        CanDushNum = cfg.dushNum;
        CanAirJumpNum = cfg.airJumpNum;
        Gravity.OnTouchGround += OnTouchGround;
    }

    public void AddControlAction(ControlActionBase ctrl)
    {
        mControllerDic.Add(ctrl.CtrlType, ctrl);
    }

    /// <summary>
    /// 水平数值
    /// </summary>
    /// <returns></returns>
    public abstract float GetHorizontal();

    /// <summary>
    /// 垂直数值
    /// </summary>
    /// <returns></returns>
    public abstract float GetVertical();

    public abstract bool GetJumpInput();

    public abstract bool GetMove();

    public abstract bool GetDushInput();

    public abstract bool GetNormalAttack();

    /// <summary>
    /// 武器槽切换
    /// </summary>
    /// <returns></returns>
    public abstract int GetChangeWeapon();

    /// <summary>
    /// 是否可跳跃
    /// </summary>
    /// <returns></returns>
    public bool GetJump()
    {
        if (GetJumpInput())
        {
            if (Gravity.IsAir)
            {
                //可跳跃
                if (mJumpCount < CanAirJumpNum)
                {
                    return true;
                }
                //超过最大跳跃次数，不可跳跃
                else
                {
                    return false;
                }
            }
            //地上无限跳跃
            else
            {
                return true;
            }
        }
        //没按下跳跃键
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 是否可dush
    /// </summary>
    /// <returns></returns>
    public bool GetDush()
    {
        if(GetDushInput())
        {
            if (Gravity.IsAir)
            {
                //可dush
                if (mDushCount < CanDushNum)
                {
                    return true;
                }
                //空中超过最大dush次数，不可dush
                else
                {
                    return false;
                }
            }
            //地上无限dush
            else
            {
                return true;
            }
        }
        //没按下dush键
        else
        {
            return false;
        }
    }

    public void OnUpdate()
    {
        if(IsPause)
        {
            return;
        }

        if (GetMove())
        {
            Debug.Log("Move");
            UpdateControllerAction(ControllerType.Move);
        }

        if (GetJump())
        {
            if(Gravity.IsAir && mJumpCount < CanAirJumpNum)
            {
                mJumpCount++;
            }
            UpdateControllerAction(ControllerType.Jump);
        }

        if(GetDush())
        {
            if (Gravity.IsAir && mDushCount < CanDushNum)
            {
                mDushCount++;
            }
            UpdateControllerAction(ControllerType.Dush);
        }

        if (GetNormalAttack())
        {
            UpdateControllerAction(ControllerType.NomralAttack);
        }

        if(GetChangeWeapon() != -1)
        {
            UpdateControllerAction(ControllerType.ChangeWeapon);
        }
    }

    private void UpdateControllerAction(ControllerType type)
    {
        if(mControllerDic.ContainsKey(type))
        {
            mControllerDic[type].DoAction(this);
        }
    }

    private void OnTouchGround()
    {
        mJumpCount = 0;
        mDushCount = 0;
    }
}
