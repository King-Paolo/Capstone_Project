using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private EnemyPool _enemyPool;

    private int _enemiesAlive = 0;

    public bool IsWaveCleared => _enemiesAlive <= 0;

    public void StartWave(WaveData wave)
    {
        StartCoroutine(SpawnWaveRoutine(wave));
    }

    private void SpawnSingleEnemy()
    {
        int randomIndex = Random.Range(0, 100);
        int indexToSpawn;

        if (randomIndex < 50) indexToSpawn = 0;
        else if (randomIndex < 80) indexToSpawn = 1;
        else if (randomIndex < 95) indexToSpawn = 2;
        else indexToSpawn = 3;

        GameObject enemy = _enemyPool.GetEnemy(indexToSpawn);

        if (enemy != null)
        {
            Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            Vector3 offset = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));

            enemy.transform.position = spawnPoint.position + offset;

            enemy.SetActive(true);
        }
    }

    public void EnemyDied()
    {
        _enemiesAlive--;

        if (_enemiesAlive < 0) _enemiesAlive = 0;
    }

    private IEnumerator SpawnWaveRoutine(WaveData wave)
    {
        _enemiesAlive = 0;

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnSingleEnemy();
            _enemiesAlive++;

            yield return new WaitForSeconds(wave.spawnRate);
        }
    }
}
