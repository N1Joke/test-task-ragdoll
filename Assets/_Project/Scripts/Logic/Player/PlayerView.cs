using RootMotion.Demos;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private UserControlThirdPerson _userControlThirdPersonKeyboardAndMouse;
    [SerializeField] private UserControlThirdPersonTouch _userControlThirdPersonTouch;
    [SerializeField] private CharacterPuppet _characterPuppet;

    public CharacterPuppet CharacterPuppet => _characterPuppet;
    public UserControlThirdPerson UserControlThirdPerson => _userControlThirdPersonKeyboardAndMouse;
    public UserControlThirdPersonTouch UserControlThirdPersonTouch => _userControlThirdPersonTouch;
}
