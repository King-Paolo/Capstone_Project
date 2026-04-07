using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 15f;
    [SerializeField] private Camera _camera;

    private Rigidbody _rb;
    private Vector3 _move;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _camera = Camera.main;
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
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _move * (_speed * Time.fixedDeltaTime));
    }
}
