using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New PowerUp", menuName = "Game/PowerUp")]
public class SO_PowerUpData : ScriptableObject
{
    public string powerUpName;
    public UnityEvent effect;
}
