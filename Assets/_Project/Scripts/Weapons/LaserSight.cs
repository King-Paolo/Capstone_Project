using UnityEngine;

public class LaserSight : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private LayerMask _layersToHit;

    private SO_WeaponData _currentWeaponData;

    public void SetupLaser(SO_WeaponData data)
    {
        _currentWeaponData = data;
    }

    private void Update()
    {
        if (_currentWeaponData == null)
        {
            _lineRenderer.enabled = false;
            return;
        }

        _lineRenderer.enabled = true;
        DrawLaser();
    }

    private void DrawLaser()
    {
        _lineRenderer.positionCount = 2;

        Vector3 startPos = _firePoint.position;
        _lineRenderer.SetPosition(0, startPos);

        float range = _currentWeaponData.range;

        Vector3 forwardDirection = _firePoint.forward;

        forwardDirection.y = 0;

        Vector3 endPos = startPos + forwardDirection * range;

        if (Physics.Raycast(startPos, forwardDirection, out var hitInfo, range, _layersToHit))
        {
            endPos = hitInfo.point;
        }

        _lineRenderer.SetPosition(1, endPos);
    }
}
