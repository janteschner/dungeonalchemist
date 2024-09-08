using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarManager : MonoBehaviour
{
    public static HealthbarManager Instance { get; private set; }
    
    [SerializeField] GameObject GoblinHPBar;
    [SerializeField] GameObject WizardHPBar;
    [SerializeField] GameObject SkeletonHPBar;
    [SerializeField] GameObject DragonHPBar;
    [SerializeField] GameObject PlayerHPBar;

    GameObject currentEnemyHPBar;
    private Slider currentEnemySlider;
    private Slider playerSlider;
    
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
        // TODO: Get Current Enemy Type
        playerSlider = PlayerHPBar.GetComponent<Slider>();
        
        GoblinHPBar.SetActive(false);
        WizardHPBar.SetActive(false);
        SkeletonHPBar.SetActive(false);
        DragonHPBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetEnemyType()
    {
        var enemyId = EnemyManager.Instance.CurrentEnemyType.id;
        if (enemyId.StartsWith("wizard"))
        {
            GoblinHPBar.SetActive(false);
            WizardHPBar.SetActive(true);
            SkeletonHPBar.SetActive(false);
            DragonHPBar.SetActive(false);
            
            currentEnemySlider = WizardHPBar.GetComponent<Slider>();
        }
        if (enemyId.StartsWith("dragon"))
        {
            GoblinHPBar.SetActive(false);
            WizardHPBar.SetActive(false);
            SkeletonHPBar.SetActive(false);
            DragonHPBar.SetActive(true);
            
            currentEnemySlider = DragonHPBar.GetComponent<Slider>();
        }
        if (enemyId == "skeleton")
        {
            GoblinHPBar.SetActive(false);
            WizardHPBar.SetActive(false);
            SkeletonHPBar.SetActive(true);
            DragonHPBar.SetActive(false);
            
            currentEnemySlider = SkeletonHPBar.GetComponent<Slider>();
        }
        if (enemyId == "goblin")
        {
            GoblinHPBar.SetActive(true);
            WizardHPBar.SetActive(false);
            SkeletonHPBar.SetActive(false);
            DragonHPBar.SetActive(false);
            
            currentEnemySlider = GoblinHPBar.GetComponent<Slider>();
        }
        currentEnemySlider.value = 1;
    }

    public void SetEnemyHP(int newHP)
    {
        var maxHp = EnemyManager.Instance.CurrentEnemyType.maxHealth;
        Debug.Log("Setting enemy slider to " + (float)newHP + " / " + (float)maxHp + " = " + (float)newHP / (float)maxHp);
        currentEnemySlider.value = (float)newHP / (float)maxHp;
    }

    public void SetPlayerHP(int newHP)
    {
        var maxHp = CombatManager.Instance.startingHp;
        playerSlider.value = (float)newHP / (float)maxHp;
    }
}
