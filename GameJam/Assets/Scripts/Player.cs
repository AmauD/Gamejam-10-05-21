using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : Entity
{
    [SerializeField] private Gun gun;

    public override void Kill()
    {
        base.Kill();
    }

    protected override void Start()
    {
        base.Start();
    }

    private void TryFireGun()
    {
        

    }

    void Update()
    {
        if (!gun.HasTarget())
            gun.AssignTarget(FindObjectOfType<Enemy>());
        if (Input.GetMouseButtonDown(0))
        {
            TryFireGun();
        }
    }
}
