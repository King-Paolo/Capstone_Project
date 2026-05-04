using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    [SerializeField] private SO_WeaponData _data;
    [SerializeField] private int _remainingAmmo;
    [SerializeField] private int _bulletIndex;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private UnityEvent<int, int> _onAmmoChanged;
    [SerializeField] private UnityEvent<Sprite> OnWeaponEquipped;

    private BulletPool _bulletPool;
    private int _currentAmmo;
    private float _shootDelay;
    private bool _isReloading;
    private bool _isAiming;
    private Animator _animator;

    public bool IsAiming { get { return _isAiming; } }

    private void Awake()
    {
        _currentAmmo = _data.maxAmmo;

        if (_bulletPool == null)
        {
            _bulletPool = FindObjectOfType<BulletPool>();
        }

        _animator = GetComponentInParent<Animator>();

        UpdateWeaponIcon();
        UpdateAmmoUI();
    }

    private void OnEnable()
    {
        UpdateWeaponIcon();
        UpdateAmmoUI();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        _isReloading = false;
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (_isReloading)
            return;

        if (Input.GetMouseButton(0) && _currentAmmo > 0)
        {
            _isAiming = true;

            if (Time.time >= _shootDelay)
            {
                Shoot();
                _shootDelay = Time.time + _data.finalFireRate;
            }
        }
        else
        {
            _isAiming = false;
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
            _muzzleFlash.Play();
            bullet.transform.position = _firePoint.position;
            bullet.transform.rotation = _firePoint.rotation;

            bullet.GetComponent<Bullets>().Setup(_firePoint.forward, _data.range, _data.speed, _data.finalDamage);

            _currentAmmo--;
            _currentAmmo = Mathf.Clamp(_currentAmmo, 0, _data.maxAmmo);

            UpdateAmmoUI();
            Debug.Log("munizioni in canna" + _currentAmmo);

            if (_data.shootSound != null)
                AudioManager.Instance.PlaySFX(_data.shootSound);

            if (_data.pumpSound != null && _data.weaponName == "Shotgun")
            {
                StartCoroutine(PlayShotgunPumpSound(_data.delay));
            }
        }
    }

    private void UpdateAmmoUI()
    {
        _onAmmoChanged?.Invoke(_currentAmmo, _remainingAmmo);
    }

    private void UpdateWeaponIcon()
    {
        if (_data.weaponIcon != null)
        {
            OnWeaponEquipped?.Invoke(_data.weaponIcon);
        }
    }


    public IEnumerator Reload()
    {
        Debug.Log("Ricarico" + "munizioni disponibili" + _remainingAmmo);
        _isReloading = true;

        _animator.SetTrigger("IsReloading");

        if (_data.reloadSound != null)
            AudioManager.Instance.PlaySFX(_data.reloadSound);

        yield return new WaitForSeconds(_data.reloadTime);

        int ammoNeeded = _data.maxAmmo - _currentAmmo;
        int amountToTake = Mathf.Min(ammoNeeded, _remainingAmmo);

        _currentAmmo += amountToTake;
        _remainingAmmo -= amountToTake;

        UpdateAmmoUI();

        _isReloading = false;
        Debug.Log(" caricate " + amountToTake + " munizioni in canna " + _currentAmmo + " Munizioni rimanenti " + _remainingAmmo);
    }

    private IEnumerator PlayShotgunPumpSound(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_data.pumpSound != null)
            AudioManager.Instance.PlaySFX(_data.pumpSound);
    }
}
