using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Combat;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public Attack[] availableAttacks;
    public Attack FirstAttack;
    public Attack SecondAttack;

    public Animator Animator { get; private set; }

    public int hp;
 
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else
        { 
            Instance = this; 
        }
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }


    public void AddAttack(Attack attack)
    {
        Debug.Log("Adding attack " + attack.attackName + " to player's available attacks!");
        availableAttacks.Append(attack);
    }
    

    public void ChoseAttackOne(Attack attack)
    {
        Debug.Log("Player chose attack " + attack.attackName + " with " + attack.baseDamage + " damage for first attack!");
        this.FirstAttack = attack;
    }
    
    public void DeselectAttackOne()
    {
        Debug.Log("Player deselected first attack!");
        this.FirstAttack = null;
    }
    

    public void ChoseAttackTwo(Attack attack)
    {
        Debug.Log("Player chose attack " + attack.attackName + " with " + attack.baseDamage + " damage for second attack!");
        this.SecondAttack = attack;
    }

    public void ResetChosenAttacks()
    {
        this.FirstAttack = null;
        this.SecondAttack = null;
    }


    public void SetStartingHp(int startingHp)
    {
        Debug.Log("Player starts the game with " + startingHp + " HP");
        hp = startingHp;
    }
    
    public int CalculateDamage(Attack attack)
    {
        return attack.baseDamage;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Player took " + damage + " damage!");
        hp -= damage;
    }

    public bool IsDead()
    {
        return hp <= 0;
    }

    public void OnAnimationAttack()
    {
        CombatManager.Instance.PlayerTurn();
        EnemyManager.Instance.Animator.SetTrigger("Hit");
    }

    public void OnAnimationFinished()
    {
        CombatManager.Instance.Combat();
    }

    public void OnAnimationGameOver()
    {
        // Open Menu

        // Reset Combat
        //CombatManager.Instance.BeginCombat();
    }
}
