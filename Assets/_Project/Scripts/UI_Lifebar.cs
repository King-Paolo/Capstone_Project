using UnityEngine;
using UnityEngine.UI;

public class UI_Lifebar : MonoBehaviour
{
    [SerializeField] private Image _fillableLifebar;

    public void UpdateGraphics(int hp, int maxHp)
    {
        _fillableLifebar.fillAmount = (float)hp / maxHp;
    }
}
