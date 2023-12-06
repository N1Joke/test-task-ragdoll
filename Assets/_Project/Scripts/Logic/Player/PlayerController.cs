using UnityEngine.UI;
using System;

public class PlayerController : IDisposable
{
    public struct Ctx
    {
        public Joystick joystick;
        public PlayerSettings settings;
        public PlayerView view;
        public PlayerInputType playerInputType;
        public Button jumpButton;
        public CameraController cameraController;
    }

    private readonly Ctx _ctx;

    public PlayerController(Ctx ctx)
    {
        _ctx = ctx;

        switch (_ctx.playerInputType)
        {
            case PlayerInputType.Touch:
                {
                    _ctx.view.UserControlThirdPerson.enabled = false;
                    _ctx.view.CharacterPuppet.userControl = _ctx.view.UserControlThirdPersonTouch;
                    _ctx.view.UserControlThirdPersonTouch.joystick = _ctx.joystick;
                    if (_ctx.jumpButton != null)
                        _ctx.jumpButton.onClick.AddListener(_ctx.view.UserControlThirdPersonTouch.DoJump);
                    break;
                }
            case PlayerInputType.Mouse:
                {
                    _ctx.view.UserControlThirdPersonTouch.enabled = false;
                    _ctx.view.CharacterPuppet.userControl = _ctx.view.UserControlThirdPerson;
                    break;
                }
        }

        TogglePause(false);

        _ctx.cameraController.OnCameraModChanged += OnCameraModChanged;
    }

    public void Dispose()
    {
        _ctx.cameraController.OnCameraModChanged -= OnCameraModChanged;
    }

    private void OnCameraModChanged(CameraMod cameraMod)
    {
        switch (cameraMod)
        {
            case CameraMod.FreeLook:
                {
                    TogglePause(true);
                    break;
                }
            case CameraMod.ThirdPerson:
                {
                    TogglePause(false);
                    break;
                }
        }
    }    

    private void TogglePause(bool pause)
    {
        switch (_ctx.playerInputType)
        {
            case PlayerInputType.Touch:
                {
                    _ctx.view.UserControlThirdPersonTouch.enabled = !pause;
                    break;
                }
            case PlayerInputType.Mouse:
                {
                    _ctx.view.UserControlThirdPerson.enabled = !pause;
                    break;
                }
        }
    }
}
