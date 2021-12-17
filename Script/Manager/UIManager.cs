using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static public UIManager instance;

    [SerializeField]
    public Inventory Inventory;

    [SerializeField]
    public ItemSlot pickUpSlot;

    [SerializeField]
    public Define.PickUpType pickUpType;

    [SerializeField]
    public AbilityBase pickUpSkill;

    [SerializeField]
    public QuickSlotMain quickMain;

    [SerializeField]
    public Image DrageImage;

    [SerializeField]
    GameObject tooltip;

    public SkillBookMain skillBook;

    public StateInfoContoller statContoller;

    [SerializeField]
    public ToolTipHandler toolTipHandler;

    private void Start()
    {
        Init();
        Inventory = transform.GetChild(0).GetComponent<Inventory>();
        statContoller.Close();
        skillBook.Close();
        for (int i = 0; i < Inventory.transform.childCount; i++)
        {
            Inventory.transform.GetChild(i).gameObject.SetActive(false);
        }
        quickMain = transform.GetChild(1).GetComponent<QuickSlotMain>();

    }

    public void OnOffUI(Define.UIType type)
    {
        switch(type)
        {
            case Define.UIType.Inventory:
                Inventory.UI_OnOff();
                break;
            case Define.UIType.Stat:
                statContoller.UI_OnOff();
                break;
            case Define.UIType.Skill:
                skillBook.UI_OnOff();
                break;
        }
        instance.DrageImage.sprite = null;
        instance.DrageImage.gameObject.SetActive(false);
        instance.pickUpType = Define.PickUpType.none;
        instance.pickUpSlot = null;
        instance.pickUpSkill = null;
    }


    public void UseSlot(KeyCode InputKey, BaseContoller player)
    {
        switch(InputKey)
        {
            case KeyCode.Alpha1:
                if(!quickMain.UseQuickSlot(0, player))
                {
                    Debug.Log("쿨타임입니다.");
                }
                break;
            case KeyCode.Alpha2:
                if (!quickMain.UseQuickSlot(1, player))
                {
                    Debug.Log("쿨타임입니다.");
                }
                break;
            case KeyCode.Alpha3:
                if (!quickMain.UseQuickSlot(2, player))
                {
                    Debug.Log("쿨타임입니다.");
                }
                break;
            case KeyCode.Alpha4:
                if (!quickMain.UseQuickSlot(3, player))
                {
                    Debug.Log("쿨타임입니다.");
                }
                break;
            case KeyCode.Alpha5:
                if (!quickMain.UseQuickSlot(4, player))
                {
                    Debug.Log("쿨타임입니다.");
                }
                break;
            case KeyCode.Alpha6:
                if (!quickMain.UseQuickSlot(5, player))
                {
                    Debug.Log("쿨타임입니다.");
                }
                break;
        }
    }


    void Init()
    {
        if(instance == null)
        {
            instance = this.gameObject.GetComponent<UIManager>();
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }



}
