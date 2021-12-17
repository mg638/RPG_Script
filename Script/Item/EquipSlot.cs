using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    [SerializeField]
    Image icon;
    [SerializeField]
    Define.EquipType slotType;
    // Start is called before the first frame update
    void Start()
    {
   
        icon = gameObject.transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(UIManager.instance.pickUpSlot != null)
        {
            //if(UIManager.instance.pickUpSlot._item[0]._itemType == Define.ItemType.equip)
            //{
            //    List<ItemBase> temp = itemSlot.item;
            //    itemSlot.item = UIManager.instance.pickUpSlot.item;
            //    UIManager.instance.pickUpSlot.item = temp;
            //    SlotSetting();
            //    UIManager.instance.pickUpSlot.SlotSetting();
            //}
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    void SlotSetting()
    {
        //icon.sprite = itemSlot._item[0]._Icon;
    }
}
