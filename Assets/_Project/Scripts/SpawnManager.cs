using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private UnityEvent<int> OnWaveChanged;

    [Header("Wave Settings")]
    [SerializeField] private WaveData[] _waves;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        for (int i = 0; i < _waves.Length; i++)
        {
            OnWaveChanged?.Invoke(i + 1);

            WaveData currentWave = _waves[i];

            yield return new WaitForSeconds(currentWave.delayBeforeWave);

            _enemySpawner.StartWave(currentWave);

            yield return new WaitForEndOfFrame();

            yield return new WaitUntil(() => _enemySpawner.IsWaveCleared);

            yield return new WaitForSeconds(2f);
        }
    }
}
