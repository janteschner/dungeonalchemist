using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public static event Action OnProjectileHit;

    public void OnProjectileFinished()
    {
        OnProjectileHit?.Invoke();
        Destroy(gameObject);
    }

    public void OnPlayerShootBullet()
    {
        PlayerManager.Instance.Controller.OnAnimationAttack();
    }

    public void OnPlayerShootingEnd()
    {
        PlayerManager.Instance.Controller.OnAnimationFinished();
        Destroy(gameObject);
    }

    public void OnEnemyShootBullet()
    {
        EnemyManager.Instance.Controller.OnAnimationAttack();
        Destroy(gameObject);

    }
}
