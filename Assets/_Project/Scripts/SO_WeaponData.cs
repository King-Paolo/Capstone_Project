using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "New Weapon")]
public class SO_WeaponData : ScriptableObject
{
    public Bullets bulletPrefab;
    public int damage;
    public string weaponName;
    public float fireRate;
    public float range;
    public float speed;
    public int maxAmmo;
    public float reloadTime;
}
