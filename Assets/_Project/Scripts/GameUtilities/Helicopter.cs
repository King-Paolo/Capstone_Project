using System.Collections;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timer;

    private Transform _player;
    private AudioSource _audioSource;
    private bool _canStart;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _audioSource = GetComponentInChildren<AudioSource>();
        StartCoroutine(WaitReinforcements());
    }

    private void Update()
    {
        if (_canStart)
        {
            Vector3 targetPosition = new Vector3(_player.position.x, transform.position.y, _player.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.Victory();
            _audioSource.Stop();
        }
    }

    public IEnumerator WaitReinforcements()
    {
        yield return new WaitForSeconds(_timer);

        _canStart = true;
    }
}
