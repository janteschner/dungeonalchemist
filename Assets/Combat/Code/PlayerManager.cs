using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Combat;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    public List<Attack> availableAttacks = new List<Attack>();
    public Attack FirstAttack;
    public Attack SecondAttack;

    public Animator Animator { get; private set; }
    public PlayerAnimationController Controller { get; private set; }

    public int hp;
    [SerializeField] private GameObject PlayerPrefab;
 
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
        Animator = PlayerPrefab.GetComponentInChildren<Animator>();
        Controller = PlayerPrefab.GetComponentInChildren<PlayerAnimationController>();
        var randomAttacks = LevelUpScript.Instance.GetStartingAttacks();
        foreach (var attack in randomAttacks)
        {
            AddAttack(attack);
        }
    }


    public void AddAttack(Attack attack)
    {
        Debug.Log("Adding attack " + attack.attackName + " to player's available attacks!");
        availableAttacks.Add(attack);
    }

    public void UpgradeAttack(Attack newAttack)
    {
        //set previousAttack to the existing attack with the same element
        var previousAttack = availableAttacks.FirstOrDefault(a => a.element == newAttack.element);
        
        //replace the previous attack with the new one, keeping it at the same index
        var index = availableAttacks.IndexOf(previousAttack);
        availableAttacks[index] = newAttack;
        Debug.Log("Upgraded attack " + previousAttack.attackName + " to " + newAttack.attackName + "!");
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
    
    public DamageNumberWithInfo CalculateDamage(Attack attack)
    {
        return new DamageNumberWithInfo(attack.baseDamage, attack.element, false, false, false);
    }

    public void TakeDamage(DamageNumberWithInfo damageInfo)
    {
        Debug.Log("Player took " + damageInfo.damage + " damage!");
        DamageNumberSpawner.Instance.SpawnDamageNumber(damageInfo, false);
        hp -= damageInfo.damage;
    }
    
    public void Heal(int healing)
    {
        Debug.Log("Player healed " + healing + " damage!");
        hp += healing;
    }

    public bool IsDead()
    {
        return hp <= 0;
    }

}
