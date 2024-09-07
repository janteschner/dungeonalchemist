using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Combat;
using TMPro;
using UI.UI_Scripts.Notebook;
using UnityEngine;
using UnityEngine.UI;

public class NotebookScript : MonoBehaviour
{
    
    public static NotebookScript Instance { get; private set; }

    [SerializeField] private Sprite normalEffectivity;
    [SerializeField] private Sprite weakEffectivity;
    [SerializeField] private Sprite resistantEffectivity;
    [SerializeField] private Sprite immuneEffectivity;
    
    [SerializeField] private Image slashAffinity;
    [SerializeField] private Image stabAffinity;
    [SerializeField] private Image bashAffinity;
    [SerializeField] private Image fireAffinity;
    [SerializeField] private Image iceAffinity;
    [SerializeField] private Image voltAffinity;

    [SerializeField] private TMP_Text enemyName;
    [SerializeField] private TMP_Text enemyText;

    
    private Dictionary<EnemyType, NotebookEntry> _enemyDictionary = new Dictionary<EnemyType, NotebookEntry>();
    
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

    public void AddEnemyIfNotPresent(EnemyType enemyType)
    {
        if(_enemyDictionary.ContainsKey(enemyType)) return;
        _enemyDictionary.Add(enemyType, new NotebookEntry());
        UnlockBasicDescription(enemyType);
    }

    public void SwitchToEnemy(EnemyType enemyType)
    {
        UpdateDisplayedInfo(enemyType);
    }
    
    public void AddAttack(EnemyType enemyType, Attack attack)
    {
        if (!_enemyDictionary.ContainsKey(enemyType))
        {
            _enemyDictionary.Add(enemyType, new NotebookEntry());
            _enemyDictionary[enemyType].AddAttack(attack);
        }
        _enemyDictionary[enemyType].AddAttack(attack);
        UpdateDisplayedInfo(enemyType);
    }
    
    public void AddElement(EnemyType enemyType, Element element)
    {
        if (!_enemyDictionary.ContainsKey(enemyType))
        {
            _enemyDictionary.Add(enemyType, new NotebookEntry());
            _enemyDictionary[enemyType].AddElement(element);
        }
        _enemyDictionary[enemyType].AddElement(element);
        UpdateDisplayedInfo(enemyType);
    }
    
    public void UnlockBasicDescription(EnemyType enemyType)
    {
        if (!_enemyDictionary.ContainsKey(enemyType))
        {
            _enemyDictionary.Add(enemyType, new NotebookEntry());
            _enemyDictionary[enemyType].UnlockBasicDescription();
        }
        _enemyDictionary[enemyType].UnlockBasicDescription();
        UpdateDisplayedInfo(enemyType);
    }
    
    public void UnlockDetailedDescription(EnemyType enemyType)
    {
        if (!_enemyDictionary.ContainsKey(enemyType))
        {
            _enemyDictionary.Add(enemyType, new NotebookEntry());
            _enemyDictionary[enemyType].UnlockDetailedDescription();
        }
        _enemyDictionary[enemyType].UnlockDetailedDescription();
        UpdateDisplayedInfo(enemyType);
    }

    public void UpdateDisplayedInfo(EnemyType enemyType)
    {
        var entry = _enemyDictionary[enemyType];
        enemyName.text = enemyType.enemyName;
        UpdateAffinity(Element.SLASH, slashAffinity, entry, enemyType);
        UpdateAffinity(Element.STAB, stabAffinity, entry, enemyType);
        UpdateAffinity(Element.BASH, bashAffinity, entry, enemyType);
        UpdateAffinity(Element.FIRE, fireAffinity, entry, enemyType);
        UpdateAffinity(Element.ICE, iceAffinity, entry, enemyType);
        UpdateAffinity(Element.VOLT, voltAffinity, entry, enemyType);
        var description = ComputeDescription(enemyType);
        enemyText.text = description;
    }

    public void UpdateAffinity(Element element, Image uiImage, NotebookEntry entry, EnemyType enemyType)
    {
        if(entry.attemptedElements.Contains(element))
        {
            uiImage.enabled = true;
            if (enemyType.weaknesses.Contains(element))
            {
                uiImage.sprite = weakEffectivity;
                return;
            }
            if (enemyType.immunities.Contains(element))
            {
                uiImage.sprite = immuneEffectivity;
                return;
            }
            if (enemyType.resistances.Contains(element))
            {
                uiImage.sprite = resistantEffectivity;
                return;
            }
            uiImage.sprite = normalEffectivity;
        }
        else
        {
            uiImage.enabled = false;
        }
    }
    
    private string ComputeDescription(EnemyType enemyType)
    {
        string text = "";
        var entry = _enemyDictionary[enemyType];
        if(entry.unlockedBasicDescription)
        {
            text += enemyType.descriptionBasic;
            text += "\n";
        }

        if (entry.unlockedDetailedDescription)
        {
            text += enemyType.descriptionDetailed;
            text += "\n";
        }
        
        if(entry.seenAttacks.Count > 0)
        {
            text += "Attacks:\n";
            foreach (var attack in entry.seenAttacks)
            {
                text += "-" + attack.attackName + ": " + attack.attackDescription + "\n";
            }
        }

        return text;
    }
}
