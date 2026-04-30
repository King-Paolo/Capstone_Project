using System.Collections;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private int _maxHp;

    private bool _isDead;
    private Animator _animator;
    private Coroutine _deathCoroutine;

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

        if (_animator != null)
        {
            _animator.Rebind();
            _animator.Update(0f);
        }
    }

    private void OnDisable()
    {
        if (_deathCoroutine != null)
        {
            StopCoroutine(_deathCoroutine);
            _deathCoroutine = null;
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
            {
                if (_deathCoroutine != null) StopCoroutine(_deathCoroutine);
                _deathCoroutine = StartCoroutine(DeathTimer());
            }
        }
    }

    public void TakeDamage(int damage) => SetHp(_hp - damage);

    public IEnumerator DeathTimer()
    {
        FindObjectOfType<EnemySpawner>().EnemyDied();

        yield return new WaitForSeconds(5f);

        if (CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }
}
