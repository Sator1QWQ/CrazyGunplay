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

    /// <summary>
    /// 角色控制器
    /// </summary>
    public CharacterController CharacterController;

    /// <summary>
    /// 控制器字典，移动、跳跃这些
    /// </summary>

    private Dictionary<ControllerType, ControlActionBase> mControllerDic = new Dictionary<ControllerType, ControlActionBase>();

    public PlayerController(PlayerEntity entity, CharacterController controller)
    {
        Entity = entity;
        CharacterController = controller;
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

    public abstract bool GetJump();

    public abstract bool GetMove();

    public abstract bool GetNormalAttack();

    public void OnUpdate()
    {
        CharacterController.SimpleMove(Vector3.zero);

        if (GetMove())
        {
            Debug.Log("Move");
            UpdateControllerAction(ControllerType.Move);
        }

        if (GetJump())
        {
            Debug.Log("JUmp");
            UpdateControllerAction(ControllerType.Jump);

        }

        if (GetNormalAttack())
        {
            UpdateControllerAction(ControllerType.NomralAttack);
        }
    }

    private void UpdateControllerAction(ControllerType type)
    {
        if(mControllerDic.ContainsKey(type))
        {
            mControllerDic[type].DoAction(this);
        }
    }
}
