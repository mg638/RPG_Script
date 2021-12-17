using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoomAtk : AbilityBase
{
    
    public GameObject BoomAtkInstance;
    GameObject clone;
    public Transform spawnPos;

    private void OnApplicationQuit()
    {
        coolTime = 0.0f;
        level = 0;
    }

    public override bool Use()
    {
  
        if(clone == null)
        {
            spawnPos = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerContoller>().spawnPos.transform;
            clone = Instantiate(BoomAtkInstance, spawnPos);
            clone.transform.parent = null;
            clone.GetComponent<SKillContoller>().Damage = damageValue[level];
            clone.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            clone.gameObject.SetActive(true);
        }
        else
        {
            clone.GetComponent<SKillContoller>().Damage = damageValue[level];
            clone.transform.position = spawnPos.position;
            clone.gameObject.SetActive(true);
        }
        coolTime = maxCoolTime;
        GameManager.coolDownManager.StartCoolDown(this);
        return true;
      
    }


  
    
}
