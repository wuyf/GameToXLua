using System;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : LuaBase
{
    public float maxJumpHeight = 4f;
    public float minJumpHeight = 1f;
    public float timeToJumpApex = .4f;
    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;
    public bool canDoubleJump;
    public float wallSlideSpeedMax = 3f;
    public float wallStickTime = .25f;

    #region Lua相关变量
    Action luaOnJumpInputDown;
    Action luaOnJumpInputUp;
    Action luaHandleWallSliding;
    Action luaCalculateVelocity;
    Action<string> SetDirectionalInput_X;
    Action<string> SetDirectionalInput_Y;
    #endregion
   
    /// <summary>
    /// 初始化Lua函数
    /// </summary>
    protected override void InitFunction()
    {
        base.InitFunction();
        scriptEnv.Get("OnJumpInputDown", out luaOnJumpInputDown);
        scriptEnv.Get("OnJumpInputUp", out luaOnJumpInputUp);
        scriptEnv.Get("HandleWallSliding", out luaHandleWallSliding);
        scriptEnv.Get("CalculateVelocity", out luaCalculateVelocity);
        scriptEnv.Get("SetDirectionalInput_X", out SetDirectionalInput_X);
        scriptEnv.Get("SetDirectionalInput_Y", out SetDirectionalInput_Y);
    }
    /// <summary>
    /// 将c#脚本中的值压入lua脚本中
    /// </summary>
    protected override void SetValueToLua()
    {
        base.SetValueToLua();
        scriptEnv.Set<string,float>("maxJumpHeight", maxJumpHeight);
        scriptEnv.Set<string,float>("minJumpHeight", minJumpHeight);
        scriptEnv.Set<string,float>("timeToJumpApex", timeToJumpApex);
        scriptEnv.Set<string,Vector2>("wallJumpClimb", wallJumpClimb);
        scriptEnv.Set<string,Vector2>("wallJumpOff", wallJumpOff);
        scriptEnv.Set<string,Vector2>("wallLeap", wallLeap);
        scriptEnv.Set<string,bool>("canDoubleJump", canDoubleJump);
        scriptEnv.Set<string,float>("wallSlideSpeedMax", wallSlideSpeedMax);
        scriptEnv.Set<string, float>("wallStickTime", wallStickTime);
    }
    #region lua函数代理c#函数

    
    public void SetDirectionalInput(Vector2 input)
    {
        if (SetDirectionalInput_X != null) SetDirectionalInput_X(input.x.ToString());
        if (SetDirectionalInput_Y != null) SetDirectionalInput_Y(input.y.ToString());

    }
    /// <summary>
    /// 起跳键按下
    /// </summary>
    public void OnJumpInputDown()
    {
        if (luaOnJumpInputDown != null) luaOnJumpInputDown();

    }
    /// <summary>
    /// 起跳键弹起
    /// </summary>
    public void OnJumpInputUp()
    {
        if (luaOnJumpInputUp != null) luaOnJumpInputUp();
    }

    private void HandleWallSliding()
    {
        if (luaHandleWallSliding != null) luaHandleWallSliding();
    }

    private void CalculateVelocity()
    {
        if (luaCalculateVelocity != null) luaCalculateVelocity();

    } 
    #endregion
}
