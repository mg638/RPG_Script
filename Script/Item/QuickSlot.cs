using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class QuickSlot : MonoBehaviour, IDropHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    ItemSlot itemSlot;
    [SerializeField]
    Item temp;
    [SerializeField]
    Image Icon;
    [SerializeField]
    Text Cnt_Txt;
    [SerializeField]
    Image coolTimeIcon;
    [SerializeField]
    public Text inputKeyTxt;

    [SerializeField]
    List<ItemBase> SearchItem;


    [SerializeField]
    public AbilityBase skill;

    int currentCnt;
    public float coolTime;
    public float maxCoolTime;

    private void OnEnable()
    {
        Icon.gameObject.SetActive(false);
        Cnt_Txt.gameObject.SetActive(false);
       // coolTimeIcon.gameObject.SetActive(false);
    }

    private void Start()
    {

    }
    //드롭 헀을 경우. 스킬과 아이템으로 구분함.

    
    private void Update()
    {
        if (itemSlot != null)
        {
            if (itemSlot._item != null)
            {
                slotUpdate();
            }
            else
            { 
                itemSlot = null;
                slotUpdate();
            }
        }
        coolTimeIconUpdate();
    }
    //아이템 슬롯 정보 업데이트
    public void slotUpdate()
    {
        if(itemSlot == null && skill == null && temp == null)
        {
            Icon.gameObject.SetActive(false);
            Cnt_Txt.gameObject.SetActive(false);
            return;
        }
        if(itemSlot != null)
        {
            Icon.sprite = itemSlot._item.itemSprite;
            Cnt_Txt.text = itemSlot._item.itemCost.ToString();
            Icon.gameObject.SetActive(true);
            Cnt_Txt.gameObject.SetActive(true);
        }
        else if(skill != null)
        {
            Cnt_Txt.gameObject.SetActive(false);
            coolTimeIcon.fillAmount = 0.0f;
            Icon.sprite = skill.icon;
            Icon.gameObject.SetActive(true);
        }
        else
        {
            itemSlot = UIManager.instance.Inventory.SearchItem(temp);
            if(itemSlot == null)
            {
                Icon.gameObject.SetActive(false);
                Cnt_Txt.gameObject.SetActive(false);
            }
            else
            {
                slotUpdate();
            }
        }
    }
    //아이템을 드롭했을 때 정보 업데이트
    void dropUpdate()
    {
        if (UIManager.instance.pickUpSlot != null && UIManager.instance.pickUpType == Define.PickUpType.item)
        {
            if (UIManager.instance.pickUpSlot._item.itemType == Define.ItemType.Use)
            {
                itemSlot = UIManager.instance.pickUpSlot;
                temp = itemSlot._item;
                UIManager.instance.pickUpSlot = null;
                skill = null;
                slotUpdate();
            }
        }
        else if(UIManager.instance.pickUpSkill != null && UIManager.instance.pickUpType == Define.PickUpType.Skill)
        {
            skill = UIManager.instance.pickUpSkill;
            UIManager.instance.pickUpSkill = null;
            UIManager.instance.pickUpType = Define.PickUpType.none;
            itemSlot = null;
            slotUpdate();
        }
    }

    //쿨타임 이미지. 스킬 아이템 공용
    void coolTimeIconUpdate()
    {
        if(itemSlot != null)
        {
            if (itemSlot._item.coolTime > 0.0f)
              coolTimeIcon.fillAmount = itemSlot._item.coolTime / itemSlot._item.maxCoolTime;
        }else if(skill != null)
        {
            if (skill.coolTime > 0.0f)
                coolTimeIcon.fillAmount = skill.coolTime / skill.maxCoolTime;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
       if(itemSlot != null)
        {
            UIManager.instance.pickUpSlot = itemSlot;
            UIManager.instance.pickUpType = Define.PickUpType.item;
            UIManager.instance.DrageImage.sprite = Icon.sprite;
            UIManager.instance.DrageImage.gameObject.SetActive(true);
            Define.rayContoller = Define.RayContoller.Stop;
        }
        else if(skill != null)
        {
            UIManager.instance.pickUpSkill = skill;
            UIManager.instance.pickUpType = Define.PickUpType.Skill;
            UIManager.instance.DrageImage.sprite = Icon.sprite;
            UIManager.instance.DrageImage.gameObject.SetActive(true);
            Define.rayContoller = Define.RayContoller.Stop;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(UIManager.instance.DrageImage.sprite != null)
        {
            UIManager.instance.DrageImage.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (UIManager.instance.pickUpSlot == null && UIManager.instance.pickUpSkill == null)
        {
            UIManager.instance.pickUpSlot = null;
            UIManager.instance.pickUpSkill = null;
            UIManager.instance.pickUpType = Define.PickUpType.none;
            UIManager.instance.DrageImage.sprite = null;
            itemSlot = null;
            skill = null;
            slotUpdate();
        }
        else
        {
            UIManager.instance.pickUpSlot = null;
            UIManager.instance.pickUpSkill = null;
            UIManager.instance.pickUpType = Define.PickUpType.none;
            UIManager.instance.DrageImage.sprite = null;
        }
        UIManager.instance.DrageImage.gameObject.SetActive(false);
        Define.rayContoller = Define.RayContoller.Start;
    }
    public void OnDrop(PointerEventData eventData)
    {
         dropUpdate();
    }

    //안씀
    public ItemSlot item { get { return itemSlot; } }
}
