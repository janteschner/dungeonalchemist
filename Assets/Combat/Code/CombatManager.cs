using System.Collections;
using System.Collections.Generic;
using Combat;
using Unity.VisualScripting;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }
    private PlayerManager _player;
    private EnemyManager _enemy;
    [SerializeField] public Attack defaultAttack;
    [SerializeField] public EnemyType firstEnemy;
    [SerializeField] public int startingHp;

    public bool isPlayerTurn;
    
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
    
    // Start is called before the first frame update
    void Start()
    {
        _player = PlayerManager.Instance;
        _enemy = EnemyManager.Instance;

        _player.SetStartingHp(startingHp);
        BeginCombat(firstEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BeginCombat(EnemyType enemyType)
    {
        _enemy.PrepareForCombat(enemyType);
        var combatOver = false;
        isPlayerTurn = true;
        while (!combatOver)
        {
            if (isPlayerTurn)
            {
                BeginPlayerTurn();
                if (_enemy.IsDead())
                {
                    Debug.Log("The enemy died!");
                    combatOver = true;
                }
                isPlayerTurn = false;
            }
            else
            {
                BeginEnemyTurn();
                if (_player.IsDead())
                {
                    Debug.Log("The player died!");
                    combatOver = true;
                }
                isPlayerTurn = true;
            }
        }
        Debug.Log("Combat is over! Checking who won...");
        if (_player.IsDead())
        {
            PlayerDied();
        }
        else
        {
            PlayerWonCombat();
        }
    }

    void BeginPlayerTurn()
    {
        Debug.Log("Player's turn! (Player is at " + _player.hp + " HP)");
        var firstAttack = _player.ChooseFirstAttack();
        var secondAttack = _player.ChooseSecondAttack(firstAttack);
        
        var firstAttackDamage = _enemy.CalculateDamage(firstAttack);
        _enemy.TakeDamage(firstAttackDamage);
        var secondAttackDamage = _enemy.CalculateDamage(secondAttack);
        _enemy.TakeDamage(secondAttackDamage);
    }

    void BeginEnemyTurn()
    {
        Debug.Log("Enemy's turn! (Enemy is at " + _enemy.hp + " HP)");
        var attack = _enemy.ChooseAttack();
        var damage = _player.CalculateDamage(attack);
        _player.TakeDamage(damage);

    }

    void PlayerWonCombat()
    {
        Debug.Log("Player won combat!");

    }

    void PlayerDied()
    {
        Debug.Log("Player lost!");
        
    }
}
