using System.Collections;
using System.Collections.Generic;
using Combat;
using Combat.Player_Attacks.Combos;
using JetBrains.Annotations;
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
    [SerializeField] public FXSpawner fxSpawner;
    [SerializeField] public int startingHp;

    public bool isPlayerTurn;
    public bool isSecondPlayerAttack;
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

        BeginNewCombat();
    }

    public void BeginNewCombat()
    {
        StartCoroutine(BeginAfter1Second());
    }

    public IEnumerator BeginAfter1Second()
    {
        yield return new WaitForSeconds(1);
        BeginCombat(EnemyProgression.Instance.GetEnemyForCurrentLevel());
    }

    public void BeginCombat(EnemyType enemyType)
    {
        NotebookScript.Instance.AddEnemyIfNotPresent(enemyType);
        NotebookScript.Instance.SwitchToEnemy(enemyType);
        _enemy.PrepareForCombat(enemyType);
        PlayerManager.Instance.Animator.SetTrigger("Reset");
        EnemyManager.Instance.Animator.SetTrigger("Reset");


        NotebookShowHide.Instance.ShowFromCompletelyHidden();
        isCombatOver = false;
        isPlayerTurn = true;
        isSecondPlayerAttack = false;
        DisplayUIForPlayerTurn();
    }

    public void Combat()
    {
        if (_player.IsDead() || _enemy.IsDead()) return;
        Debug.Log("Combat cycle beginning!");
        var firstAttack = _player.FirstAttack;
        var secondAttack = _player.SecondAttack;
        var enemyAttack = _enemy.ChooseAttack();

        if (isPlayerTurn)
        {
            //PlayerTurn();
            if (isSecondPlayerAttack)
            {
                SecondPlayerTurn();
            }
            else
            {
                FirstPlayerTurn();
            }

        }
        else
        {
            // if ((int)enemyAttack.element <= 3 && _enemy.CurrentEnemyType.bCanShoot)
            // {
            //     EnemyManager.Instance.Animator.SetTrigger("Shoot");
            // }
            // else
            // {
                EnemyManager.Instance.Animator.SetTrigger("Move");
            // }

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


    // public void PlayerTurn()
    // {
    //     Debug.Log("Player's turn! (Player is at " + _player.hp + " HP)");
    //     var firstAttack = _player.FirstAttack;
    //     var secondAttack = _player.SecondAttack;
    //
    //     PlayFXOnHit(firstAttack, secondAttack);
    //
    //     var firstAttackDamage = _enemy.CalculateDamage(firstAttack);
    //     _enemy.TakeDamage(firstAttackDamage);
    //     var secondAttackDamage = _enemy.CalculateDamage(secondAttack);
    //     _enemy.TakeDamage(secondAttackDamage);
    //
    //     _player.ResetChosenAttacks();
    // }

    public void OnPlayerHitConnect()
    {
        if (_player.CurrentCombo != Combo.NONE)
        {
            Debug.Log("Player's Combo Connected!");
            var effectToPlay = GetFXOnHit(_player.FirstAttack.element);
            if (effectToPlay != null)
            {
                PlayFXOnHit(effectToPlay!.Value);
            }

            var comboDamage = _enemy.CalculateComboDamage(_player.CurrentCombo, _player.CurrentComboBaseDamage);
            _enemy.TakeDamage(comboDamage);
            _enemy.MaybeTakeFireDamage();

            if (_player.CurrentCombo == Combo.THORS_HAMMER)
            {
                _enemy.MaybeApplyStatus(StatusEffect.PARALYSIS, 0.75f);
            }
            if (_player.CurrentCombo == Combo.FLAME_BLADE)
            {
                _enemy.MaybeApplyStatus(StatusEffect.BURN, 0.75f);
            }
            if (_player.CurrentCombo == Combo.FIRE_ICE)
            {
                _enemy.MaybeApplyStatus(StatusEffect.FREEZE, 0.75f);
            }
            if (_player.CurrentCombo == Combo.ICE_FIRE)
            {
                _enemy.MaybeApplyStatus(StatusEffect.BURN, 0.75f);
            }

            _player.ResetChosenAttacks();
            _player.ResetCombo();
            isSecondPlayerAttack = false;
        }
        else
        {
            if (isSecondPlayerAttack)
            {
                Debug.Log("Player's second hit connected");
                var secondAttack = _player.SecondAttack;

                var effectToPlay = GetFXOnHit(secondAttack.element);
                if (effectToPlay != null)
                {
                    PlayFXOnHit(effectToPlay!.Value);
                }

                var secondAttackDamage = _enemy.CalculateDamage(secondAttack);
                _enemy.TakeDamage(secondAttackDamage);
                _enemy.MaybeTakeFireDamage();
                if (_player.SecondAttack.statusEffect != StatusEffect.NONE)
                {
                    _enemy.MaybeApplyStatus(_player.SecondAttack.statusEffect, _player.SecondAttack.statusEffectChance);
                }

                _player.ResetChosenAttacks();
                isSecondPlayerAttack = false;
            }
            else
            {
                Debug.Log("Player's first hit connected");
                var firstAttack = _player.FirstAttack;

                var effectToPlay = GetFXOnHit(firstAttack.element);
                if (effectToPlay != null)
                {
                    PlayFXOnHit(effectToPlay!.Value);
                }

                var firstAttackDamage = _enemy.CalculateDamage(firstAttack);
                _enemy.TakeDamage(firstAttackDamage);
                _enemy.MaybeTakeFireDamage();

                if (_player.FirstAttack.statusEffect != StatusEffect.NONE)
                {
                    _enemy.MaybeApplyStatus(_player.FirstAttack.statusEffect, _player.FirstAttack.statusEffectChance);
                }
                isSecondPlayerAttack = true;
            }
        }
    }

    public bool MaybeComboTurn()
    {
        var firstAttack = _player.FirstAttack;
        var secondAttack = _player.SecondAttack;
        if (firstAttack.element == Element.ICE && secondAttack.element == Element.BASH)
        {
            _player.SetCombo(Combo.ICE_HAMMER, firstAttack, secondAttack);
            return true;
        }
        if (firstAttack.element == Element.FIRE && secondAttack.element == Element.SLASH)
        {
            _player.SetCombo(Combo.FLAME_BLADE, firstAttack, secondAttack);
            return true;
        }
        if (firstAttack.element == Element.VOLT && secondAttack.element == Element.SLASH)
        {
            _player.SetCombo(Combo.VOLT_BLADE, firstAttack, secondAttack);
            return true;
        }
        if (firstAttack.element == Element.FIRE && secondAttack.element == Element.ICE)
        {
            _player.SetCombo(Combo.FIRE_ICE, firstAttack, secondAttack);
            return true;
        }
        if (firstAttack.element == Element.ICE && secondAttack.element == Element.FIRE)
        {
            _player.SetCombo(Combo.ICE_FIRE, firstAttack, secondAttack);
            return true;
        }
        if (firstAttack.element == Element.BASH && secondAttack.element == Element.VOLT)
        {
            _player.SetCombo(Combo.THORS_HAMMER, firstAttack, secondAttack);
            return true;
        }
        return false;
    }

    public void FirstPlayerTurn()
    {
        var usedCombo = MaybeComboTurn();

        if (usedCombo)
        {
            ComboTurn();
            return;
        }
        
        Debug.Log("First Player turn and the selected attacks are " + _player.FirstAttack.attackName + " and " + _player.SecondAttack.attackName);
        isPlayerTurn = true;

        // Elemental Attacks are Shoot
        if (false)
        {
            PlayerManager.Instance.Animator.SetTrigger("Shoot");
        }
        else 
        {
            PlayerManager.Instance.Animator.SetTrigger("Move");
        }
    }

    public void ComboTurn()
    {
        Debug.Log("Combo being used! The combo is " + _player.CurrentCombo);
        isPlayerTurn = false;

        // // Elemental Attacks are Shoot
        // if (ElementFunctions.IsMagicalElement(_player.FirstAttack.element))
        // {
        //     PlayerManager.Instance.Animator.SetTrigger("Shoot");
        // }
        PlayerManager.Instance.Animator.SetTrigger("Move");
        
    }

    public void SecondPlayerTurn()
    {
        Debug.Log("Second Player turn and the selected attacks are " + _player.FirstAttack.attackName + " and " + _player.SecondAttack.attackName);
        isPlayerTurn = false;

        // Elemental attacks are Shoot
        if (false)
        {
            PlayerManager.Instance.Animator.SetTrigger("Shoot");
        }
        else
        {
            PlayerManager.Instance.Animator.SetTrigger("Move");
        }
    }

    public Effects? GetFXOnHit(Element element)
    {
        switch (element)
            {
                case Element.UNTYPED:
                    break;
                case Element.FIRE:
                    return Effects.FIRE;
                case Element.ICE:
                    return Effects.ICE;
                case Element.VOLT:
                    return Effects.VOLT;
                case Element.SLASH:
                    break;
                case Element.STAB:
                    break;
                case Element.BASH:
                    return Effects.BASH;
            }
        return null;
    }

    public Effects? GetFXOnHit(Element element, Element secondElement)
    {
        if (element == Element.SLASH && secondElement == Element.FIRE)
            return Effects.FIRESTORM;
        if (element == Element.BASH && secondElement == Element.ICE)
            return null;
        return null;
    }

    public void PlayFXOnHit(Effects effects)
    {
        fxSpawner.PlayFightFX(effects);
    }

    public void OnEnemyHitConnect()
    {
        var enemyAttack = _enemy.CurrentAttack;

        var effectToPlay = GetFXOnHit(enemyAttack.element);
        if (effectToPlay != null)
        {
            PlayFXOnHit(effectToPlay!.Value);
        }

        var damage = _player.CalculateDamage(enemyAttack);
        NotebookScript.Instance.AddAttack(_enemy.CurrentEnemyType, enemyAttack);
        _player.TakeDamage(damage);
    }


    public void EnemyTurn()
    {
        Debug.Log("Enemy's turn! (Enemy is at " + _enemy.Hp + " HP)");
        var attack = _enemy.CurrentAttack;
        var damage = _player.CalculateDamage(attack);
        NotebookScript.Instance.AddAttack(_enemy.CurrentEnemyType, attack);
        _player.TakeDamage(damage);
    }

    public void EnemySkipsTurnBecauseOfFreeze()
    {
        
    }

    public void EndCombat()
    {
        NotebookShowHide.Instance.CompletelyHideNotebook();
        Debug.Log("Combat is over! Checking who won...");
        if (_player.IsDead())
        {
            //PlayerManager.Instance
            PlayerDied();
        }
        else
        {
            PlayerWonCombat();
            _player.ResetChosenAttacks();
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
