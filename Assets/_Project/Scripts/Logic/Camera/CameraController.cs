using Cinemachine;

public class CameraController
{
    public struct Ctx
    {
        public CinemachineVirtualCamera freeLookVirtualCamera;
        public CinemachineFreeLook thirdPersonVirtualCamera;
        public Joystick joystick;
        public PlayerInputType playerInputType;
    }

    private readonly Ctx _ctx;

    public CameraController(Ctx ctx)
    {
        _ctx = ctx;

        switch (_ctx.playerInputType)
        {
            case PlayerInputType.Touch:
                {
                    _ctx.thirdPersonVirtualCamera.m_YAxis.m_InputAxisName = "";
                    _ctx.thirdPersonVirtualCamera.m_XAxis.m_InputAxisName = "";
                    break;
                }
            case PlayerInputType.Mouse:
                {
                    break;
                }
        }
    }

    public void Update(float deltaTime)
    {
        _ctx.thirdPersonVirtualCamera.m_YAxis.m_InputAxisValue = _ctx.joystick.Vertical;
        _ctx.thirdPersonVirtualCamera.m_XAxis.m_InputAxisValue = _ctx.joystick.Horizontal;
    }
}
