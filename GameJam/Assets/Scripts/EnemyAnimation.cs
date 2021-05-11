using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private int numberAlternateWeakpointDeath = 1;
    [SerializeField] private int numberAlternateNormalDeath = 1;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    [ContextMenu("Normal Death")]
    public void NormalDeath()
    {
        Death();
    }

    [ContextMenu("Weakpoint Death")]
    public void WeakpointDeath()
    {
        Death(true);
    }

    void Death(bool byWeakPoints = false)
    {
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
        }
    }
}
