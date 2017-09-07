using MySpace;
using System;
using UnityEngine;
using XLua;

public class LuaBase : MonoBehaviour {
    #region Lua相关变量
   protected LuaEnv luaEvn;
   protected LuaTable scriptEnv;
    //以后可依次添加扩展
    Action luaAwake;
    Action luaStart;
    Action luaUpdate;
    Action luaLateUpdate;
    Action luaOnDrawGizmos;
    public string fileName ;
    #endregion
    /// <summary>
    /// Lua 初始化
    /// </summary>
    private void Awake()
    {
        string luaString = Tool.LoadLuaFile(fileName);
        luaEvn = new LuaEnv();
        scriptEnv = luaEvn.NewTable();
        LuaTable meta = luaEvn.NewTable();
        meta.Set("__index", luaEvn.Global);
        scriptEnv.SetMetaTable(meta);
        luaEvn.DoString(luaString, this.name, scriptEnv);
        SetValueToLua();
        InitFunction();
        if (luaAwake != null) luaAwake();
    }
   /// <summary>
   /// 值压入
   /// </summary>
    protected virtual void SetValueToLua()
    {
        scriptEnv.Set("self", this);
    }
    /// <summary>
    /// 函数绑定
    /// </summary>
    protected virtual void InitFunction()
    {
        scriptEnv.Get("Awake", out luaAwake);
        scriptEnv.Get("Start", out luaStart);
        scriptEnv.Get("Update", out luaUpdate);
        scriptEnv.Get("LateUpdate", out luaLateUpdate);
        scriptEnv.Get("OnDrawGizmos", out luaOnDrawGizmos);
    }
    void Start()
    {
        if (luaStart != null) luaStart();
    }
    private void Update()
    {
        if (luaUpdate != null) luaUpdate();
    }
    private void LateUpdate()
    {
        if (luaLateUpdate != null) luaLateUpdate();
    }
    private void OnDrawGizmos()
    {
        if (luaOnDrawGizmos != null) luaOnDrawGizmos();
    }
}
