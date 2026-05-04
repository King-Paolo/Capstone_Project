using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "New Weapon")]
public class SO_WeaponData : ScriptableObject
{
    public Sprite weaponIcon;
    public Bullets bulletPrefab;
    public int damage;
    public int bonusDamage;
    public int finalDamage => damage + bonusDamage;
    public string weaponName;
    public float fireRate;
    public float fireRateBonusPercentage;
    public float finalFireRate => fireRate * (1f - fireRateBonusPercentage);
    public float range;
    public float speed;
    public int maxAmmo;
    public float reloadTime;
    public AudioClip shootSound;
    public AudioClip reloadSound;

    [Header("For Shotgun Only")]
    public AudioClip pumpSound;
    public float delay;

    public void AddBonusDamage(int amount)
    {
        bonusDamage += amount;
    }

    public void FireRateUp(float amount)
    {
        fireRateBonusPercentage += amount;

        if (fireRateBonusPercentage > 0.9f) fireRateBonusPercentage = 0.9f;
    }

    public void ResetWeaponData()
    {
        bonusDamage = 0;
        fireRateBonusPercentage = 0f;
    }
}
