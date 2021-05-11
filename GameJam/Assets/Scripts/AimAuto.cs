using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AimAuto : MonoBehaviour
{
    #region Unity API

    private void Awake()
    {
        _playerTransform = transform;
    }


    private void Start()
    {
        if (!_crosshair) return;
        Sequence sequence = DOTween.Sequence().SetLoops(-1,LoopType.Restart);
        sequence.Append(_crosshair.rectTransform.DOPivotY(-2, 1f));
        sequence.Append(_crosshair.rectTransform.DOPivotY(2.5f, 1f));
        // _crosshair.rectTransform.DOPivotY(-5, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        FindNextClosestEnemy();
        SetCrosshair();
    }

    #endregion


    #region Utils

    private void FindNextClosestEnemy()
    {
        var minDistance = Mathf.Infinity;
        var enemyToTarget = Vector3.zero;

        foreach (var enemy in _enemySpawner.enemyList)
        {
            var distanceWithChampion = Vector3.Distance(_playerTransform.position, enemy.transform.position);
            if (distanceWithChampion < minDistance)
            {
                minDistance = distanceWithChampion;
                enemyToTarget = enemy.transform.position;
                _closestEnemy = enemy.transform;
                var direction = (enemyToTarget - _playerTransform.position).normalized;
                _playerTransform.DOLookAt(direction, 0.5f);
            }

            enemyToTarget.y = 0f;
            //var rotGoal = Quaternion.LookRotation(direction);
            //_playerTransform.rotation =
            //    Quaternion.Slerp(_playerTransform.rotation, rotGoal, _rotationSpeed * Time.deltaTime);
        }
    }

    private void SetCrosshair()
    {
        if (!_closestEnemy || !_crosshair) return;
        _crosshair.rectTransform.position = _camera.WorldToScreenPoint(_closestEnemy.position);
        _playerShooter.TargetPosition = _camera.ScreenToWorldPoint(_crosshair.rectTransform.position);
    }

    #endregion


    #region Private Members

    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private Image _crosshair;
    [SerializeField] private PlayerShooter _playerShooter;
    private Transform _playerTransform;
    private Transform _closestEnemy;
    [SerializeField] private Camera _camera;

    #endregion
}