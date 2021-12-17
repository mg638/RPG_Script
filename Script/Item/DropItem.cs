using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropItem : MonoBehaviour
{
    [SerializeField]
    public string itemID;
    public Item item;
    public int dropCost;



    public void Drop()
    {
        item = GameManager.itemDataBase.SearchItem(itemID);
    }
    

}
