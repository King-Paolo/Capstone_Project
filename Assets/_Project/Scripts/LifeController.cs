using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private int _maxHp;

    private void Awake()
    {
        _hp = _maxHp;
    }

    public void SetHp(int hp)
    {
        _hp = Mathf.Clamp(hp, 0, _maxHp);
    }

    public void TakeDamage(int damage) => SetHp (_hp -  damage);
}
