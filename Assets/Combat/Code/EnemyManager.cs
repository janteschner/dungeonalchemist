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

    public Animator Animator { get; private set; }
    public EnemyAnimationController Controller { get; private set; }

    public GameObject currentPrefab { get; private set; }

    public int Hp
    {
        get => hp;
        set
        {
            hp = value;
        }
    }
    
    public EnemyType CurrentEnemyType => _currentEnemyType;

    [SerializeField] private GameObject EnemyPrefab;
    private SpriteRenderer spriteRenderer;

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
        currentPrefab = EnemyPrefab;
        spriteRenderer = EnemyPrefab.GetComponentInChildren<SpriteRenderer>();
        Animator = EnemyPrefab.GetComponentInChildren<Animator>();
        Controller = EnemyPrefab.GetComponentInChildren<EnemyAnimationController>();
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
        currentPrefab = Instantiate(enemyType.prefab, new Vector3(4.5f, 0, -2), Quaternion.identity);

        spriteRenderer = currentPrefab.GetComponentInChildren<SpriteRenderer>();
        Animator = currentPrefab.GetComponentInChildren<Animator>();
        Controller = currentPrefab.GetComponentInChildren<EnemyAnimationController>();

        CombatManager.Instance.fxSpawner = currentPrefab.GetComponentInChildren<FXSpawner>();

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

}
