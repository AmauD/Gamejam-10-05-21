using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 2;
    protected int currenHealth;

    protected virtual void Start()
    {
        currenHealth = maxHealth;
    }
    public bool IsAlive()
    {
        return (currenHealth > 0);
    }

    public void Damage(int damage)
    {
        currenHealth -= damage;
        if (currenHealth <= 0)
            Kill();
    }

    public virtual void Kill()
    {
        currenHealth = 0;
    }
}
