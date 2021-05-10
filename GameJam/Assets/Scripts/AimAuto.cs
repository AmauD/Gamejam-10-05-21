using UnityEngine;

public class AimAuto : MonoBehaviour
{
    #region Public members

    #endregion


    #region Unity API

    private void Awake()
    {
        _playerTransform = transform;
    }
    
    private void Update()
    {
        FindClosestEnemy();
    }

    #endregion


    #region Utils

    private void FindClosestEnemy()
    {
        var minDistance = Mathf.Infinity;
        var enemyToTarget = Vector3.zero;

        foreach (var enemy in _enemySpawner.enemyList)
        {
            var distanceWithChampion = Vector3.Distance(_playerTransform.position, enemy.position);
            if (distanceWithChampion < minDistance)
            {
                minDistance = distanceWithChampion;
                enemyToTarget = enemy.position;
            }

            _playerTransform.LookAt(enemyToTarget);
        }
    }

    #endregion


    #region Private Members

    [SerializeField] private EnemySpawner _enemySpawner;
    private Transform _playerTransform;

    #endregion
}