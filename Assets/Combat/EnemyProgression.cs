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
    public EnemyType dragon;
    
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
                return wizard;
            case 3:
                return dragon;
            default:
                return goblin;
        }
    }
}
