using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    #region Public

    public Vector3 TargetPosition
    {
        get => _targetPosition;
        set => _targetPosition = value;
    }

    #endregion


    #region Unity API

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    #endregion


    #region Utils

    private void Fire()
    {
        Ray ray = new Ray(_camera.transform.position, _targetPosition - _camera.transform.position);


        Debug.DrawLine(_camera.transform.position, _targetPosition, Color.blue, 1f);


        if (Physics.Raycast(ray, out _hitInfo))
        {
            Debug.DrawLine(_gunTip.position, _hitInfo.point, Color.red,1f);
            var hitObject = _hitInfo.collider.gameObject;
            _enemySpawner.enemyList.Remove(_hitInfo.transform);
            Destroy(hitObject, 0.5f);
        }
    }

    #endregion


    #region Private Members

    private RaycastHit _hitInfo;

    [SerializeField] private Transform _gunTip;
    private Vector3 _targetPosition;
    [SerializeField] private Camera _camera;
    [SerializeField] private EnemySpawner _enemySpawner;

    #endregion
}