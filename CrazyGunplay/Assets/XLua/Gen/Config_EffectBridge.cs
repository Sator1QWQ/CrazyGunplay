﻿#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System;


namespace XLua.CSObjectWrap
{
    public class Config_EffectBridge : LuaBase, Config_Effect
    {
	    public static LuaBase __Create(int reference, LuaEnv luaenv)
		{
		    return new Config_EffectBridge(reference, luaenv);
		}
		
		public Config_EffectBridge(int reference, LuaEnv luaenv) : base(reference, luaenv)
        {
        }
		
        

        
        int Config_Effect.id 
        {
            
            get 
            {
#if THREAD_SAFE || HOTFIX_ENABLE
                lock (luaEnv.luaEnvLock)
                {
#endif
					RealStatePtr L = luaEnv.L;
					int oldTop = LuaAPI.lua_gettop(L);
					
					LuaAPI.lua_getref(L, luaReference);
					LuaAPI.xlua_pushasciistring(L, "id");
					if (0 != LuaAPI.xlua_pgettable(L, -2))
					{
						luaEnv.ThrowExceptionFromError(oldTop);
					}
					int __gen_ret = LuaAPI.xlua_tointeger(L, -1);
					LuaAPI.lua_pop(L, 2);
					return __gen_ret;
#if THREAD_SAFE || HOTFIX_ENABLE
                }
#endif
            }
            
            
            set
            {
#if THREAD_SAFE || HOTFIX_ENABLE
                lock (luaEnv.luaEnvLock)
                {
#endif
					RealStatePtr L = luaEnv.L;
					int oldTop = LuaAPI.lua_gettop(L);
					
					LuaAPI.lua_getref(L, luaReference);
					LuaAPI.xlua_pushasciistring(L, "id");
					LuaAPI.xlua_pushinteger(L, value);
					if (0 != LuaAPI.xlua_psettable(L, -3))
					{
						luaEnv.ThrowExceptionFromError(oldTop);
					}
					LuaAPI.lua_pop(L, 1);
#if THREAD_SAFE || HOTFIX_ENABLE
                }
#endif
            }
            
        }
        
        string Config_Effect.effectPath 
        {
            
            get 
            {
#if THREAD_SAFE || HOTFIX_ENABLE
                lock (luaEnv.luaEnvLock)
                {
#endif
					RealStatePtr L = luaEnv.L;
					int oldTop = LuaAPI.lua_gettop(L);
					
					LuaAPI.lua_getref(L, luaReference);
					LuaAPI.xlua_pushasciistring(L, "effectPath");
					if (0 != LuaAPI.xlua_pgettable(L, -2))
					{
						luaEnv.ThrowExceptionFromError(oldTop);
					}
					string __gen_ret = LuaAPI.lua_tostring(L, -1);
					LuaAPI.lua_pop(L, 2);
					return __gen_ret;
#if THREAD_SAFE || HOTFIX_ENABLE
                }
#endif
            }
            
            
            set
            {
#if THREAD_SAFE || HOTFIX_ENABLE
                lock (luaEnv.luaEnvLock)
                {
#endif
					RealStatePtr L = luaEnv.L;
					int oldTop = LuaAPI.lua_gettop(L);
					
					LuaAPI.lua_getref(L, luaReference);
					LuaAPI.xlua_pushasciistring(L, "effectPath");
					LuaAPI.lua_pushstring(L, value);
					if (0 != LuaAPI.xlua_psettable(L, -3))
					{
						luaEnv.ThrowExceptionFromError(oldTop);
					}
					LuaAPI.lua_pop(L, 1);
#if THREAD_SAFE || HOTFIX_ENABLE
                }
#endif
            }
            
        }
        
        bool Config_Effect.isEffectFollow 
        {
            
            get 
            {
#if THREAD_SAFE || HOTFIX_ENABLE
                lock (luaEnv.luaEnvLock)
                {
#endif
					RealStatePtr L = luaEnv.L;
					int oldTop = LuaAPI.lua_gettop(L);
					
					LuaAPI.lua_getref(L, luaReference);
					LuaAPI.xlua_pushasciistring(L, "isEffectFollow");
					if (0 != LuaAPI.xlua_pgettable(L, -2))
					{
						luaEnv.ThrowExceptionFromError(oldTop);
					}
					bool __gen_ret = LuaAPI.lua_toboolean(L, -1);
					LuaAPI.lua_pop(L, 2);
					return __gen_ret;
#if THREAD_SAFE || HOTFIX_ENABLE
                }
#endif
            }
            
            
            set
            {
#if THREAD_SAFE || HOTFIX_ENABLE
                lock (luaEnv.luaEnvLock)
                {
#endif
					RealStatePtr L = luaEnv.L;
					int oldTop = LuaAPI.lua_gettop(L);
					
					LuaAPI.lua_getref(L, luaReference);
					LuaAPI.xlua_pushasciistring(L, "isEffectFollow");
					LuaAPI.lua_pushboolean(L, value);
					if (0 != LuaAPI.xlua_psettable(L, -3))
					{
						luaEnv.ThrowExceptionFromError(oldTop);
					}
					LuaAPI.lua_pop(L, 1);
#if THREAD_SAFE || HOTFIX_ENABLE
                }
#endif
            }
            
        }
        
        SkillEffectDeleteTiming Config_Effect.effectDeleteTiming 
        {
            
            get 
            {
#if THREAD_SAFE || HOTFIX_ENABLE
                lock (luaEnv.luaEnvLock)
                {
#endif
					RealStatePtr L = luaEnv.L;
					int oldTop = LuaAPI.lua_gettop(L);
					ObjectTranslator translator = luaEnv.translator;
					LuaAPI.lua_getref(L, luaReference);
					LuaAPI.xlua_pushasciistring(L, "effectDeleteTiming");
					if (0 != LuaAPI.xlua_pgettable(L, -2))
					{
						luaEnv.ThrowExceptionFromError(oldTop);
					}
					SkillEffectDeleteTiming __gen_ret;translator.Get(L, -1, out __gen_ret);
					LuaAPI.lua_pop(L, 2);
					return __gen_ret;
#if THREAD_SAFE || HOTFIX_ENABLE
                }
#endif
            }
            
            
            set
            {
#if THREAD_SAFE || HOTFIX_ENABLE
                lock (luaEnv.luaEnvLock)
                {
#endif
					RealStatePtr L = luaEnv.L;
					int oldTop = LuaAPI.lua_gettop(L);
					ObjectTranslator translator = luaEnv.translator;
					LuaAPI.lua_getref(L, luaReference);
					LuaAPI.xlua_pushasciistring(L, "effectDeleteTiming");
					translator.Push(L, value);
					if (0 != LuaAPI.xlua_psettable(L, -3))
					{
						luaEnv.ThrowExceptionFromError(oldTop);
					}
					LuaAPI.lua_pop(L, 1);
#if THREAD_SAFE || HOTFIX_ENABLE
                }
#endif
            }
            
        }
        
        
        
		
		
	}
}