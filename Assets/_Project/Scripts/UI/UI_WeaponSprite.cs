using UnityEngine;
using UnityEngine.UI;

public class UI_WeaponSprite : MonoBehaviour
{
    [SerializeField] private Image _weaponIcon;

    public void UpdateWeaponIcon(Sprite icon)
    {
        if (_weaponIcon == null) return;

        _weaponIcon.sprite = icon;
        _weaponIcon.preserveAspect = true;

        _weaponIcon.enabled = (icon != null);
    }
}
