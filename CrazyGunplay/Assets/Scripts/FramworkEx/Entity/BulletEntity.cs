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
    public Vector3 Down { get; private set; }

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        Gravity = GetComponent<SimpleGravity>();
    }

    protected override void OnShow(object userData)
    {
        base.OnShow(userData);
        Bullet bullet = userData as Bullet;
        LookAt = bullet.OwnerWeapon.Entity.PlayerEntity.LookDirection;
        Down = -transform.up;
    }

    protected override void OnRecycle()
    {
        base.OnRecycle();
        Entity.transform.position = GlobalDefine.FAY_WAY;
        Gravity.ResetGravity();
        Gravity.ResetVelocity();
        LookAt = Vector3.zero;
        Down = Vector3.zero;
    }

    protected override void OnHide(bool isShutdown, object userData)
    {
        base.OnHide(isShutdown, userData);
        Gravity.ResetGravity();
        Gravity.ResetVelocity();
    }
}
