using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private UnityEvent<int> OnWaveChanged;
    [SerializeField] private UI_PowerUpPanel _powerUpPanel;
    [SerializeField] private GameObject _waveClearedBanner;
    [SerializeField] private Helicopter _helicopter;
    [SerializeField] private GameObject _helicoperText;
    [SerializeField] private GameObject _keysMap;

    [Header("Wave Settings")]
    [SerializeField] private SO_WaveData[] _waves;
    [SerializeField] private float _delayBetweenWaves = 3f;

    private int _waveCount = 0;

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
        MenuManager.Instance.KeysMap(_keysMap);
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        for (int i = 0; i < _waves.Length; i++)
        {
            _waveCount = i + 1;

            OnWaveChanged?.Invoke(i + 1);

            SO_WaveData currentWave = _waves[i];

            yield return new WaitForSeconds(currentWave.delayBeforeWave);

            _enemySpawner.StartWave(currentWave);

            yield return new WaitForEndOfFrame();

            yield return new WaitUntil(() => _enemySpawner.IsWaveCleared);

            MenuManager.Instance.WaveCleared(_waveClearedBanner);

            if (_waveCount == _waves.Length)
            {
                _helicopter.gameObject.SetActive(true);

                MenuManager.Instance.WaitHelicopterText(_helicoperText);
            }
            else
            {
                yield return new WaitForSeconds(1f);
                _powerUpPanel.ShowPowerUps();
                yield return new WaitForSeconds(_delayBetweenWaves);
            }
        }
    }
}
