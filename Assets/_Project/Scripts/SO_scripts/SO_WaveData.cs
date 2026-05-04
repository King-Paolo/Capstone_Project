using UnityEngine;

[CreateAssetMenu(fileName = "NewWave", menuName = "Wave System/Wave Data")]
public class SO_WaveData : ScriptableObject
{
    public string waveName;
    public float delayBeforeWave;
    public int enemyCount;
    public float spawnRate;
}
