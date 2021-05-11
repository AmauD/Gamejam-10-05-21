using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gun : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] public Transform canon;
    [SerializeField] private SpriteRenderer crosshair;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private AudioSource gunAudioSource;

    [SerializeField] private int damage = 1;
    [SerializeField] private int maxAmmo = 6;
    private int currentAmmo;

    [SerializeField] private float reloadTime = 2f;

    private Enemy currentTarget = null;
    private Tweener tween;
    private Coroutine pingPong = null;
    private Coroutine reloading = null;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    public bool HasValidTarget()
    {
        return currentTarget && currentTarget.IsAlive();
    }

    public void AssignTarget(Enemy enemyToTarget)
    {
        if (enemyToTarget && enemyToTarget.IsAlive())
        {
            currentTarget = enemyToTarget;
            pingPong = StartCoroutine(PingPong());
        }
    }

    public bool Reloading()
    {
        return (reloading != null);
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
        if(pingPong != null)
        {
            StopCoroutine(pingPong);
            tween.Kill();
        }

        currentAmmo--;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 250f, layerMask))
        {
            Enemy enemy = hitInfo.collider.GetComponentInParent<Enemy>();
            if (enemy)
            {
                if(hitInfo.collider.tag == "Weakpoint")
                {
                    enemy.Damage(9999, true);
                }
                else
                {
                    enemy.Damage(damage);
                }
            }
        }

        if (currentAmmo <= 0)
        {
            ReloadGun();
        }
        else
        {
            player.NextEnemy();
        }
    }

    private void ReloadGun()
    {
        reloading = StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        tween = transform.DOLocalMoveY(0f, 0.5f);
        yield return tween.WaitForCompletion();
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        tween = transform.DOLocalMoveY(1f, 0.5f);
        yield return tween.WaitForCompletion();
        player.TargetClosestEnemy();
        reloading = null;
    }

    IEnumerator PingPong()
    {
        if (currentTarget)
        {
            tween = transform.DOLookAt(currentTarget.gunTarget.position, 0.5f);

            yield return tween.WaitForCompletion();
            while (true && currentTarget)
            {
                tween = transform.DOLookAt(currentTarget.gunTarget.position, 0f);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    void Update()
    {
        if(reloading == null && player.IsAlive())
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, 250f, layerMask))
            {
                Debug.DrawLine(transform.position, hitInfo.point, Color.red);
                crosshair.transform.position = hitInfo.point;
                crosshair.color = Color.green;
            }
            else
            {
                Debug.DrawLine(transform.position, transform.forward * 200f + transform.position, Color.blue);
                if (currentTarget)
                    crosshair.transform.position = transform.forward * Vector3.Distance(currentTarget.transform.position, transform.position) + transform.position;
                crosshair.color = Color.red;
            }
        }
        else
        {
            crosshair.transform.localPosition = Vector3.zero;
        }
    }
}
