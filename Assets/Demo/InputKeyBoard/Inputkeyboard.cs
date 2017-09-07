using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class Inputkeyboard : MonoBehaviour {
    public TextAsset textAsset;
    LuaEnv luaEnv;
    private LuaTable scriptEnv;
    Action luaStart;
    Action luaUpdate;
    public Transform target;
    // Use this for initialization
    void Start () {
        luaEnv = new LuaEnv();
        scriptEnv = luaEnv.NewTable();
        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        luaEnv.DoString(textAsset.text, "Inputkeyboard",scriptEnv);

        #region 读取和修改Lua变量
        //int speed = luaEnv.Global.Get<int>("speed");
        //Debug.Log("======speed=====" + speed);
        //Debug.Log("_G.a = " + luaEnv.Global.Get<int>("a"));
        //luaEnv.Global.Set<string, int>("a", 100);
        //Debug.Log("_G.a = " + luaEnv.Global.Get<int>("a"));
        //Debug.Log("_G.b = " + luaEnv.Global.Get<string>("b"));
        //luaEnv.Global.Set<string, string>("b", "袋娱科技");
        //Debug.Log("_G.b = " + luaEnv.Global.Get<string>("b"));
        //Debug.Log("_G.c = " + luaEnv.Global.Get<bool>("c"));
        //luaEnv.Global.Set<string, bool>("c", false);
        //Debug.Log("_G.a = " + luaEnv.Global.Get<bool>("c"));
        #endregion


        // scriptEnv.Set("self", target);
        Debug.Log("=====speed=======" + scriptEnv.Get<int>("speed"));
        scriptEnv.Set<string,int>("speed",100);
        Debug.Log("=====speed=======" + scriptEnv.Get<int>("speed"));
        scriptEnv.Get("start", out luaStart);
        if (luaStart != null) luaStart();
        scriptEnv.Get("update", out luaUpdate);

        
	}
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Debug.Log("A键按下了");
        //}
        if (luaUpdate != null) luaUpdate();
    }
}
