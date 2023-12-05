using UnityEngine.UI;

public class PlayerController
{
    public struct Ctx
    {
        public Joystick joystick;
        public PlayerSettings settings;
        public PlayerView view;
        public PlayerInputType playerInputType;
        public Button jumpButton;
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
                    _ctx.view.UserControlThirdPersonTouch.enabled = true;
                    _ctx.view.CharacterPuppet.userControl = _ctx.view.UserControlThirdPersonTouch;
                    _ctx.view.UserControlThirdPersonTouch.joystick = _ctx.joystick;
                    if (_ctx.jumpButton != null)
                        _ctx.jumpButton.onClick.AddListener(_ctx.view.UserControlThirdPersonTouch.DoJump);
                    break;
                }
            case PlayerInputType.Mouse:
                {
                    _ctx.view.UserControlThirdPerson.enabled = true;
                    _ctx.view.UserControlThirdPersonTouch.enabled = false;
                    _ctx.view.CharacterPuppet.userControl = _ctx.view.UserControlThirdPerson;
                    break;
                }
        }
    }
}
