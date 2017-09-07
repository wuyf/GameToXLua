using System;
using UnityEngine;
using XLua;
using MySpace;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float maxJumpHeight = 4f;
    public float minJumpHeight = 1f;
    public float timeToJumpApex = .4f;
    private float accelerationTimeAirborne = .2f;
    private float accelerationTimeGrounded = .1f;
    private float moveSpeed = 6f;

    public Vector2 wallJumpClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public bool canDoubleJump;
    private bool isDoubleJumping = false;

    public float wallSlideSpeedMax = 3f;
    public float wallStickTime = .25f;
    private float timeToWallUnstick;

    private float gravity;
    private float maxJumpVelocity;
    private float minJumpVelocity;
    private Vector3 velocity;
    private float velocityXSmoothing;

    private Controller2D controller;

    private Vector2 directionalInput;
    private bool wallSliding;
    private int wallDirX;

    #region Lua相关变量
    LuaEnv luaEvn;
    LuaTable scriptEnv;
    Action luaStart;
    Action luaUpdate;
    Action luaOnJumpInputDown;
    Action luaOnJumpInputUp;
    Action luaHandleWallSliding;
    Action luaCalculateVelocity;
    [CSharpCallLua]
    Action<float ,float> luaSetDirectionalInput;
    string fileName = "Player.lua.txt";
    #endregion
    private void Awake()
    {
        string luaString = Tool.LoadLuaFile(fileName);
        luaEvn = new LuaEnv();
        scriptEnv = luaEvn.NewTable();
        LuaTable meta = luaEvn.NewTable();
        meta.Set("__index", luaEvn.Global);
        scriptEnv.SetMetaTable(meta);
        //luaEvn.DoString(textAsset.text, "PlayerInput", scriptEnv);
        //luaEvn.DoString(luaString, this.name, scriptEnv);
        luaEvn.DoString(luaString);
        SetValueToLua();
        scriptEnv.Get("Start", out luaStart);
        if (luaStart != null) luaStart();
        InitFunction();
    }

    private void InitFunction()
    {
        scriptEnv.Get("Update", out luaUpdate);
        //scriptEnv.Get("SetDirectionalInput", out luaSetDirectionalInput);
        scriptEnv.Get("OnJumpInputDown", out luaOnJumpInputDown);
        scriptEnv.Get("OnJumpInputUp", out luaOnJumpInputUp);
        scriptEnv.Get("HandleWallSliding", out luaHandleWallSliding);
        scriptEnv.Get("CalculateVelocity", out luaCalculateVelocity);
    }

    private void SetValueToLua()
    {
        luaEvn.Set("self", this);
        scriptEnv.Set("maxJumpHeight", maxJumpHeight);
        scriptEnv.Set("minJumpHeight", minJumpHeight);
        scriptEnv.Set("timeToJumpApex", timeToJumpApex);
        scriptEnv.Set("wallJumpClimb", wallJumpClimb);
        scriptEnv.Set("wallJumpOff", wallJumpOff);
        scriptEnv.Set("wallLeap", wallLeap);
        scriptEnv.Set("canDoubleJump", canDoubleJump);
        scriptEnv.Set("wallSlideSpeedMax", wallSlideSpeedMax);
        scriptEnv.Set("wallStickTime", wallStickTime);
    }

    private void Start()
    {
        if (luaStart != null) luaStart();
        //controller = GetComponent<Controller2D>();
        // gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        //maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        //minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    private void Update()
    {
        if (luaUpdate != null) luaUpdate();
        
        //CalculateVelocity();
        //HandleWallSliding();

        //controller.Move(velocity * Time.deltaTime, directionalInput);

        //if (controller.collisions.above || controller.collisions.below)
        //{
        //    velocity.y = 0f;
        //}
    }
    public void SetDirectionalInput(Vector2 input)
    {
        if (luaSetDirectionalInput != null) luaSetDirectionalInput(input.x,input.y);
        //directionalInput = input;
    }

    public void OnJumpInputDown()
    {
        if(luaOnJumpInputDown!=null) luaOnJumpInputDown();
        //if (wallSliding)
        //{
        //    if (wallDirX == directionalInput.x)
        //    {
        //        velocity.x = -wallDirX * wallJumpClimb.x;
        //        velocity.y = wallJumpClimb.y;
        //    }
        //    else if (directionalInput.x == 0)
        //    {
        //        velocity.x = -wallDirX * wallJumpOff.x;
        //        velocity.y = wallJumpOff.y;
        //    }
        //    else
        //    {
        //        velocity.x = -wallDirX * wallLeap.x;
        //        velocity.y = wallLeap.y;
        //    }
        //    isDoubleJumping = false;
        //}
        //if (controller.collisions.below)
        //{
        //    velocity.y = maxJumpVelocity;
        //    isDoubleJumping = false;
        //}
        //if (canDoubleJump && !controller.collisions.below && !isDoubleJumping && !wallSliding)
        //{
        //    velocity.y = maxJumpVelocity;
        //    isDoubleJumping = true;
        //}
    }

    public void OnJumpInputUp()
    {
        if (luaOnJumpInputUp != null) luaOnJumpInputUp();
        //if (velocity.y > minJumpVelocity)
        //{
        //    velocity.y = minJumpVelocity;
        //}
    }

    private void HandleWallSliding()
    {
        if (luaHandleWallSliding != null) luaHandleWallSliding();
        //wallDirX = (controller.collisions.left) ? -1 : 1;
        //wallSliding = false;
        //if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        //{
        //    wallSliding = true;

        //    if (velocity.y < -wallSlideSpeedMax)
        //    {
        //        velocity.y = -wallSlideSpeedMax;
        //    }

        //    if (timeToWallUnstick > 0f)
        //    {
        //        velocityXSmoothing = 0f;
        //        velocity.x = 0f;
        //        if (directionalInput.x != wallDirX && directionalInput.x != 0f)
        //        {
        //            timeToWallUnstick -= Time.deltaTime;
        //        }
        //        else
        //        {
        //            timeToWallUnstick = wallStickTime;
        //        }
        //    }
        //    else
        //    {
        //        timeToWallUnstick = wallStickTime;
        //    }
        //}
    }

    private void CalculateVelocity()
    {
        if (luaCalculateVelocity != null) luaCalculateVelocity();
        //float targetVelocityX = directionalInput.x * moveSpeed;
        //velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below ? accelerationTimeGrounded : accelerationTimeAirborne));
        //velocity.y += gravity * Time.deltaTime;
    }
}
