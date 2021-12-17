using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownManager
{

    List<Item> itemCoolDown = new List<Item>();
    List<AbilityBase> skillCoolDown = new List<AbilityBase>();


    public void OnUpdate()
    {
        ItemCoolDownUpdate();
        SkillCoolDownUpdate();
    }

    void ItemCoolDownUpdate()
    {
        for (int i = 0; i < itemCoolDown.Count; i++)
        {
            itemCoolDown[i].coolTime -= Time.deltaTime;
            if (itemCoolDown[i].coolTime <= 0.0f)
            {
                itemCoolDown[i].coolTime = 0.0f;
                itemCoolDown.Remove(itemCoolDown[i]);
            }
        }
    }
    void SkillCoolDownUpdate()
    {
        for (int i = 0; i < skillCoolDown.Count; i++)
        {
            skillCoolDown[i].coolTime -= Time.deltaTime;
            if (skillCoolDown[i].coolTime <= 0.0f)
            {
                skillCoolDown[i].coolTime = 0.0f;
                skillCoolDown.Remove(skillCoolDown[i]);
            }
        }
    }

    public void StartCoolDown(Item item)
    {
        itemCoolDown.Add(item);
    }

    public void StartCoolDown(AbilityBase skill)
    {
        skillCoolDown.Add(skill);
    }
}
