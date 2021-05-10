using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    #region Public Members

    public List<Transform> enemyList;

    #endregion


    #region Unity API

    private void Start()
    {
        _nextSpawnTime = 0f;
    }

    private void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            SpawnEnemy();
            _nextSpawnTime = Time.time + _spawnPeriod;
        }
    }

    #endregion


    #region Utils

    private void SpawnEnemy()
    {
        Vector3 randomPosition = spawnPositionList[Random.Range(0, spawnPositionList.Count)].position + new Vector3(0, 1, 0);
        GameObject enemy = Instantiate(_enemyPrefab, randomPosition, Quaternion.identity);
        enemyList.Add(enemy.transform);
    }

    #endregion


    #region Private Members

    [SerializeField] private GameObject _enemyPrefab;
    public List<Transform> spawnPositionList;
    [SerializeField] private float _spawnPeriod;
    [SerializeField] private float _nextSpawnTime;


    // [SerializeField] private Bounds _terrainBounds;

    #endregion
}