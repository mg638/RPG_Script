using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Inventory : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField]
    public List<ItemSlot> itemSlot;

    public ItemSlot Weapon;
    public ItemSlot Armor;

    public Vector2 clickPosition;

    public bool isOn = false;

    private void Start()
    {
        for(int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            for(int j = 0; j < transform.GetChild(0).transform.GetChild(i).childCount; j++)
            {
                itemSlot.Add(transform.GetChild(0).transform.GetChild(i).GetChild(j).GetComponent<ItemSlot>());
            }
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Define.rayContoller!= Define.RayContoller.Stop)
        Define.rayContoller = Define.RayContoller.Stop;

        
        float x = (clickPosition.x - eventData.position.x) * -1;
        float y = (clickPosition.y - eventData.position.y) * -1;
        transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
        clickPosition = eventData.position;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        clickPosition = eventData.position;
    }


    public bool AddItme(Item _item, int _cost = 1)
    {
        for(int i = 0; i < itemSlot.Count; i++)
        {
            if(itemSlot[i]._item == _item)
            {
                if(itemSlot[i].CostUpCheck(_cost))
                {
                    return Define.Success;
                }
            }
            else if(itemSlot[i]._item == null)
            {
                itemSlot[i]._item = _item;
                itemSlot[i].CostUpCheck(_cost);
                return Define.Success;
            }
        }
        return !Define.Success;
    }

    public void UI_OnOff()
    {
        if (isOn)
            CloseInventory();
        else
            OpenInventory();
        transform.SetAsLastSibling();
    }

    public void CloseInventory()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            isOn = false;
            Define.rayContoller = Define.RayContoller.Start;
        }
    }
    public void OpenInventory()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
            isOn = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Define.rayContoller = Define.RayContoller.Start;
    }

    public void EquipItem(ItemSlot itemSlot)
    {
        if(itemSlot._item.itemType == Define.ItemType.Armor)
        {
            Armor.ItemSwap(itemSlot);
        }
        else if(itemSlot._item.itemType == Define.ItemType.Weapon)
        {
            Weapon.ItemSwap(itemSlot);
        }
        UIManager.instance.statContoller.statUpdate();
    }

    public ItemSlot SearchItem(Item _item)
    {
        for(int i = 0; i < itemSlot.Count; i++)
        {
          if (itemSlot[i]._item == _item)
            {
                return itemSlot[i];
            }
        }
            return null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }
}
