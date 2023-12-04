using Cinemachine;
using UnityEngine;

public class CameraController
{
    public struct Ctx
    {
        public CinemachineVirtualCamera freeLookVirtualCamera;
        public CinemachineVirtualCamera thirdPersonVirtualCamera;
        public Joystick joystick;
    }

    private readonly Ctx _ctx;

    public CameraController(Ctx ctx)
    {
        _ctx = ctx;
    }
}
