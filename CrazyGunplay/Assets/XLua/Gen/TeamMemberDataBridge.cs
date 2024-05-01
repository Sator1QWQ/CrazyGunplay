#if USE_UNI_LUA
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
    public class TeamMemberDataBridge : LuaBase, TeamMemberData
    {
	    public static LuaBase __Create(int reference, LuaEnv luaenv)
		{
		    return new TeamMemberDataBridge(reference, luaenv);
		}
		
		public TeamMemberDataBridge(int reference, LuaEnv luaenv) : base(reference, luaenv)
        {
        }
		
        

        
        int TeamMemberData.teamId 
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
					LuaAPI.xlua_pushasciistring(L, "teamId");
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
					LuaAPI.xlua_pushasciistring(L, "teamId");
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
        
        int TeamMemberData.playerId 
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
					LuaAPI.xlua_pushasciistring(L, "playerId");
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
					LuaAPI.xlua_pushasciistring(L, "playerId");
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
        
        
        
		
		
	}
}
