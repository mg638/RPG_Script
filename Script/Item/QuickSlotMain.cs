using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlotMain : MonoBehaviour
{
    [SerializeField]
    QuickSlot[] quickSlot; 

    // Start is called before the first frame update
    void Start()
    {
        quickSlot = new QuickSlot[transform.GetChild(0).transform.childCount];
        for(int i = 0; i < transform.GetChild(0).transform.childCount; i++)
        {
            quickSlot[i] = transform.GetChild(0).transform.GetChild(i).GetComponent<QuickSlot>();
            quickSlot[i].inputKeyTxt.text = (i + 1).ToString();
        }
    }

    public bool UseQuickSlot(int idx, BaseContoller player)
    {
        if(quickSlot[idx].item == null)
        {
            if (quickSlot[idx].skill != null)
                if(quickSlot[idx].skill.CoolTimeCheck())
                {
                    player.gameObject.GetComponent<PlayerContoller>().UseNoneTargetSkill(quickSlot[idx].skill.skillState);
                    return true;
                }
                else
                {
                    return false;
                }
        }
        else
        {
            if (quickSlot[idx].item != null)
                return quickSlot[idx].item.UseItem(player);
        }
        return false;
    }



}
