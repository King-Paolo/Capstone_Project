using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _damage;

    private Transform _target;
    private NavMeshAgent _agent;
    private LifeController _lifeController;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _lifeController = GetComponent<LifeController>();
    }

    private void OnEnable()
    {
        if (_agent != null)
        {
            _agent.enabled = true;
            _agent.Warp(transform.position);
            _agent.isStopped = false;
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
            Attack(true);
        }
        else
        {
            _agent.isStopped = false;
            _agent.SetDestination(_target.position);
            Attack(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LifeController player = collision.gameObject.GetComponent<LifeController>();

            player.TakeDamage(_damage);
        }
    }
    public void Attack(bool inRange)
    {
        //aggiungere animation.SetBool(isInRange) per animazione di attacco
    }
}
