using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    string SkillID;

    public AbilityBase skill;
    public PlayerStat myStat;

    [SerializeField]
    GameObject levelUpBtn;
    [SerializeField]
    Image coolTimeIcon;
    [SerializeField]
    Image skillIcon;
    [SerializeField]
    Text skillName;
    [SerializeField]
    Text skillLvValue;


    // Start is called before the first frame update
    void Start()
    {
        skill = SkillDataBase.instance.SearchSkill(SkillID);
        SlotUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SlotUpdate()
    {
        if (skill != null)
        {
            skillIcon.sprite = skill.icon;
            skillName.text = skill.name;
            skillLvValue.text = skill.level.ToString();
        }
        else
        {
            Debug.Log("Skill is Null");
        }

    }

    public void SkillUp()
    {
        if(myStat == null)
            myStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();

        if (skill.level < skill.maxLevel && myStat.Level >= skill.skillLevelUpTable[skill.level])
        {
            skill.level += 1;
            skillLvValue.text = skill.level.ToString();
        }

        SkillUpCheck(myStat.Level);
    }


    public void SkillUpCheck(int level)
    {
        if(level < skill.skillLevelUpTable[skill.level])
        levelUpBtn.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(skill.level > 0)
        {
            UIManager.instance.pickUpType = Define.PickUpType.Skill;
            UIManager.instance.DrageImage.sprite = skillIcon.sprite;
            UIManager.instance.DrageImage.gameObject.SetActive(true);
            UIManager.instance.pickUpSkill = skill;
            Define.rayContoller = Define.RayContoller.Stop;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UIManager.instance.pickUpSkill = null;
        UIManager.instance.pickUpType = Define.PickUpType.none;
        UIManager.instance.DrageImage.sprite = null;
        UIManager.instance.DrageImage.gameObject.SetActive(false);
        Define.rayContoller = Define.RayContoller.Start;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(skill.level > 0)
        {
            UIManager.instance.DrageImage.transform.position = eventData.position;
            UIManager.instance.DrageImage.transform.SetAsLastSibling();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       
    }
}
