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
    public class Config_SummonBridge : LuaBase, Config_Summon
    {
	    public static LuaBase __Create(int reference, LuaEnv luaenv)
		{
		    return new Config_SummonBridge(reference, luaenv);
		}
		
		public Config_SummonBridge(int reference, LuaEnv luaenv) : base(reference, luaenv)
        {
        }
		
        

        
        int Config_Summon.id 
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
        
        int Config_Summon.spawnCount 
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
					LuaAPI.xlua_pushasciistring(L, "spawnCount");
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
					LuaAPI.xlua_pushasciistring(L, "spawnCount");
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
        
        float Config_Summon.spawnInterval 
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
					LuaAPI.xlua_pushasciistring(L, "spawnInterval");
					if (0 != LuaAPI.xlua_pgettable(L, -2))
					{
						luaEnv.ThrowExceptionFromError(oldTop);
					}
					float __gen_ret = (float)LuaAPI.lua_tonumber(L, -1);
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
					LuaAPI.xlua_pushasciistring(L, "spawnInterval");
					LuaAPI.lua_pushnumber(L, value);
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
        
        float Config_Summon.moveSpeed 
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
					LuaAPI.xlua_pushasciistring(L, "moveSpeed");
					if (0 != LuaAPI.xlua_pgettable(L, -2))
					{
						luaEnv.ThrowExceptionFromError(oldTop);
					}
					float __gen_ret = (float)LuaAPI.lua_tonumber(L, -1);
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
					LuaAPI.xlua_pushasciistring(L, "moveSpeed");
					LuaAPI.lua_pushnumber(L, value);
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
        
        bool Config_Summon.isContinueFollow 
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
					LuaAPI.xlua_pushasciistring(L, "isContinueFollow");
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
					LuaAPI.xlua_pushasciistring(L, "isContinueFollow");
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
        
        bool Config_Summon.isRandomStartPoint 
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
					LuaAPI.xlua_pushasciistring(L, "isRandomStartPoint");
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
					LuaAPI.xlua_pushasciistring(L, "isRandomStartPoint");
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
        
        bool Config_Summon.isRandomRoute 
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
					LuaAPI.xlua_pushasciistring(L, "isRandomRoute");
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
					LuaAPI.xlua_pushasciistring(L, "isRandomRoute");
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
        
        string Config_Summon.assetPath 
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
					LuaAPI.xlua_pushasciistring(L, "assetPath");
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
					LuaAPI.xlua_pushasciistring(L, "assetPath");
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
        
        
        
		
		
	}
}
