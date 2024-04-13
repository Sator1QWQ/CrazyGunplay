using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
		
*/
public class BulletEntity : EntityLogic
{
	public SimpleGravity Gravity { get; private set; }
    public Vector3 LookAt { get; private set; }

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        Gravity = GetComponent<SimpleGravity>();
        LookAt = transform.right;
    }

    protected override void OnRecycle()
    {
        base.OnRecycle();
        Entity.transform.position = GlobalDefine.FAY_WAY;
        Gravity.ResetGravity();
        Gravity.ResetVelocity();
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
        Gravity.ResetGravity();
        Gravity.ResetVelocity();
    }
}
