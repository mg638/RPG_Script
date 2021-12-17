using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StateInfoContoller : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{

    public ItemSlot Weapon;
    public ItemSlot Armor;
    public StatBase myStat;

    [SerializeField]
    Text HP;
    [SerializeField]
    Text MP;
    [SerializeField]
    Text ATKDamage;
    [SerializeField]
    Text Defence;

    Vector2 oldPos;

    float UpdateTimer;

    public delegate void StatUpdate();
    public StatUpdate statUpdate;
    public PartsManager partsManager;
    void EquipStatUpdate()
    {
        if(Weapon._item == null)
        {
            myStat.ItemAtkDamage = 0;
        }
        else
        {
            myStat.ItemAtkDamage = Weapon._item.atk;
        }

        if (Armor._item == null)
        {
            myStat.ItemDef = 0;
        }
        else
        {
            myStat.ItemDef = Armor._item.def;
        }
        StatTxtUpdate();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateTimer = 0.0f;
        statUpdate += EquipStatUpdate;
        statUpdate();
        partsManager = gameObject.GetComponent<PartsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if((UpdateTimer += Time.deltaTime) >= 0.1f)
        {
            StatTxtUpdate();
            UpdateTimer = 0.0f;
        }
    }

    void StatTxtUpdate()
    {
        HP.text = $"{myStat.Hp} / {myStat.MaxHp}";
        MP.text = $"{myStat.Mp} / {myStat.MaxMp}";
        SetText(ATKDamage, myStat.AtkDamage);
        SetText(Defence, myStat.Defense);
    }

    void SetText(Text txt, int Value)
    {
        txt.text = Value.ToString();
    }

    public void Close()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        Define.rayContoller = Define.RayContoller.Start;
    }

    void Open()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void UI_OnOff()
    {
        if (transform.GetChild(0).gameObject.activeSelf)
            Close();
        else
            Open();
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        float x =  eventData.position.x - oldPos.x;
        float y = eventData.position.y - oldPos.y;
        transform.position = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
        oldPos = eventData.position;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        oldPos = eventData.position;
        transform.SetAsLastSibling();
        Define.rayContoller = Define.RayContoller.Stop;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Define.rayContoller = Define.RayContoller.Start;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }
}
