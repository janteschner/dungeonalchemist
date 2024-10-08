using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> Particles;

    public static ObjectPool Instance { get; private set; }

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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject PlayFightFX(Transform _transform, Effects _element)
    {
        List<ParticleSystem> particles = new List<ParticleSystem>();
        GameObject particle = null;

        switch (_element)
        {
            case Effects.UNTYPED:
                break;
            case Effects.FIRE:
                particle = Instantiate(Particles[2], _transform);
                break;
            case Effects.ICE:
                particle = Instantiate(Particles[4], _transform);
                break;
            case Effects.VOLT:
                particle = Instantiate(Particles[5], _transform);
                break;
            case Effects.SLASH:
                break;
            case Effects.STAB:
                break;
            case Effects.BASH:
                particle = Instantiate(Particles[0], _transform);
                break;
            case Effects.FLAMES:
                particle = Instantiate(Particles[1], _transform);
                break;
            case Effects.FIRESTORM:
                particle = Instantiate(Particles[3], _transform);
                break;
            case Effects.FIREBALL:
                particle = Instantiate(Particles[6], _transform);
                break;
            case Effects.ICEBALL:
                particle = Instantiate(Particles[7], _transform);
                break;
            case Effects.VOLTBALL:
                particle = Instantiate(Particles[8], _transform);
                break;
        }

        particles.Add(particle.GetComponent<ParticleSystem>());
        particles.AddRange(particle.GetComponentsInChildren<ParticleSystem>());
        foreach (ParticleSystem particleSystem in particles)
        {
            particleSystem.Play();
        }

        return particle;
    }
}