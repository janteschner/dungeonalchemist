using System;
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
    private int hp;

    [SerializeField] private GameObject enemyPrefab;
    
    public Animator Animator { get; private set; }

    private SpriteRenderer _spriteRenderer;
    
    public int Hp
    {
        get => hp;
        set
        {
            hp = value;
        }
    }
    
    public EnemyType CurrentEnemyType => _currentEnemyType;

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
        Animator = enemyPrefab.GetComponentInChildren<Animator>();
        _spriteRenderer = enemyPrefab.GetComponentInChildren<SpriteRenderer>();
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
        Hp = enemyType.maxHealth;
        // TODO: Sprite change for Enemy-type

        Debug.Log("Preparing enemy for combat! Enemy is " + enemyType.enemyName + " with " + Hp + " HP!");
    }

    public DamageNumberWithInfo CalculateDamage(Attack attack)
    {
        //First, determine if any modifiers apply
        int damage = attack.baseDamage;
        var isWeak = false;
        var isResistant = false;
        var isImmune = false;
        if (_currentEnemyType.IsImmune(attack.element))
        {
            Debug.Log("Enemy is immune to " + attack.element + " damage, so it does Zero damage!");
            damage = 0;
            isImmune = true;
        }
        else if (_currentEnemyType.IsResistant(attack.element))
        {
            Debug.Log("Enemy is resistant to " + attack.element + " damage, so it's halved!");
            damage = (damage / 2);
            isResistant = true;
        }
        else if (_currentEnemyType.IsWeak(attack.element))
        {
            Debug.Log("Enemy is weak to " + attack.element + " damage, so it's doubled!");
            damage = (damage * 2);
            isWeak = true;
        }
        else
        {
            Debug.Log("Enemy is neutral to " + attack.element + " damage");
        }

        return new DamageNumberWithInfo(damage, attack.element, isWeak, isResistant, isImmune);
    }
    
    public void TakeDamage(DamageNumberWithInfo damageInfo)
    {
        Debug.Log("Enemy took " + damageInfo.damage + " damage");
        DamageNumberSpawner.Instance.SpawnDamageNumber(damageInfo, true);
        NotebookScript.Instance.AddElement(_currentEnemyType, damageInfo.element);
        Hp -= damageInfo.damage;
    }

    public bool IsDead()
    {
        bool dead = Hp <= 0;
        Debug.Log("isDead Status: "+dead);
        if (dead)
        {
            NotebookScript.Instance.UnlockDetailedDescription(_currentEnemyType);
        }
        return dead;
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
}
