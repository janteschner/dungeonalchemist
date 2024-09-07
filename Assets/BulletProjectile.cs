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
        PlayerManager.Instance.OnAnimationAttack();
    }

    public void OnPlayerShootingEnd()
    {
        PlayerManager.Instance.OnAnimationFinished();
        Destroy(gameObject);
    }

    public void OnEnemyShootBullet()
    {
        EnemyManager.Instance.OnAnimationAttack();
        Destroy(gameObject);

    }
}
