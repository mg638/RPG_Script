using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : ScriptableObject
{
    [SerializeField]
    string itemName;
    [SerializeField]
    Define.ItemType itemType;
    [SerializeField]
    Sprite icon;
    [SerializeField]
    string displayText;
    [SerializeField]
    int maxCnt;
    [SerializeField]
    int Cnt;
    [SerializeField]
    protected float maxCoolTime;

    public string _itemName { get { return itemName; } }
    public string _displayText { get { return displayText; } }

    public int _Cnt { get { return Cnt; } set { Cnt = _Cnt; } }

    public Sprite _Icon { get { return icon; } }

    public bool MaxCheck(int itemCnt)
    {
        if (itemCnt == maxCnt)
        {
            return true;
        }
        return false;
    }


    public float _MaxCoolTime { get { return maxCoolTime; }set { maxCoolTime = _MaxCoolTime; } }

    public Define.ItemType _itemType { get { return itemType; } }

    public virtual bool Use(BaseContoller target)
    {
        return true;
    }
}
