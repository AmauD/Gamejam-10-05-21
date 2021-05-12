using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private int numberAlternateWeakpointDeath = 1;
    [SerializeField] private int numberAlternateNormalDeath = 1;

    private Enemy enemy = null;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        if (!enemy)
            enemy = GetComponentInParent<Enemy>();
    }

    [ContextMenu("Full Speed")]
    public void FullSpeed()
    {
        SetWalkingSpeed(1f);
    }

    public void StopWalk()
    {
        SetWalkingSpeed(0f);
    }

    public void SetWalkingSpeed(float speed)
    {
        if (animator != null)
        {
            animator.SetFloat("BlendWalk", speed);
        }
    }

    public void LaunchAttack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
            enemy.enemySounds.PlayAttackSound();
        }
    }

    public void AttackOver()
    {
        enemy.DamagePlayer();
        enemy.RemoveEnemy();
        Destroy(enemy.gameObject);
    }

    public void DeathOver()
    {
        Destroy(enemy.gameObject);
    }

    /// <summary>
    /// Lance l'animation de mort normale
    /// </summary>
    [ContextMenu("Normal Death")]
    public void NormalDeath()
    {
        Death();
    }

    /// <summary>
    /// Lance l'animation de mort par point faible
    /// </summary>
    [ContextMenu("Weakpoint Death")]
    public void WeakpointDeath()
    {
        Death(true);
    }

    public void Death(bool byWeakPoints = false)
    {
        enemy.RemoveEnemy();
        if(animator != null)
        {
            if(byWeakPoints)
            {
                animator.SetInteger("WeakpointDeathRandom", Random.Range(0, numberAlternateWeakpointDeath));
                animator.SetTrigger("WeakpointDeath");
            }
            else
            {
                animator.SetInteger("NormalDeathRandom", Random.Range(0, numberAlternateNormalDeath));
                animator.SetTrigger("NormalDeath");
            }
            enemy.enemySounds.PlayDeathSound();
        }
    }
}
