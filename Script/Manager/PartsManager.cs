using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsManager : MonoBehaviour
{

    GameObject currentWeapon;
    GameObject currentArmor;

    public List<GameObject> RWeaponList;
    public List<GameObject> armorList;
    public GameObject LWeapon;

    public void SearchID(Define.ItemType itemType, string itemID)
    {

        switch (itemType)
        {
            case Define.ItemType.Weapon:
                if(itemID == null)
                {
                    currentWeapon.SetActive(false);
                    currentWeapon = null;
                    return;
                }
                for (int i = 0; i < RWeaponList.Count; i++)
                {
                    if (RWeaponList[i].name == itemID)
                    {
                        RWeaponList[i].gameObject.SetActive(true);
                        SetCurrentParts(itemType, RWeaponList[i]);
                    }
                }
                break;
            case Define.ItemType.Armor:
                if (itemID == null)
                {
                    SetCurrentParts(itemType, armorList[0]);
                    return;
                }
                for (int i = 0; i < armorList.Count; i++)
                {
                    if (armorList[i].name == itemID)
                    {
                        armorList[i].gameObject.SetActive(true);
                        SetCurrentParts(itemType, armorList[i]);
                    }
                }
                break;
            default:

                break;
        }

    }

    void SetCurrentParts(Define.ItemType itemType, GameObject newCurrentParts)
    {
        switch (itemType)
        {
            case Define.ItemType.Weapon:
                if (currentWeapon != null) newCurrentParts.gameObject.SetActive(false);
                currentWeapon = newCurrentParts;
                break;
            case Define.ItemType.Armor:
                if (currentArmor != null) newCurrentParts.gameObject.SetActive(false);
                currentArmor = newCurrentParts;
                break;
            default:

                break;
        }
    }


}
