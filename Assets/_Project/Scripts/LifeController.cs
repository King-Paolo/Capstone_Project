using System.Collections;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private int _maxHp;

    private bool _isDead;
    private Animator _animator;

    public bool IsDead { get { return _isDead; } }

    private void Awake()
    {
        _hp = _maxHp;
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        _isDead = false;

        if (CompareTag("Enemy"))
        {
            _hp = _maxHp;
        }
    }

    public void SetHp(int hp)
    {
        _hp = Mathf.Clamp(hp, 0, _maxHp);

        if (_hp <= 0 && !_isDead)
        {
            _isDead = true;
            _animator.SetTrigger("IsDead");

            if (CompareTag("Enemy"))
            StartCoroutine(DeathTimer());
        }
    }

    public void TakeDamage(int damage) => SetHp(_hp - damage);

    public IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(2f);

        if (CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }
}
