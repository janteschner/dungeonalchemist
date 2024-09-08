using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
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

        switch (EnemyManager.Instance.CurrentAttack.element)
        {
            case Element.UNTYPED:
                break;
            case Element.FIRE:
                ObjectPool.Instance.PlayFightFX(transform, Effects.FIREBALL);
                break;
            case Element.ICE:
                ObjectPool.Instance.PlayFightFX(transform, Effects.ICEBALL);
                break;
            case Element.VOLT:
                ObjectPool.Instance.PlayFightFX(transform, Effects.VOLTBALL);
                break;

        }

        // BulletProjectile.OnProjectileHit += CombatManager.Instance.PlayerTurn;

    }
}
