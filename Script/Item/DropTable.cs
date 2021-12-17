using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour
{

    [SerializeField]
    string[] ID;

    [SerializeField]
    int[] DropChance;

    [SerializeField]
    GameObject prePab;
    
    public void Drop()
    {
        for(int i = 0; i < ID.Length; i++)
        {
            if(Random.Range(0, 100) > DropChance[i])
            {
                prePab.GetComponent<DropItem>().itemID = ID[i];
                GameObject clone = Instantiate(prePab);
                clone.GetComponent<DropItem>().Drop();
                clone.transform.position = gameObject.transform.position;
            }
            
        }
    }

}
