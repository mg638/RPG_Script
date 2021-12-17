using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStat : StatBase
{
    [SerializeField]
    int _exp;
    [SerializeField]
    int _maxExp;
    [SerializeField]
    int _gold;

    [SerializeField]
    Image expBar;

    [SerializeField]
    Text levelTxt;
    public void statSetting(int level, int maxHp, int maxMp, int atkDamage, int definse, int maxExp)
    {
        _level = level;
        _maxHp = maxHp;
        _hp = _maxHp;
        _maxMp = maxMp;
        _mp = _maxMp;
        _atkDamage = atkDamage;
        _defense = definse;
        _maxExp = maxExp;
        Debug.Log("레벨업!");
    }


    public void levelUp()
    {
        switch(_level)
        {
            case 1:
                statSetting(1, 50, 50, 5, 0, 50);
                break;
            case 2:
                statSetting(2, 60, 55, 6, 1, 75);
                break;
            case 3:
                statSetting(3, 70, 60, 6, 2, 100);
                break;
            case 4:
                statSetting(4, 90, 60, 7, 2, 150);
                break;
            case 5:
                statSetting(5, 100, 70, 7, 2, 200);
                break;
            case 6:
                statSetting(6, 110, 80, 7, 3, 280);
                break;
            case 7:
                statSetting(7, 130, 80, 7, 3, 350);
                break;
            case 8:
                statSetting(8, 140, 90, 7, 3, 400);

                break;
            case 9:
                statSetting(9, 160, 90, 7, 3, 550);
                break;
            case 10:
                statSetting(10, 180, 100, 7, 3, 1000000);
                break;
            default:
                {
                    _exp = _maxExp;
                    return;
                }

        }

    }

  
    public override void Init()
    {
        base.Init();

        _level = 1;
        _maxHp = 50;
        _hp = MaxHp;
        _maxMp = 50;
        _mp = MaxMp;
        _atkDamage = 5;
        _defense = 0;
        _moveSpeed = 5.0f;
        _atkSpeed = 1.0f;
        _exp = 0;
        _maxExp = 50;
        _gold = 0;
        levelTxt.text = _level.ToString();
    }

    public int exp 
    { 
        get 
        { 
            return _exp; 
        }
        set
        {
            _exp = value;
            expBar.fillAmount = (float)_exp / _maxExp;
            if (LevelUpChecl())
            {
               levelUp();
            }
        }
    }

    bool LevelUpChecl()
    {
        if (_exp >= _maxExp)
        {
            _level += 1;
            _exp -= _maxExp;
            levelTxt.text = _level.ToString();
            return true;
        }
        else
            return false;
    }
}
