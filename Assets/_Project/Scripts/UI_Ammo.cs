using TMPro;
using UnityEngine;

public class UI_Ammo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ammoText;

    public void UpdateAmmoGraphics(int current, int total)
    {
        _ammoText.text = $"{current} / {total}";
    }
}
