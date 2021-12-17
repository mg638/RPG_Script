using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ( menuName = "Item/Equip")]
public class ItemEquip : ItemBase
{
    [SerializeField]
    int EquipLevel;
    [SerializeField]
    int Str;
    [SerializeField]
    int Life;
    [SerializeField]
    int Armor;
    [SerializeField]
    int Damage;
    [SerializeField]
    float AtkSpeed;
    [SerializeField]
    Define.EquipType equipType;


    public int _Str { get { return Str; } }
    public int _EquipLevel { get { return EquipLevel; } }
    public int _Life { get { return Life; } }
    public int _Armor { get { return Armor; } }
    public int _Damage { get { return Damage; } }
    public float _AtkSpeed { get { return AtkSpeed; } }
    public Define.EquipType _equipType { get { return equipType; } }

}
