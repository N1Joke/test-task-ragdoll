using UnityEngine;

public class PlayerController
{
    public struct Ctx
    {
        public Joystick joystick;
        public PlayerSettings settings;
    }

    private readonly Ctx _ctx;

    public PlayerController(Ctx ctx)
    {
        _ctx = ctx;
    }
}
