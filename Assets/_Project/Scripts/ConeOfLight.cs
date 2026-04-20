using UnityEngine;

public class ConeOfLight : MonoBehaviour
{
    [SerializeField] private Transform _cone;
    [SerializeField] private LayerMask _layerMask;

    private float _maxRange = 10f;

    private void Update()
    {
        RaycastHit hit;
        float currentRange;

        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxRange, _layerMask))
        {
            currentRange = hit.distance;
        }
        else
        {
            currentRange = _maxRange;
        }

        float scaleZ = currentRange / _maxRange;

        Vector3 newScale = _cone.localScale;
        newScale.y = -scaleZ;
        _cone.localScale = newScale;
    }
}
