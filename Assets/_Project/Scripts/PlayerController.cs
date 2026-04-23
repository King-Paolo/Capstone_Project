using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _firingSpeed = 2f;
    [SerializeField] private float _rotationSpeed = 15f;
    [SerializeField] private Camera _camera;
    [SerializeField] private Animator _animator;

    private Rigidbody _rb;
    private Vector3 _move;
    private Weapon _weapon;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _camera = Camera.main;
        _weapon = GetComponentInChildren<Weapon>();
    }

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float sqrtLenght = input.sqrMagnitude;

        if (sqrtLenght > 1)
            input /= Mathf.Sqrt(sqrtLenght);

        _move = new Vector3(input.x, 0, input.y);

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPoint = hit.point;

            Vector3 direction = targetPoint - transform.position;
            direction.y = 0;

            if (direction != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
            }
        }

        Vector3 localMove = transform.InverseTransformDirection(_move);

        _animator.SetFloat("Horizontal", localMove.x);
        _animator.SetFloat("Vertical", localMove.z);
    }

    private void FixedUpdate()
    {
        float speed;

        if (_weapon.IsAiming)
        {
            speed = _firingSpeed;
        }
        else
        {
            speed = _movementSpeed;
        }

        _rb.MovePosition(_rb.position + _move * (speed * Time.fixedDeltaTime));
    }
}
