using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _damage;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _slowing = 0.5f;

    private Transform _target;
    private NavMeshAgent _agent;
    private LifeController _enemyLife;
    private bool _isAttacking;
    private Animator _animator;
    private LifeController _playerLife;
    private float _speed;
    private Coroutine _slowCoroutine;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyLife = GetComponent<LifeController>();
        _animator = GetComponentInChildren<Animator>();
        _speed = _agent.speed;
    }

    private void OnEnable()
    {
        if (_agent != null)
        {
            _agent.enabled = true;
            _agent.speed = _speed;
            _agent.Warp(transform.position);
            _agent.isStopped = false;
            _isAttacking = false;

            foreach (var col in GetComponentsInChildren<Collider>())
            {
                col.enabled = true;
            }
        }
    }

    private void OnDisable()
    {
        if (_slowCoroutine != null)
        {
            StopCoroutine(_slowCoroutine);
            _slowCoroutine = null;
        }

        if (_agent != null)
        {
            _agent.speed = _speed;
        }
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            _target = player.transform;
            _playerLife = player.GetComponent<LifeController>();
        }
    }

    private void Update()
    {
        if (_enemyLife.IsDead)
        {
            if (_agent.enabled)
            {
                _agent.isStopped = true;
                _agent.enabled = false;

                foreach (var col in GetComponentsInChildren<Collider>())
                {
                    col.enabled = false;
                }
            }
            return;
        }

        if (!_agent.enabled || !_agent.isOnNavMesh) return;

        float distance = Vector3.Distance(transform.position, _target.position);

        if (distance <= _agent.stoppingDistance)
        {
            _agent.isStopped = true;

            Vector3 direction = (_target.position - transform.position).normalized;
            direction.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);

            if (_playerLife != null && !_playerLife.IsDead)
            Attack();
        }
        else
        {
            _agent.isStopped = false;
            _agent.SetDestination(_target.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && _isAttacking)
        {
            if (_playerLife != null)
            {
                _playerLife.TakeDamage(_damage);

                _isAttacking = false;
            }
        }

        if (other.CompareTag("Bullet"))
        {
            if (_slowCoroutine != null) StopCoroutine(_slowCoroutine);
            _slowCoroutine = StartCoroutine(SlowRoutine());
        }

    }
    public void Attack()
    {
        _animator.SetTrigger("IsAttacking");
    }

    public void EnableDamage()
    {
        _isAttacking = true;
    }

    public void DisableDamage()
    {
        _isAttacking = false;
    }

    private IEnumerator SlowRoutine()
    {
        _agent.speed = _speed * _slowing;

        yield return new WaitForSeconds(0.5f);

        _agent.speed = _speed;
        _slowCoroutine = null;
    }
}
