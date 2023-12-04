using Cinemachine;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _freeLookVirtualCamera;
    [SerializeField] private CinemachineVirtualCamera _thirdPersonVirtualCamera;
    [SerializeField] private PlayerSettings _playerSettings;
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private Joystick _movementJoystick;
    [SerializeField] private Joystick _cameraJoystick;

    private CameraController _cameraController;
    private PlayerController _playerController;

    private void Awake()
    {
        _cameraController = new CameraController(new CameraController.Ctx
        {
            freeLookVirtualCamera = _freeLookVirtualCamera,
            thirdPersonVirtualCamera = _thirdPersonVirtualCamera,
            joystick = _cameraJoystick
        });

        _playerController = new PlayerController(new PlayerController.Ctx
        {
            joystick = _movementJoystick,
            settings = _playerSettings
        });
    }
}
