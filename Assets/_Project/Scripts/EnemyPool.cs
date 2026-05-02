using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private int[] _poolSizes;

    private List<GameObject>[] _pools;

    private void Awake()
    {
        _pools = new List<GameObject>[_enemyPrefabs.Length];

        for (int i = 0; i < _enemyPrefabs.Length; i++)
        {
            _pools[i] = new List<GameObject>();

            for (int j = 0; j < _poolSizes[i]; j++)
            {
                GameObject enemy = Instantiate(_enemyPrefabs[i], transform);
                enemy.SetActive(false);
                _pools[i].Add(enemy);
            }
        }
    }

    public GameObject GetEnemy(int index)
    {
        foreach (var enemy in _pools[index])
        {
            if (!enemy.activeInHierarchy)
            {
                return enemy;
            }
        }

        GameObject enemyExtra = Instantiate(_enemyPrefabs[index], transform);
        enemyExtra.SetActive(false);
        _pools[index].Add(enemyExtra);
        return enemyExtra;
    }
}
