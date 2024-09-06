using System.Collections;
using System.Collections.Generic;
using Combat;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public Attack[] availableAttacks;

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
}
