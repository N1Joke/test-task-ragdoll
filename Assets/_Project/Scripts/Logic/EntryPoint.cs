using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private CinemachineVirtualCamera _freeLookVirtualCamera;
    [SerializeField] private CinemachineFreeLook _thirdPersonVirtualCamera;    
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private Joystick _movementJoystick;
    [SerializeField] private Joystick _cameraJoystick;
    [SerializeField] private Button _jumpButton;
    [SerializeField] private Button _freeLookButton;
    [SerializeField] private Button _thirdPersonButton;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;

    private CameraController _cameraController;
    private PlayerController _playerController;

    private void Awake()
    {
        _cameraController = new CameraController(new CameraController.Ctx
        {           
            freeLookVirtualCamera = _freeLookVirtualCamera,
            thirdPersonVirtualCamera = _thirdPersonVirtualCamera,
            joystickCameraRotation = _cameraJoystick,
            joystickCameraPosition = _movementJoystick,
            playerInputType = _gameSettings.PlayerInputType,
            freeLookButton = _freeLookButton,
            thirdPersonButton = _thirdPersonButton,
            activeColor = _activeColor,
            inactiveColor = _inactiveColor
        });

        _playerController = new PlayerController(new PlayerController.Ctx
        {
            joystick = _movementJoystick,
            playerInputType = _gameSettings.PlayerInputType,
            view = _playerView,
            jumpButton = _jumpButton,
            cameraController = _cameraController
        });                
    }

    private void OnDestroy()
    {
        _playerController.Dispose();
    }

    private void Update()
    {
        if (_cameraController != null)
        {
            _cameraController.Update(Time.deltaTime);
        }
    }
}
