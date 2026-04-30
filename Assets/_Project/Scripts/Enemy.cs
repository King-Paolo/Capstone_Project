using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _damage;
    [SerializeField] private float _rotationSpeed = 5f;

    private Transform _target;
    private NavMeshAgent _agent;
    private LifeController _enemyLife;
    private bool _isAttacking;
    private Animator _animator;
    private LifeController _playerLife;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyLife = GetComponent<LifeController>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        if (_agent != null)
        {
            _agent.enabled = true;
            _agent.Warp(transform.position);
            _agent.isStopped = false;
            _isAttacking = false;

            foreach (var col in GetComponentsInChildren<Collider>())
            {
                col.enabled = true;
            }
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
}
