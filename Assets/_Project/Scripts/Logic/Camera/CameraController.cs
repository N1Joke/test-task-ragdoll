using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CameraController
{
    public struct Ctx
    {
        public CinemachineVirtualCamera freeLookVirtualCamera;
        public CinemachineFreeLook thirdPersonVirtualCamera;
        public Joystick joystickCameraRotation;
        public Joystick joystickCameraPosition;
        public PlayerInputType playerInputType;        
        public Button freeLookButton;
        public Button thirdPersonButton;
        public Color activeColor;
        public Color inactiveColor;
    }

    private readonly Ctx _ctx;
    private CameraMod _currentCameraMod;
    private Image _freeLookImage;
    private Image _thirdPersonImage;
    private float _freeCameraMoveSpeed = 5f;
    private float _freeCameraRotationSpeed = 0.25f;

    public Action<CameraMod> OnCameraModChanged;

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
        if (_ctx.thirdPersonButton)
        {
            _thirdPersonImage = _ctx.thirdPersonButton.GetComponent<Image>();
            _ctx.thirdPersonButton.onClick.AddListener(() =>
            {
                OnCameraModSwitch(CameraMod.ThirdPerson);
            });
        }

        if (_ctx.freeLookButton)
        {
            _freeLookImage = _ctx.freeLookButton.GetComponent<Image>();
            _ctx.freeLookButton.onClick.AddListener(() =>
            {
                OnCameraModSwitch(CameraMod.FreeLook);
            });
        }

        OnCameraModSwitch(CameraMod.ThirdPerson);
    }

    public void Update(float deltaTime)
    {
        switch (_currentCameraMod)
        {
            case CameraMod.ThirdPerson:
                {
                    _ctx.thirdPersonVirtualCamera.m_YAxis.m_InputAxisValue = _ctx.joystickCameraRotation.Vertical;
                    _ctx.thirdPersonVirtualCamera.m_XAxis.m_InputAxisValue = _ctx.joystickCameraRotation.Horizontal;
                    break;
                }
            case CameraMod.FreeLook:
                {
                    //Free camera movement
                    float horizontal = _ctx.joystickCameraPosition.Horizontal;
                    float vertical = _ctx.joystickCameraPosition.Vertical;

                    Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;
                    Vector3 moveAmount = moveDirection * _freeCameraMoveSpeed * deltaTime;

                    _ctx.freeLookVirtualCamera.transform.Translate(moveAmount);

                    //Free camera rotation
                    float mouseX = _ctx.joystickCameraRotation.Horizontal;
                    float mouseY = _ctx.joystickCameraRotation.Vertical;

                    _ctx.freeLookVirtualCamera.transform.Rotate(Vector3.up * mouseX * _freeCameraRotationSpeed);
                    _ctx.freeLookVirtualCamera.transform.Rotate(Vector3.left * mouseY * _freeCameraRotationSpeed);
                    break;
                }
        }
    }

    private void OnCameraModSwitch(CameraMod newMod)
    {
        if (_currentCameraMod == newMod)
            return;

        _currentCameraMod = newMod;
        OnCameraModChanged?.Invoke(_currentCameraMod);

        switch(_currentCameraMod)
        {
            case CameraMod.ThirdPerson:
                {
                    _ctx.freeLookButton.enabled = true;
                    _freeLookImage.color = _ctx.activeColor;
                    _ctx.thirdPersonButton.enabled = false;
                    _thirdPersonImage.color = _ctx.inactiveColor;
                    _ctx.thirdPersonVirtualCamera.enabled = true;
                    _ctx.freeLookVirtualCamera.enabled = false;
                    break;
                }
                case CameraMod.FreeLook:
                {
                    _ctx.freeLookButton.enabled = false;
                    _freeLookImage.color = _ctx.inactiveColor;
                    _ctx.thirdPersonButton.enabled = true;
                    _thirdPersonImage.color = _ctx.activeColor;
                    _ctx.thirdPersonVirtualCamera.enabled = false;
                    _ctx.freeLookVirtualCamera.enabled = true;
                    break;
                }
        }
    }
}
