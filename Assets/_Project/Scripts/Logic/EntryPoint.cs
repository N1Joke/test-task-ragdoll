using Cinemachine;
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

    private CameraController _cameraController;
    private PlayerController _playerController;

    private void Awake()
    {
        _cameraController = new CameraController(new CameraController.Ctx
        {
            freeLookVirtualCamera = _freeLookVirtualCamera,
            thirdPersonVirtualCamera = _thirdPersonVirtualCamera,
            joystick = _cameraJoystick,
            playerInputType = _gameSettings.PlayerInputType
        });

        _playerController = new PlayerController(new PlayerController.Ctx
        {
            joystick = _movementJoystick,
            playerInputType = _gameSettings.PlayerInputType,
            view = _playerView,
            jumpButton = _jumpButton
        });
    }

    private void Update()
    {
        if (_cameraController != null)
        {
            _cameraController.Update(Time.deltaTime);
        }
    }
}
