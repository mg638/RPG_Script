using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDataBase : MonoBehaviour
{

    static public SkillDataBase instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public enum SpellType
    {
        boomAtk,
    }

    public BoomAtk boomAtk;

    [SerializeField]
    public List<AbilityBase> SkillList;

    // Start is called before the first frame update
    void Start()
    {
        SkillList.Add(boomAtk);
        SkillList[0].coolTime = 0;
        SkillList[0].level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AbilityBase SearchSkill(string _SkilID)
    {
        for(int i =0; i< SkillList.Count;i++)
        {
            if (SkillList[i].spellID == _SkilID)
                return SkillList[i];
        }
        return null;
    }
}
