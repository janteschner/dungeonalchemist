using System.Collections;
using System.Collections.Generic;
using Combat;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance { get; private set; }
    private PlayerManager _player;
    private EnemyManager _enemy;
    [SerializeField] private ActionButtonContainerScript _actionButtonContainerScript;
    [SerializeField] public Attack defaultAttack;
    [SerializeField] public EnemyType firstEnemy;
    [SerializeField] public int startingHp;

    public bool isPlayerTurn;
    public bool isEnemyTurn;
    public bool isCombatOver;
    
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
        isCombatOver = false;
        isPlayerTurn = true;
        DisplayUIForPlayerTurn();
    }

    void Combat()
    {
        if (isPlayerTurn)
        {
            //PlayerTurn();
            PlayerManager.Instance.Animator.SetTrigger("Move");
            if (_enemy.IsDead())
            {
                Debug.Log("The enemy died!");
                isCombatOver = true;
            }

            isPlayerTurn = false;
        }
        else
        {
            //EnemyTurn();
            EnemyManager.Instance.Animator.SetTrigger("Move");
            if (_player.IsDead())
            {
                Debug.Log("The player died!");
                isCombatOver = true;
            }

            isPlayerTurn = true;
            DisplayUIForPlayerTurn();
        }
    }

    public void DisplayUIForPlayerTurn()
    {
        Debug.Log("Creating UI for player's turn!");    
        //Create a new Card Selection UI from thje preset and add it to the ui canvas
        // var newUi = Instantiate(_attackSelectionUiPrefab, _uiCanvas.transform, false);
        // newUi.SetActive(true);
        _actionButtonContainerScript.AppearAnimation();
    }

    public void CombatCycle()
    {
        Combat();
        if (isCombatOver)
        {
            EndCombat();
        }

        Combat();
        if (isCombatOver)
        {
            EndCombat();
        }
    }

    public void PlayerTurn()
    {
        Debug.Log("Player's turn! (Player is at " + _player.hp + " HP)");
        var firstAttack = _player.FirstAttack;
        var secondAttack = _player.SecondAttack;
        
        // One big Attack
        if(firstAttack.element == Element.FIRE || firstAttack.element == Element.ICE || firstAttack.element == Element.VOLT)
        {
            // PlayerManager.Instance.Animator.SetTrigger("Move");
        }
        // Two attacks
        else
        {

        }
        var firstAttackDamage = _enemy.CalculateDamage(firstAttack);
        _enemy.TakeDamage(firstAttackDamage);
        var secondAttackDamage = _enemy.CalculateDamage(secondAttack);
        _enemy.TakeDamage(secondAttackDamage);
        
        _player.ResetChosenAttacks();
    }

    public void EnemyTurn()
    {
        Debug.Log("Enemy's turn! (Enemy is at " + _enemy.hp + " HP)");
        var attack = _enemy.ChooseAttack();
        var damage = _player.CalculateDamage(attack);
        _player.TakeDamage(damage);

    }

    void EndCombat()
    {
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

    void PlayerWonCombat()
    {
        Debug.Log("Player won combat!");

    }

    void PlayerDied()
    {
        Debug.Log("Player lost!");
        
    }
}
