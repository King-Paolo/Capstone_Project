using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "New Weapon")]
public class SO_WeaponData : ScriptableObject
{
    public string weaponName;
    public float fireRate;
    public float range;
    public int maxAmmo;
    public float reloadTime;
}
