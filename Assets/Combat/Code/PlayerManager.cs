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

    public Attack ChooseFirstAttack()
    {
        if (availableAttacks.Length == 0)
        {
            return CombatManager.Instance.defaultAttack;
        }
        int r = Random.Range(0, availableAttacks.Length);
        var chosenAttack = availableAttacks[r];
        Debug.Log("Player chose attack " + chosenAttack.attackName + " with " + chosenAttack.baseDamage + " damage for first attack!");
        return chosenAttack;
    }

    public void ChoseAttackOne()
    {
        if (availableAttacks.Length == 0)
        {
            this.FirstAttack = CombatManager.Instance.defaultAttack;
        }
        int r = Random.Range(0, availableAttacks.Length);
        var chosenAttack = availableAttacks[r];
        Debug.Log("Player chose attack " + chosenAttack.attackName + " with " + chosenAttack.baseDamage + " damage for first attack!");
        this.FirstAttack = chosenAttack;
    }
    
    public Attack ChooseSecondAttack(Attack firstAttack)
    {
        if (availableAttacks.Length == 0)
        {
            return CombatManager.Instance.defaultAttack;
        }

        //Choose an attack from the array that is not firstAttack
        Attack secondAttack = firstAttack;
        while (secondAttack == firstAttack)
        {
            int r = Random.Range(0, availableAttacks.Length);
            secondAttack = availableAttacks[r];
        }
        Debug.Log("Player chose attack " + secondAttack.attackName + " with " + secondAttack.baseDamage + " damage for second attack!");
        return secondAttack;
    }

    public void ChoseAttackTwo()
    {
        if (availableAttacks.Length == 0)
        {
            this.SecondAttack = CombatManager.Instance.defaultAttack;
        }

        //Choose an attack from the array that is not firstAttack
        Attack secondAttack = this.FirstAttack;
        while (secondAttack == this.FirstAttack)
        {
            int r = Random.Range(0, availableAttacks.Length);
            secondAttack = availableAttacks[r];
        }
        Debug.Log("Player chose attack " + secondAttack.attackName + " with " + secondAttack.baseDamage + " damage for second attack!");
        this.SecondAttack = secondAttack;
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
    }
}
