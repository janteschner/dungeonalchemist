using System.Collections;
using System.Collections.Generic;
using Combat;
using UnityEngine;

public class DamageNumberSpawner : MonoBehaviour
{
    public static DamageNumberSpawner Instance { get; private set; }

    
    [SerializeField] private GameObject damageNumberPrefab;
    [SerializeField] private Transform enemyNumberSpawnPoint;
    [SerializeField] private Transform playerNumberSpawnPoint;
    
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
        damageNumberPrefab.SetActive(false);
    }
    

    public void SpawnDamageNumber(DamageNumberWithInfo damageInfo, bool isEnemy)
    {
        //instantiate a new damage number
        var newDamageNumber = Instantiate(damageNumberPrefab, isEnemy ? enemyNumberSpawnPoint : playerNumberSpawnPoint, false);
        newDamageNumber.SetActive(true);
        
        var script = newDamageNumber.GetComponent<DamageNumberScript>();
        script.SetInfo(damageInfo);
        //offset the damage number by a random amount in x and y direction
        var maxVariation = 150f;
        newDamageNumber.transform.position += new Vector3(Random.Range(-maxVariation, maxVariation), Random.Range(-maxVariation, maxVariation) * 1.3f, 0);
    }
}
