using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : Entity
{
    private EnemySpawner enemySpawner = null;

    [SerializeField] private EnemyMove enemyMove = null;
    [SerializeField] private EnemyAnimation enemyAnimation = null;
    [SerializeField] public EnemySounds enemySounds = null;

    public Transform gunTarget;
    public Transform overTarget;
    public Transform underTarget;

    [SerializeField] private float speed = 2.5f;
    private bool moving = false;

    [SerializeField] private int damage = 1;
    private Player target;

    protected override void Start()
    {
        base.Start();
        target = FindObjectOfType<Player>();
        StartCoroutine(PingPong());
    }

    public void InitializeEnemy(EnemySpawner enemySpawner, Transform spawnLocation)
    {
        this.enemySpawner = enemySpawner;
        transform.position = spawnLocation.position;
        transform.rotation = spawnLocation.rotation;
        moving = true;
        enemyAnimation.FullSpeed();
    }

    public void RemoveEnemy()
    {
        enemySpawner.UnlistEnemy(this);
    }

    public void LaunchAttack()
    {
        moving = false;
        enemyAnimation.StopWalk();
        enemyAnimation.LaunchAttack();
    }

    private void MoveToPlayer()
    {
        transform.LookAt(target.transform.position);
        transform.position += Time.deltaTime * speed * transform.forward;

        if (Vector3.Distance(transform.position, target.transform.position) < 4f)
        {
            LaunchAttack();
        }
    }

    public void Damage(int damage, bool weakpoint = false)
    {
        currenHealth -= damage;
        if (currenHealth <= 0)
        {
            Kill();
            enemyAnimation.Death(weakpoint);
        }
    }

    public void DamagePlayer()
    {
        target.Damage(damage);
    }

    public override void Kill()
    {
        base.Kill();
        RemoveEnemy();
        moving = false;
    }

    IEnumerator PingPong()
    {
        while (IsAlive())
        {

            Tween tween = gunTarget.DOLocalMove(overTarget.localPosition, 0.5f);
            yield return tween.WaitForCompletion();
            tween = gunTarget.DOLocalMove(underTarget.localPosition, 0.5f);
            yield return tween.WaitForCompletion();
        }
    }

    private void Update()
    {
        if (moving)
            MoveToPlayer();
    }
}
