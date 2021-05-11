using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    #region Public Members

    public List<Transform> spawnPositionList;
    public List<Enemy> enemyList;

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
        Enemy enemy = Instantiate(_enemyPrefab, new Vector3(0f, -100f, 0f), Quaternion.identity).GetComponent<Enemy>();
        if(enemy)
        {
            enemy.InitializeEnemy(this, spawnPositionList[Random.Range(0, spawnPositionList.Count)]);
            enemyList.Add(enemy);
        }
    }

    public void UnlistEnemy(Enemy enemyToUnspawn)
    {
        enemyList.Remove(enemyToUnspawn);
    }

    #endregion


    #region Private Members

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _spawnPeriod;
    [SerializeField] private float _nextSpawnTime;
    #endregion
}