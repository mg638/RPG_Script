using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    private static ParticleManager m_Instance;
    public static ParticleManager Instance;

    [SerializeField]
    GameObject[] particleSystems;

    public enum ParticleType
    {
        HP_Recovery,
        fire_Explosion
    }


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        if(m_Instance == null)
        {
            m_Instance = GameObject.FindGameObjectWithTag("ParticleManager").GetComponent<ParticleManager>();
            Instance = m_Instance;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlayParticle(Vector3 pos, Vector3 normal, ParticleType particleType, Transform parent = null)
    {
        switch(particleType)
        {
            case ParticleType.HP_Recovery:

                break;
        }
    }

    public void Onec(Transform pos, ParticleType type)
    {
        switch(type)
        {
            case ParticleType.HP_Recovery:
                GameObject clone = GameObject.Instantiate(particleSystems[(int)type], pos);
                break;
            case ParticleType.fire_Explosion:
                break;
        }
    }

}
