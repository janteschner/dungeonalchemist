using System;
using System.Collections;
using System.Collections.Generic;
using Combat;
using Combat.Player_Attacks.Combos;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    private EnemyType _currentEnemyType;
    public StatusEffect CurrentStatusEffect = StatusEffect.NONE;

    public Attack CurrentAttack;
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
        CurrentAttack = _currentEnemyType.GetRandomAttack();
        Debug.Log("Enemy chose attack " + CurrentAttack.attackName + " with " + CurrentAttack.baseDamage + " damage!");
        return CurrentAttack;
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

        HealthbarManager.Instance.SetEnemyType();

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
        
        if(CurrentStatusEffect == StatusEffect.FREEZE && attack.element == Element.BASH)
        {
            Debug.Log("Enemy is frozen, so it takes double damage from hammer attacks!");
            damage = damage * 2;
        }

        return new DamageNumberWithInfo(damage, attack.element, isWeak, isResistant, isImmune);
    }

    public DamageNumberWithInfo CalculateComboDamage(Combo combo, int baseDamage)
    {
        var element = ElementFunctions.GetComboElement(combo);
        
        int damage = baseDamage;
        var isWeak = false;
        var isResistant = false;
        var isImmune = false;
        if (_currentEnemyType.IsImmune(element))
        {
            isImmune = true;
        }
        else if (_currentEnemyType.IsResistant(element))
        {
            isResistant = true;
        }
        else if (_currentEnemyType.IsWeak(element))
        {
            isWeak = true;
        }

        if (combo == Combo.ICE_HAMMER && !(_currentEnemyType.IsImmune(Element.ICE) || _currentEnemyType.IsResistant(Element.ICE)))
        {
            isWeak = true;
            isImmune = false;
            isResistant = false;
        }
        if (combo == Combo.FLAME_BLADE && (_currentEnemyType.enemyName == "Goblin"))
        {
            isWeak = true;
            isImmune = false;
            isResistant = false;
        }
        if (combo == Combo.VOLT_BLADE && (_currentEnemyType.enemyName == "Goblin"))
        {
            isWeak = true;
            isImmune = false;
            isResistant = false;
        }
        

        if (isImmune)
        {
            damage = 0;
        }
        if (isResistant)
        {
            damage = damage / 2;
        }
        if (isWeak)
        {
            damage = damage * 2;
        }

        return new DamageNumberWithInfo(damage, element, isWeak, isResistant, isImmune);
    }
    
    public void TakeDamage(DamageNumberWithInfo damageInfo)
    {
        Debug.Log("Enemy took " + damageInfo.damage + " damage");
        DamageNumberSpawner.Instance.SpawnDamageNumber(damageInfo, true);
        NotebookScript.Instance.AddElement(_currentEnemyType, damageInfo.element);
        Hp -= damageInfo.damage;
        HealthbarManager.Instance.SetEnemyHP(hp);
        HealFreeze();
    }

    public void MaybeTakeFireDamage()
    {
        if (CurrentStatusEffect == StatusEffect.BURN)
        {
            var fireDamage = 1;
            if (_currentEnemyType.IsWeak(Element.FIRE))
            {
                fireDamage *= 2;
            }
            Debug.Log("Enemy took " + fireDamage + " damage");
            DamageNumberSpawner.Instance.SpawnDamageNumber(new DamageNumberWithInfo(fireDamage, Element.FIRE, fireDamage == 2, false, false), true);
            Hp -= fireDamage;
            //40% chance to remove fire
            if (Random.Range(0f, 1f) < 0.4f)
            {
                RemoveStatus();
            }
        }
    }

    public void RemoveStatus()
    {
        CurrentStatusEffect = StatusEffect.NONE;
        currentPrefab.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        Debug.Log("Enemy status effect removed!");
    }

    public void HealParalysis()
    {
        if(CurrentStatusEffect == StatusEffect.PARALYSIS)
        {
            RemoveStatus();
        }
    }
    
    public void HealFreeze()
    {
        if(CurrentStatusEffect == StatusEffect.FREEZE)
        {
            RemoveStatus();
        }
    }

    public void MaybeApplyStatus(StatusEffect statusEffect, float chance)
    {
        if (statusEffect == StatusEffect.BURN && _currentEnemyType.IsImmune(Element.FIRE)) return;
        if (statusEffect == StatusEffect.FREEZE && _currentEnemyType.IsImmune(Element.ICE)) return;
        if (statusEffect == StatusEffect.PARALYSIS && _currentEnemyType.IsImmune(Element.VOLT)) return;
        
        if(Random.Range(0f, 1f) < chance)
        {
            CurrentStatusEffect = statusEffect;
            currentPrefab.GetComponentInChildren<SpriteRenderer>().color = StatusEffectFunctions.GetStatusColor(statusEffect);

            Debug.Log("Enemy gained status effect " + statusEffect);
        }
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
