using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] GameObject shootOrigin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Callback for Anim
    public void OnAnimationAttack()
    {
        CombatManager.Instance.EnemyTurn();
        PlayerManager.Instance.Animator.SetTrigger("Hit");
        if (PlayerManager.Instance.IsDead())
        {
            Debug.Log("The player died!");
            CombatManager.Instance.EndCombat();
        }
        else
        {
            CombatManager.Instance.DisplayUIForPlayerTurn();
        }
    }

    public void OnAnimationFinished()
    {
        CombatManager.Instance.Combat();
    }

    public void OnAnimationKilled()
    {
        Destroy(transform.parent.gameObject);
    }

    public void OnAnimationShooting()
    {
        {
            GameObject FXObject = null;
            switch (EnemyManager.Instance.CurrentAttack.element)
            {
                case Element.UNTYPED:
                    break;
                case Element.FIRE:
                    FXObject = ObjectPool.Instance.PlayFightFX(shootOrigin.transform, Effects.FIREBALL);
                    break;
                case Element.ICE:
                    FXObject = ObjectPool.Instance.PlayFightFX(shootOrigin.transform, Effects.ICEBALL);
                    break;
                case Element.VOLT:
                    FXObject = ObjectPool.Instance.PlayFightFX(shootOrigin.transform, Effects.VOLTBALL);
                    break;
            }

            BulletProjectile projectile = FXObject.GetComponent<BulletProjectile>();
            projectile.OnProjectileHit += CombatManager.Instance.OnEnemyHitConnect;
            projectile.OnProjectileHit += OnEnemyHitConnect;
        }

    }

    public void OnEnemyHitConnect()
    {
        Debug.LogWarning("Enemy Bullet hast Hit Player");
    }
}
