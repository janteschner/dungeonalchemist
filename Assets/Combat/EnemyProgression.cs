using System.Collections;
using System.Collections.Generic;
using Combat;
using UnityEngine;

public class EnemyProgression : MonoBehaviour
{
    public static EnemyProgression Instance { get; private set; }
    
    public int currentLevel = 0;

    public EnemyType goblin;
    public EnemyType skeleton;
    public EnemyType wizard;
    public EnemyType fireWizard;
    public EnemyType iceWizard;
    public EnemyType voltWizard;
    public EnemyType dragon;
    public EnemyType fireDragon;
    public EnemyType iceDragon;
    public EnemyType voltDragon;

    
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

    public void Start()
    {
    }

    public void AdvanceLevel()
    {
        currentLevel += 1;
        if(currentLevel >= 4)
        {
            Debug.Log("You win!");
        }
    }

    public EnemyType GetEnemyForCurrentLevel()
    {
        if(currentLevel >= 4)
        {
            StartGame_Tween.Instance.CloseMouth(false);
            Debug.Log("You win!");
            return goblin;
        }
        switch(currentLevel)
        {
            case 0:
                return goblin;
            case 1:
                return skeleton;
            case 2:
            {
                var randomWizard = Random.Range(0, 3);
                switch(randomWizard)
                {
                    case 0:
                        return fireWizard;
                    case 1:
                        return iceWizard;
                    case 2:
                        return voltWizard;
                    default:
                        return wizard;
                }
            }
            case 3:
            {
                var randomDragon = Random.Range(0, 3);
                switch(randomDragon)
                {
                    case 0:
                        return fireDragon;
                    case 1:
                        return iceDragon;
                    case 2:
                        return voltDragon;
                    default:
                        return dragon;
                }
            }
            default:
                return goblin;
        }
    }
}
