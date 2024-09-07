using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject FireHitFX;
    [SerializeField] GameObject IceHitFX;
    [SerializeField] GameObject VoltHitFX;
    [SerializeField] GameObject BonkHitFX;

    [SerializeField] GameObject BurningFX;
    [SerializeField] GameObject FireStormFX;

    public static FXSpawner Instance = new FXSpawner();

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
    private void Start()
    {
        FireHitFX.GetComponent<ParticleSystem>().Stop();
        IceHitFX.GetComponent<ParticleSystem>().Stop();
        VoltHitFX.GetComponent<ParticleSystem>().Stop();
        BonkHitFX.GetComponent<ParticleSystem>().Stop();

        BurningFX.GetComponent<ParticleSystem>().Stop();
        BurningFX.GetComponent<ParticleSystem>().Stop();
    }

    public void PlayFightFX(Effects _element)
    {
        GameObject currentFX = null;

        switch (_element)
        {
            case Effects.UNTYPED:
                break;
            case Effects.FIRE:
                currentFX = FireHitFX;
                break;
            case Effects.ICE:
                currentFX = IceHitFX;
                break;
            case Effects.VOLT:
                currentFX = VoltHitFX;
                break;
            case Effects.SLASH:
                break;
            case Effects.STAB:
                break;
            case Effects.BASH:
                currentFX = BonkHitFX;
                break;
            case Effects.FLAMES:
                currentFX = BurningFX;
                break;
            case Effects.FIRESTORM:
                currentFX = FireStormFX;
                break;
        }

        currentFX.GetComponent<ParticleSystem>().Play();
    }
}