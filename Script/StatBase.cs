using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBase : MonoBehaviour 
{
   [SerializeField] protected int _level;
   [SerializeField] protected int _hp;
   [SerializeField] protected int _maxHp;
   [SerializeField] protected int _mp;
   [SerializeField] protected int _maxMp;
   [SerializeField] protected int _atkDamage;
   [SerializeField] protected int _defense;
   [SerializeField] protected float _moveSpeed;
   [SerializeField] protected float _atkSpeed;
   [SerializeField] protected int _itemDef;
   [SerializeField] protected int _itemHP;

    [SerializeField] protected int _itemAtkDamage;

    public int Level { get { return _level; } set { _level = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp + _itemHP; } set { _maxHp = value; } }
    public int Mp { get { return _mp; } set { _mp = value; } }
    public int MaxMp { get { return _maxMp; } set { _maxMp = value; } }
    public int AtkDamage { get { return _atkDamage + _itemAtkDamage; } set { _atkDamage = value; } }
    public int Defense { get { return _defense + _itemDef; } set { _defense = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float AtkSpeed { get { return _atkSpeed; } set { _atkSpeed = value; } }

    public int ItemDef { get { return _itemDef; } set { _itemDef = value; } }
    public int ItemHP { get { return _itemHP; } set { _itemHP = value; } }
    public int ItemAtkDamage { get { return _itemAtkDamage; } set { _itemAtkDamage = value; } }

    public void TakeDamage(int Damage)
    {
       _hp -= Mathf.Max(0, Damage - _defense);
    }

    public bool HPrecovery(int hp)
    {
       if(_hp == _maxHp)
        {
            Debug.Log("이미 생명력이 최대치입니다.");
            return !Define.Success;
        }
        else
        {
            if (_hp + hp > _maxHp)
                _hp = _maxHp;
            else
                _hp += hp;

            return Define.Success;
        }
    }
    public virtual void Init()
    {
        _level = 1;
        _maxHp = 50;
        _hp = MaxHp;
        _maxMp = 50;
        _mp = MaxMp;
        _atkDamage = 5;
        _defense = 0;
        _moveSpeed = 5.0f;
        _atkSpeed = 1.0f;
        _atkSpeed = 0;
        _itemDef = 0;
        _itemHP = 0;
        _itemAtkDamage = 0;
}
}
