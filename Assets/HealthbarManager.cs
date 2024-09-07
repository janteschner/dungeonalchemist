using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthbarManager : MonoBehaviour
{
    [SerializeField] GameObject GoblinHPBar;
    [SerializeField] GameObject WizardHPBar;
    [SerializeField] GameObject SkeletonHPBar;
    [SerializeField] GameObject DragonHPBar;
    [SerializeField] GameObject PlayerHPBar;

    GameObject currentEnemyHPBar;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: Get Current Enemy Type
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetEnemyType()
    {

    }

    public void SetEnemyHP(int newHP)
    {

    }

    public void SetPlayerHP(int newHP)
    {

    }

    public void ChangeDragonType()
    {

    }
}
