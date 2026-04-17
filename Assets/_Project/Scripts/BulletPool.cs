using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject[] _bulletPrefabs;
    [SerializeField] private int[] _poolSizes;

    private List<GameObject>[] _pools;

    private void Awake()
    {
        _pools = new List<GameObject>[_bulletPrefabs.Length];

        for (int i = 0; i < _bulletPrefabs.Length; i++)
        {
            _pools[i] = new List<GameObject>();

            for (int j = 0; j < _poolSizes[i]; j++)
            {
                GameObject bullet = Instantiate(_bulletPrefabs[i], transform);
                bullet.SetActive(false);
                _pools[i].Add(bullet);
            }
        }
    }

    public GameObject GetBullet(int index)
    {
        foreach (var bullet in _pools[index])
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }

        GameObject bulletExtra = Instantiate(_bulletPrefabs[index], transform);
        _pools[index].Add(bulletExtra);
        return bulletExtra;
    }
}
