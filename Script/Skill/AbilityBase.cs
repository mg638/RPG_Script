using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBase : MonoBehaviour
{
    public string spellID;
    public string name;
    public Sprite icon;
    public Define.State skillState;
    public int[] damageValue;
    public int level;
    public int maxLevel;
    public int[] skillLevelUpTable;
    public float coolTime;
    public float maxCoolTime;



    public virtual bool Use() { return true; }

    public bool CoolTimeCheck()
    {
        if (coolTime > 0.0f || level <= 0)
            return false;
        else
            return true;
    }
}
