using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    [SerializeField] private PlayerInputType _playerInputType;

    public PlayerInputType PlayerInputType => _playerInputType;
}
