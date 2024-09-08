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
        CombatManager.Instance.OnPlayerHitConnect();
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
        GameObject FXObject = null;
        switch (PlayerManager.Instance.FirstAttack.element)
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
        projectile.OnProjectileHit += CombatManager.Instance.OnPlayerHitConnect;
        projectile.OnProjectileHit += OnPlayerHitConnect;
    }

    public void OnPlayerHitConnect()
    {
        Debug.LogWarning("Player Bullet hast Hit Enemy");
    }
}
