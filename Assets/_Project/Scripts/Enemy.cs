using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _damage;

    private Transform _target;
    private NavMeshAgent _agent;
    private LifeController _lifeController;
    private bool _isAttacking;
    private Animator _animator;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _lifeController = GetComponent<LifeController>();
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
        }
    }

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            _target = player.transform;
        }
    }

    private void Update()
    {
        if (!_agent.isOnNavMesh)
            return;

        if (_lifeController.IsDead || _target == null)
        {

            _agent.isStopped = true;
            _agent.enabled = false;
            return;
        }

        float distance = Vector3.Distance(transform.position, _target.position);

        if (distance <= _agent.stoppingDistance)
        {
            _agent.isStopped = true;
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
            LifeController player = other.GetComponent<LifeController>();

            if (player != null)
            {
                player.TakeDamage(_damage);

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
