using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Item item;
    [SerializeField]
    Image coolTimeImage;
    [SerializeField]
    Image itemStrite;
    [SerializeField]
    Text costText;
    [SerializeField]
    Define.SlotType slotType;
    [SerializeField]
    Define.ItemType equipType;

    private void Start()
    {
        SlotSetting();
    }

    private void Update()
    {
        if (item != null)
        {
            CoolTimeImageUpdate();
        }
    }

    public bool CostUpCheck(int _cost)
    {
        if (item != null)
        {
            item.itemCost += _cost;
            SlotSetting();
            return Define.Success;
        }
        return !Define.Success;
    }

    void CoolTimeImageUpdate()
    {
        if (item.coolTime > 0.0f)
            coolTimeImage.fillAmount = item.coolTime / item.maxCoolTime;

        if (item.coolTime <= 0.0f && coolTimeImage.fillAmount != 0.0f)
            coolTimeImage.fillAmount = 0.0f;

    }

    public void SlotSetting()
    {
        if (item == null)
        {
            costText.gameObject.SetActive(false);
            coolTimeImage.fillAmount = 0.0f;
            itemStrite.sprite = null;
            itemStrite.gameObject.SetActive(false);
        }
        else
        {
            if (item.itemType != Define.ItemType.Use)
            {
                coolTimeImage.fillAmount = 0.0f;
            }

            if (item.itemType == Define.ItemType.Use || item.itemType == Define.ItemType.Etc)
            {
                if (!costText.gameObject.activeSelf)
                    costText.gameObject.SetActive(true);
                costText.text = item.itemCost.ToString();
            }
            else
            {
                if (costText.gameObject.activeSelf)
                    costText.gameObject.SetActive(false);
            }

            if (itemStrite != null)
            {
                itemStrite.sprite = item.itemSprite;
                itemStrite.gameObject.SetActive(true);
            }

        }

    }  
    public void OnDrag(PointerEventData eventData)
    {
      
        if (item != null)
        {
            UIManager.instance.DrageImage.transform.position = eventData.position;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (PointerEventData.InputButton.Left != eventData.button)
            return;

        if (item != null)
        {
            UIManager.instance.pickUpSlot = this;
            UIManager.instance.DrageImage.sprite = item.itemSprite;
            UIManager.instance.DrageImage.gameObject.SetActive(true);
            UIManager.instance.DrageImage.transform.SetAsLastSibling();
            UIManager.instance.pickUpType = Define.PickUpType.item;
            Define.rayContoller = Define.RayContoller.Stop;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (UIManager.instance.pickUpSlot != null)
        {
            if (EquipCheck(UIManager.instance.pickUpSlot) && EquipTypeCheck(UIManager.instance.pickUpSlot))
            {
                ItemSwap(UIManager.instance.pickUpSlot);
                UIManager.instance.DrageImage.sprite = null;
                UIManager.instance.DrageImage.gameObject.SetActive(false);
                UIManager.instance.pickUpSlot = null;
                UIManager.instance.statContoller.statUpdate();
            }
            else if (slotType == Define.SlotType.InventorySlot)
            {
                if ((item != null && !EquipCheck(UIManager.instance.pickUpSlot)) || item == null)
                {
                    ItemSwap(UIManager.instance.pickUpSlot);
                    UIManager.instance.DrageImage.sprite = null;
                    UIManager.instance.DrageImage.gameObject.SetActive(false);
                    UIManager.instance.pickUpSlot = null;
                    UIManager.instance.statContoller.statUpdate();
                }
            }
            else
            {
                UIManager.instance.pickUpSlot = null;
                UIManager.instance.DrageImage.sprite = null;
                UIManager.instance.pickUpSlot = null;
                UIManager.instance.DrageImage.gameObject.SetActive(false);
                return;
            }
        }
    }

    public void ItemSwap(ItemSlot _itemSlot)
    {

        Item temp = item;
        item = _itemSlot.item;
        _itemSlot.item = temp;
        Define.rayContoller = Define.RayContoller.Start;
        _itemSlot.SlotSetting();
        SlotSetting();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            UIManager.instance.pickUpSlot = null;
            UIManager.instance.DrageImage.sprite = null;
            UIManager.instance.pickUpSlot = null;
            UIManager.instance.DrageImage.gameObject.SetActive(false);
            UIManager.instance.pickUpType = Define.PickUpType.none;
            Define.rayContoller = Define.RayContoller.Start;
            SlotSetting();
        }
    }


    bool EquipCheck(ItemSlot slot)
    {
        return slot.slotType == Define.SlotType.EquipSlot || slotType == Define.SlotType.EquipSlot;
    }

    bool EquipTypeCheck(ItemSlot slot)
    {
        return (equipType == slot.item.itemType);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (item == null || UIManager.instance.pickUpSlot != null)
            return;

        if (eventData.button == PointerEventData.InputButton.Right && item.coolTime <= 0.0f)
        {
            if (item.itemType == Define.ItemType.Use)
                UseItem(GameObject.FindGameObjectWithTag("Player").GetComponent<BaseContoller>());
            else if (item.itemType == Define.ItemType.Weapon || item.itemType == Define.ItemType.Armor)
            {
                if (slotType == Define.SlotType.EquipSlot)
                {
                    if (UIManager.instance.Inventory.AddItme(item))
                    {
                        item = null;
                        SlotSetting();
                        UIManager.instance.statContoller.statUpdate();
                        UIManager.instance.toolTipHandler.ColsToolTip();
                    }
                }
                else
                {
                    UIManager.instance.Inventory.EquipItem(this);
                    UIManager.instance.toolTipHandler.ColsToolTip();
                }
            }

        }
    }

    public bool UseItem(BaseContoller player)
    {
        if (item.UseItem(player._myStat))
        {
            GameManager.coolDownManager.StartCoolDown(item);
            if ((item.itemCost -= 1) == 0)
            {
                UIManager.instance.toolTipHandler.ColsToolTip();
                item = null;
            }
            SlotSetting();
            return true;
        }
        return false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            UIManager.instance.toolTipHandler.ColsToolTip();
            UIManager.instance.toolTipHandler.PopUpToolTip(item);
            UIManager.instance.toolTipHandler.gameObject.transform.position = eventData.position;
            UIManager.instance.toolTipHandler.gameObject.transform.SetAsLastSibling();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item != null)
        {
            UIManager.instance.toolTipHandler.ColsToolTip();
        }
    }

    public Item _item
    {
        get { return item; }
        set { item = value; SlotSetting(); }
    }

    void PartsSet()
    {
        if(item.itemType == Define.ItemType.Weapon)
        {
            for (int i = 0; i < GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerContoller>().Weapon.Length; i++)
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerContoller>().Weapon[i].name == item.itemName)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerContoller>().Weapon[i].SetActive(true);
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerContoller>().Weapon[i].SetActive(false);
                }
            }

        }
        else
        {
            for (int i = 0; i < GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerContoller>().Armor.Length; i++)
            {
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerContoller>().Armor[i].name == item.itemName)
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerContoller>().Armor[i].SetActive(true);
                }
                else
                {
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerContoller>().Armor[i].SetActive(false);
                }
            }
        }
        
    }
}
