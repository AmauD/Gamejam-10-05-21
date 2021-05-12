using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using TMPro;

public class Player : Entity
{
    [SerializeField] private Gun gun;
    private EnemySpawner enemySpawner;
    [SerializeField] private Enemy[] sortedEnemies;
    private int currentEnemy;

    private static Player instance;

    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }

            return instance;
        }
    }

    protected override void Start()
    {
        base.Start();
        enemySpawner = EnemySpawner.Instance;
    }

    private void TryFireGun()
    {
        if (gun.TryFireGun())
        {
            //NextEnemy();
        }
    }

    public void GetEnemiesSorted()
    {
        sortedEnemies = enemySpawner.enemyList.OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).ToArray();
    }

    public void NextEnemy()
    {
        if (currentEnemy < sortedEnemies.Length)
        {
            gun.AssignTarget(sortedEnemies[currentEnemy++]);
        }
        else
        {
            TargetClosestEnemy();
        }
    }

    public void RemoveTarget(Enemy enemyToRemove)
    {
        if (gun.RemoveTarget(enemyToRemove))
        {
            NextEnemy();
        }
    }

    public void TargetClosestEnemy()
    {
        GetEnemiesSorted();
        currentEnemy = 0;
        if (sortedEnemies.Length > 0)
            gun.AssignTarget(sortedEnemies[currentEnemy++]);
    }

    public override void Kill()
    {
        base.Kill();
        gun.AssignTarget(null);
    }

    void Update()
    {
        if (IsAlive())
        {
            GetEnemiesSorted();
            if (!gun.HasValidTarget())
                NextEnemy();
            if (Input.GetMouseButtonDown(0))
            {
                TryFireGun();
            }
        }
    }
}
