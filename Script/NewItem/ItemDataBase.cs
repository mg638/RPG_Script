using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase
{

    public List<Item> itemDB = new List<Item>();
    
    public void InitItemTable()
    {
        itemDB.Add(new Item("10001", "회복 포션", "HP를 50회복합니다.", 0, Define.ItemType.Use, 0, 0, 0, 0, 50, 0, 10));
        itemDB.Add(new Item("20001", "대거", "평범한 대거입니다.", 0, Define.ItemType.Weapon, 0, 2, 0, 0, 0, 0, 10));
        itemDB.Add(new Item("20002", "본 소드", "해골의 뼈로 만든 검입니다.", 0, Define.ItemType.Weapon, 0, 5, 0, 0, 0, 0, 10));
        itemDB.Add(new Item("20003", "스틸 소드", "강철로 만들어진 검입니다.", 0, Define.ItemType.Weapon, 0, 11, 0, 0, 0, 0, 10));
        itemDB.Add(new Item("30001", "누더기 옷", "허름한 누더기 옷", 0 ,Define.ItemType.Armor, 2, 0, 0, 0, 0, 0, 0));
        itemDB.Add(new Item("30002", "본 아머", "해골의 뼈로 만든 갑옷", 0, Define.ItemType.Armor, 5, 0, 0, 0, 0, 0, 0));
        itemDB.Add(new Item("30003", "스틸 아머", "강철로 만든 갑옷", 0, Define.ItemType.Armor, 8, 0, 0, 0, 0, 0, 0));

    }

    public Item SearchItem(string itemID)
    {
        for(int i = 0; i < itemDB.Count; i++)
        {
            if(itemDB[i].itemID == itemID)
            {
                return itemDB[i];
            }
        }
        return null;
    }
}
