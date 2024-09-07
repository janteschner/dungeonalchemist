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
        BeginNewCombat();
    }

    public void BeginNewCombat()
    {
        StartCoroutine(BeginAfter1Second());

    }

    public IEnumerator BeginAfter1Second()
    {
        yield return new WaitForSeconds(1);
        BeginCombat(firstEnemy);
    }

    public void BeginCombat(EnemyType enemyType)
    {
        _enemy.PrepareForCombat(enemyType);
        _player.SetStartingHp(startingHp);

        isCombatOver = false;
        isPlayerTurn = true;
        DisplayUIForPlayerTurn();
    }

    public void Combat()
    {
        if (_player.IsDead() || _enemy.IsDead()) return;
        Debug.Log("Combat cycle beginning!");
        if (isPlayerTurn)
        {
            //PlayerTurn();
            PlayerManager.Instance.Animator.SetTrigger("Move");
            

            isPlayerTurn = false;

        }
        else
        {
            //EnemyTurn();
            EnemyManager.Instance.Animator.SetTrigger("Move");
            
            
            isPlayerTurn = true;

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
        if (isCombatOver)
        {
            EndCombat();
        }
        Combat();
        
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
        Debug.Log("Enemy's turn! (Enemy is at " + _enemy.Hp + " HP)");
        var attack = _enemy.ChooseAttack();
        var damage = _player.CalculateDamage(attack);
        _player.TakeDamage(damage);

    }

    public void EndCombat()
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

        // Move Player
        PlayerManager.Instance.Animator.SetTrigger("Gameover");
        // Kill Enemy
        EnemyManager.Instance.Animator.SetTrigger("Die");

    }

    void PlayerDied()
    {
        Debug.Log("Player lost!");


        // Move Player
        PlayerManager.Instance.Animator.SetTrigger("Die");
        // Kill Enemy
        EnemyManager.Instance.Animator.SetTrigger("Gameover");

    }
}
