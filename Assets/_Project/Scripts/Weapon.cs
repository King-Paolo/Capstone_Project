using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private SO_WeaponData _data;
    [SerializeField] private int _remainingAmmo;

    private int _currentAmmo;
    private float _shootDelay;
    private bool _isReloading;

    private void Awake()
    {
        _currentAmmo = _data.maxAmmo;
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
        _currentAmmo--;
        _currentAmmo = Mathf.Clamp(_currentAmmo, 0, _data.maxAmmo);
    }

    public IEnumerator Reload()
    {
        _isReloading = true;

        yield return new WaitForSeconds(_data.reloadTime);

        int ammoNeeded = _data.maxAmmo - _currentAmmo;
        int amountToTake = Mathf.Min(ammoNeeded, _remainingAmmo);

        _currentAmmo += amountToTake;
        _remainingAmmo -= amountToTake;

        _isReloading = false;
    }
}
