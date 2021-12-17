using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Potion")]
public class ItemPotion : ItemBase
{

    [SerializeField]
    int recoveryPower;

    [SerializeField]
    Define.PotionType potionType;


    public int _recoveryPower { get { return recoveryPower; } }

    public override bool Use(BaseContoller target)
    {
        if(target._myStat.HPrecovery(recoveryPower))
        {
            return Define.Success;
        }
        return !Define.Success;
    }

    public Define.PotionType _potionType { get { return potionType; } }
}
