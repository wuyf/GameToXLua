using UnityEngine;

public class CameraFollow : LuaBase
{
    public Controller2D target;
    public float verticalOffset;
    public float lookAheadDstX;
    public float lookSmoothTimeX;
    public float verticalSmoothTime;
    public Vector2 focusAreaSize;

    protected override void SetValueToLua()
    {
        base.SetValueToLua();
        scriptEnv.Set<string, Controller2D>("target", target);
        scriptEnv.Set<string, float>("verticalOffset", verticalOffset);
        scriptEnv.Set<string, float>("lookAheadDstX", lookAheadDstX);
        scriptEnv.Set<string, float>("lookSmoothTimeX", lookSmoothTimeX);
        scriptEnv.Set<string, float>("verticalSmoothTime", verticalSmoothTime);
        scriptEnv.Set<string, Vector2>("focusAreaSize", focusAreaSize);
    }
}
