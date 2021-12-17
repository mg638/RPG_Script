using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipHandler : MonoBehaviour
{
    [SerializeField]
    public Item item;

    [SerializeField]
    GameObject toolTip;

    [SerializeField]
    Text name;
    [SerializeField]
    Text Desc;
    [SerializeField]
    Text cost;

    [SerializeField]
    Text LootItemText;

    void Start()
    {
        toolTip.SetActive(false);
        LootItemText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopUpToolTip(Item _item)
    {
        item = _item;
        DestSetting();
        toolTip.SetActive(true);
    }

    public void ColsToolTip()
    {
        item = null;
        toolTip.SetActive(false);
    }

    public void PopUpLootItme(Item _item)
    {
        if (_item == null)
            return;

        LootItemText.text = _item.itemName;
        LootItemText.gameObject.SetActive(true);
    }

    public void closeLootItem()
    {
        LootItemText.gameObject.SetActive(false);
    }

    void DestSetting()
    {
        name.text = item.itemName;
        cost.text = item.itemCost.ToString();

        if (item.itemType == Define.ItemType.Use)
        {
            Desc.text = item.itemDest;
        }
        else
        {
            if(item.itemType == Define.ItemType.Armor)
            {
                Desc.text = $"방어력 : {item.def} \n {item.itemDest}";
            }
            else if(item.itemType == Define.ItemType.Weapon)
            {
                Desc.text = $"공격력 : {item.atk} \n {item.itemDest}";
            }
        }
    }
}
