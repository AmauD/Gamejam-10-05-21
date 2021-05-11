using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gun : MonoBehaviour
{
    [SerializeField] public Transform canon;
    [SerializeField] private SpriteRenderer crosshair;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private int maxAmmo = 6;
    private int currentAmmo;

    [SerializeField] private float reloadTime = 2f;
    private float currentReloadTime = 0f;

    private Enemy currentTarget = null;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    public bool HasTarget()
    {
        return currentTarget;
    }

    public void AssignTarget(Enemy enemyToTarget)
    {
        if (enemyToTarget && enemyToTarget.IsAlive())
        {
            currentTarget = enemyToTarget;
            StartCoroutine(PingPong());
        }
    }

    public bool Reloading()
    {
        return (currentReloadTime > 0f);
    }

    public bool TryFireGun()
    {
        if(currentAmmo > 0 && !Reloading())
        {
            FireGun();
            return true;
        }

        return false;
    }

    private void FireGun()
    {
        currentAmmo--;


        if (currentAmmo <= 0)
        {
            ReloadGun();
        }
    }

    private void ReloadGun()
    {
        currentReloadTime = reloadTime;
        currentAmmo = maxAmmo;
    }

    IEnumerator PingPong()
    {
        if(currentTarget)
        {
            Tween tween = transform.DOLookAt(currentTarget.gunTarget.position, 0.5f);

            yield return tween.WaitForCompletion();
            while (true && currentTarget)
            {
                tween = transform.DOLookAt(currentTarget.gunTarget.position, 0f);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 250f, layerMask))
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);
            crosshair.transform.position = hitInfo.point;
            crosshair.color = Color.green;
        }
        else
        {
            Debug.DrawLine(transform.position, transform.forward * 200f + transform.position, Color.blue);
            if(currentTarget)
                crosshair.transform.position = transform.forward * Vector3.Distance(currentTarget.transform.position, transform.position) + transform.position;
            crosshair.color = Color.red;
        }

        if (currentReloadTime > 0f)
        {
            currentReloadTime -= Time.deltaTime;
        }
    }
}
