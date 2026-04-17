using UnityEngine;

public class Bullets : MonoBehaviour
{
    private int _damage;
    private Rigidbody _rb;
    private Vector3 _direction;
    private Vector3 _startPosition;
    private bool _isShooted;
    private float _rangeSqr;
    private float _speed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _isShooted = false;

        if (_rb != null)
        {
            _rb.velocity = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if (!_isShooted)
            return;

        _rb.MovePosition(_rb.position + _direction * (_speed * Time.fixedDeltaTime));

        float distanceSqrt = (transform.position - _startPosition).sqrMagnitude;

        if (distanceSqrt >= _rangeSqr)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            LifeController enemy = collision.gameObject.GetComponent<LifeController>();
            enemy.TakeDamage(_damage);
        }
    }
    public void Setup(Vector3 direction, float range, float speed, int damage)
    {
        _direction = direction.normalized;
        _speed = speed;
        _rangeSqr = range * range;
        _damage = damage;

        _startPosition = transform.position;
        transform.forward = _direction;
        _isShooted = true;
    }
}
