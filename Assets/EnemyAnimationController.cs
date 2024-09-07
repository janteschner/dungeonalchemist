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
}
