﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityGameFramework.Runtime;

/*
* 作者：
* 日期：
* 描述：
		
*/
public class GrenadeBullet : Bullet
{
    public GrenadeBullet(int bulletId, int gunId) : base(bulletId, gunId)
    {
    }

    public override void Fly()
    {
        throw new System.NotImplementedException();
    }

    public override void OnCollision(GameObject target)
    {
        throw new System.NotImplementedException();
    }
}
