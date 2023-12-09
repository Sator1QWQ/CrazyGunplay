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

	private PlayerController controller;
    private CharacterController cha;

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        PlayerId = ((int[])userData)[0];
        HeroId = ((int[])userData)[1];
        InitController();
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        controller.OnUpdate();
    }

    private void InitController()
    {
        cha = GetComponent<CharacterController>();
        controller = new OnePController(this, cha);
        Module.Lua.Env.DoString("require 'Configs.Config.CharacterData'");
        LuaTable config = Module.Lua.Env.Global.Get<LuaTable>("CharacterData");
        LuaTable data;
        config.Get(HeroId, out data);

        float speed = data.Get<float>("speed");
        controller.AddControlAction(new MoveController(speed));
        controller.AddControlAction(new JumpController());
    }
}
