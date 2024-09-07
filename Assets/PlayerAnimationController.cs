using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] GameObject shootOrigin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnAnimationAttack()
    {
        CombatManager.Instance.PlayerTurn();
        EnemyManager.Instance.Animator.SetTrigger("Hit");
        if (EnemyManager.Instance.IsDead())
        {
            Debug.Log("The enemy died!");
            CombatManager.Instance.EndCombat();
        }
    }

    public void OnAnimationFinished()
    {
        CombatManager.Instance.Combat();
    }

    public void OnAnimationGameOver()
    {
        // Open Menu
        LevelUpScript.Instance.ShowRandomCards();

        // Reset Combat
        //CombatManager.Instance.BeginCombat();
    }

    public void OnAnimationShooting()
    {

        switch (PlayerManager.Instance.FirstAttack.element)
        {
            case Element.UNTYPED:
                break;
            case Element.FIRE:
                ObjectPool.Instance.PlayFightFX(shootOrigin.transform, Effects.FIREBALL);
                break;
            case Element.ICE:
                ObjectPool.Instance.PlayFightFX(shootOrigin.transform, Effects.ICEBALL);
                break;
            case Element.VOLT:
                ObjectPool.Instance.PlayFightFX(shootOrigin.transform, Effects.VOLTBALL);
                break;

        }

        BulletProjectile.OnProjectileHit += CombatManager.Instance.PlayerTurn;

    }
}
