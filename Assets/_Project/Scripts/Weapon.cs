using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private SO_WeaponData _data;
    [SerializeField] private int _remainingAmmo;
    [SerializeField] private int _bulletIndex;
    [SerializeField] private Transform _firePoint;

    private BulletPool _bulletPool;
    private int _currentAmmo;
    private float _shootDelay;
    private bool _isReloading;

    private void Awake()
    {
        _currentAmmo = _data.maxAmmo;

        if (_bulletPool == null)
        {
            _bulletPool = FindObjectOfType<BulletPool>();
        }
    }

    private void Update()
    {
        if (_isReloading)
            return;

        if (Input.GetMouseButton(0) && _currentAmmo > 0)
        {
            if (Time.time >= _shootDelay)
            {
                Shoot();
                _shootDelay = Time.time + _data.fireRate;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && _currentAmmo < _data.maxAmmo && _remainingAmmo > 0)
        {
            StartCoroutine(Reload());
        }
    }

    public void Shoot()
    {
        GameObject bullet = _bulletPool.GetBullet(_bulletIndex);

        if (bullet != null)
        {
            bullet.transform.position = _firePoint.position;
            bullet.transform.rotation = _firePoint.rotation;

            bullet.GetComponent<Bullets>().Setup(_firePoint.forward, _data.range, _data.speed, _data.damage);

            _currentAmmo--;
            _currentAmmo = Mathf.Clamp(_currentAmmo, 0, _data.maxAmmo);
            Debug.Log("munizioni in canna" + _currentAmmo);
        }
    }

    public IEnumerator Reload()
    {
        Debug.Log("Ricarico" + "munizioni disponibili" + _remainingAmmo);
        _isReloading = true;

        yield return new WaitForSeconds(_data.reloadTime);

        int ammoNeeded = _data.maxAmmo - _currentAmmo;
        int amountToTake = Mathf.Min(ammoNeeded, _remainingAmmo);

        _currentAmmo += amountToTake;
        _remainingAmmo -= amountToTake;

        _isReloading = false;
        Debug.Log(" caricate " + amountToTake + " munizioni in canna " + _currentAmmo + " Munizioni rimanenti " + _remainingAmmo);
    }
}
