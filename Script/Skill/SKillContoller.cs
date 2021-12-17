using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKillContoller : MonoBehaviour
{
    public int Damage;

    [SerializeField]
    private float playTime;

    private void OnEnable()
    {
        playTime = 2.0f;
        gameObject.GetComponent<ParticleSystem>().Play();
        StartCoroutine("ParticleContorll");
    }

    IEnumerator ParticleContorll()
    {
        yield return new WaitForSeconds(playTime);
        gameObject.GetComponent<ParticleSystem>().Stop();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            Debug.Log(other.name);
            if (other.GetComponent<BaseContoller>().WorldObjectType == Define.WorldObject.Monster)
            {
                other.GetComponent<StatBase>().TakeDamage(Damage);
            }
        }
    }
}
