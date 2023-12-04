using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/PlayerSettings", order = 0)]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpHeight;

    public float Speed => _speed;
    public float JumpHeight => _jumpHeight;
}
