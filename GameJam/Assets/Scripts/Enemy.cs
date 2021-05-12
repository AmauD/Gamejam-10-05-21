using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : Entity
{
    private EnemySpawner enemySpawner = null;

    [SerializeField] private EnemyAnimation enemyAnimation = null;
    [SerializeField] public EnemySounds enemySounds = null;

    public Transform gunTarget;
    public Transform overTarget;
    public Transform underTarget;

    [SerializeField] private Material[] possibleMaterials;

    [SerializeField] private float speed = 2.5f;
    private bool moving = false;

    [SerializeField] private int damage = 1;
    private Player player;

    private Score score;

    protected override void Start()
    {
        base.Start();
        player = Player.Instance;
        score = Score.Instance;
        StartCoroutine(PingPong());
    }

    public void InitializeEnemy(Transform spawnLocation)
    {
        this.enemySpawner = EnemySpawner.Instance;
        if(possibleMaterials.Length > 0)
        {
            GetComponentInChildren<SkinnedMeshRenderer>().material = possibleMaterials[Random.Range(0, possibleMaterials.Length)];
        }
        transform.position = spawnLocation.position;
        transform.rotation = spawnLocation.rotation;
        moving = true;
        enemyAnimation.FullSpeed();
    }

    public void RemoveEnemy()
    {
        player.RemoveTarget(this);
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
        transform.LookAt(player.transform.position);
        transform.position += Time.deltaTime * speed * transform.forward;

        if (Vector3.Distance(transform.position, player.transform.position) < 4f)
        {
            LaunchAttack();
        }
    }

    public void Damage(int damage, bool weakpoint = false)
    {
        currenHealth -= damage;
        if (currenHealth <= 0)
        {
            score.PlayerScore(Vector3.Distance(this.transform.position, player.transform.position), weakpoint);
            Kill();
            enemyAnimation.Death(weakpoint);
        }
        else
        {
            StartCoroutine(DamageAnimation());

        }
    }

    IEnumerator DamageAnimation()
    {
        moving = false;
        enemyAnimation.StopWalk();
        yield return new WaitForSeconds(0.5f);
        if(IsAlive())
        {
            moving = true;
            enemyAnimation.FullSpeed();
        }
    }

    public void DamagePlayer()
    {
        player.Damage(damage);
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
        if (moving && player.IsAlive())
            MoveToPlayer();
    }
}
