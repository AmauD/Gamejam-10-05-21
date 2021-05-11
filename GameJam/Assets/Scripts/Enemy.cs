using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : Entity
{
    private EnemySpawner enemySpawner = null;

    [SerializeField] private EnemyMove enemyMove = null;
    [SerializeField] private EnemyAnimation enemyAnimation = null;
    [SerializeField] private EnemySounds enemySounds = null;

    public Transform gunTarget;
    public Transform overTarget;
    public Transform underTarget;

    [SerializeField] private float speed = 2.5f;
    private bool moving = false;

    private Transform target;

    protected override void Start()
    {
        base.Start();
        target = FindObjectOfType<Player>().transform;
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
        Destroy(this.gameObject);
    }

    public void LaunchAttack()
    {
        moving = false;
        enemyAnimation.StopWalk();
        enemyAnimation.LaunchAttack();
    }

    private void MoveToPlayer()
    {
        transform.LookAt(target.position);
        transform.position += Time.deltaTime * speed * transform.forward;

        if (Vector3.Distance(transform.position, target.position) < 2f)
        {
            LaunchAttack();
        }
    }

    public override void Kill()
    {
        base.Kill();
        moving = false;
        enemyAnimation.NormalDeath();
    }

    IEnumerator PingPong()
    {
        while (true)
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
