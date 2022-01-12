﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class DG_Tweening_PathModeWrap
{
	public static void Register(LuaState L)
	{
		L.BeginEnum(typeof(DG.Tweening.PathMode));
		L.RegVar("Ignore", new LuaCSFunction(get_Ignore), null);
		L.RegVar("Full3D", new LuaCSFunction(get_Full3D), null);
		L.RegVar("TopDown2D", new LuaCSFunction(get_TopDown2D), null);
		L.RegVar("Sidescroller2D", new LuaCSFunction(get_Sidescroller2D), null);
		L.RegFunction("IntToEnum", new LuaCSFunction(IntToEnum));
		L.EndEnum();
		TypeTraits<DG.Tweening.PathMode>.Check = CheckType;
		StackTraits<DG.Tweening.PathMode>.Push = Push;
	}

	static void Push(IntPtr L, DG.Tweening.PathMode arg)
	{
		ToLua.Push(L, arg);
	}

	static Type TypeOf_DG_Tweening_PathMode = typeof(DG.Tweening.PathMode);

	static bool CheckType(IntPtr L, int pos)
	{
		return TypeChecker.CheckEnumType(TypeOf_DG_Tweening_PathMode, L, pos);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Ignore(IntPtr L)
	{
		ToLua.Push(L, DG.Tweening.PathMode.Ignore);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Full3D(IntPtr L)
	{
		ToLua.Push(L, DG.Tweening.PathMode.Full3D);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_TopDown2D(IntPtr L)
	{
		ToLua.Push(L, DG.Tweening.PathMode.TopDown2D);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Sidescroller2D(IntPtr L)
	{
		ToLua.Push(L, DG.Tweening.PathMode.Sidescroller2D);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tointeger(L, 1);
		DG.Tweening.PathMode o = (DG.Tweening.PathMode)arg0;
		ToLua.Push(L, o);
		return 1;
	}
}

