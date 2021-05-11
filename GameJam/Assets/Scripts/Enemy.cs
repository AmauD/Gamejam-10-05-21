using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemySpawner enemySpawner = null;

    [SerializeField] private EnemyMove enemyMove = null;
    [SerializeField] private EnemyAnimation enemyAnimation = null;
    [SerializeField] private EnemySounds enemySounds = null;

    [SerializeField] private int maxHealth = 2;
    private int currenHealth;

    [SerializeField] private float speed = 2.5f;
    private bool moving = false;

    private Transform target;

    void Start()
    {
        currenHealth = maxHealth;
        target = GameObject.Find("Player").transform;
    }

    public void InitializeEnemy(EnemySpawner enemySpawner, Transform spawnLocation)
    {
        this.enemySpawner = enemySpawner;
        transform.position = spawnLocation.position;
        transform.rotation = spawnLocation.rotation;
        moving = true;
        enemyAnimation.FullSpeed();
    }

    public bool IsAlive()
    {
        return (currenHealth > 0);
    }

    public void DamageEnemy(int damage)
    {
        currenHealth -= damage;
        if (currenHealth <= 0)
            KillEnemy();
    }

    public void KillEnemy()
    {
        currenHealth = 0;
        moving = false;
        enemyAnimation.NormalDeath();
    }

    public void LaunchAttack()
    {
        moving = false;
        enemyAnimation.StopWalk();
    }

    private void MoveToPlayer()
    {
        transform.LookAt(target.position);
        transform.position += Time.deltaTime * speed * transform.forward;

        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            LaunchAttack();
        }
    }

    private void Update()
    {
        if (moving)
            MoveToPlayer();
    }
}
