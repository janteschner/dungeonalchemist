using System.Collections;
using System.Collections.Generic;
using Combat;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    private EnemyType _currentEnemyType;
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

    public Attack ChooseAttack()
    {
        var randomAttack = _currentEnemyType.GetRandomAttack();
        Debug.Log("Enemy chose attack " + randomAttack.attackName + " with " + randomAttack.baseDamage + " damage!");
        return randomAttack;
    }

    public void PrepareForCombat(EnemyType enemyType)
    {
        _currentEnemyType = enemyType;
        hp = enemyType.maxHealth;
        Debug.Log("Preparing enemy for combat! Enemy is " + enemyType.enemyName + " with " + hp + " HP!");
    }

    public int CalculateDamage(Attack attack)
    {
        //First, determine if any modifiers apply
        int damage = attack.baseDamage;
        if (_currentEnemyType.IsImmune(attack.element))
        {
            Debug.Log("Enemy is immune to " + attack.element + " damage, so it does Zero damage!");
            damage = 0;
        }
        else if (_currentEnemyType.IsResistant(attack.element))
        {
            Debug.Log("Enemy is resistant to " + attack.element + " damage, so it's halved!");
            damage = (damage / 2);
        }
        else if (_currentEnemyType.IsWeak(attack.element))
        {
            Debug.Log("Enemy is weak to " + attack.element + " damage, so it's doubled!");
            damage = (damage * 2);
        }
        else
        {
            Debug.Log("Enemy is neutral to " + attack.element + " damage");
        }
        return damage;
    }
    
    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy took " + damage + " damage");
        hp -= damage;
    }

    public bool IsDead()
    {
        return hp <= 0;
    }
}
