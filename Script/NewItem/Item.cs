using UnityEngine;


public class Item
{
    public string itemID;
    public string itemName;
    public string itemDest;
    public int itemCost;
    public Sprite itemSprite;
    public Define.ItemType itemType;
    public float coolTime = 0;
    public float maxCoolTime;

    public int def;
    public int atk;
    public int life;
    public int mp;
    public int recovery_HP;
    public int recovery_MP;

    public Item(string _itemID, string _itemName, string _itemDest, int _itemCost, Define.ItemType _itemType,
         int _def = 0, int _atk = 0, int _life = 0, int _mp = 0, int _recovery_HP = 0, int _recovery_MP = 0, int _maxCoolTime = 0)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemDest = _itemDest;
        itemCost = _itemCost;
        itemType = _itemType;
        itemSprite = Resources.Load<Sprite>($"Sprite/item/{_itemID}");
        def = _def;
        atk = _atk;
        life = _life;
        mp = _mp;
        recovery_HP = _recovery_HP;
        recovery_MP = _recovery_MP;
        maxCoolTime = _maxCoolTime;
    }
    public Item()
    {

    }

    public bool UseItem(StatBase playerStat)
    {
        if(recovery_HP > 0 && playerStat.HPrecovery(recovery_HP))
        {
            ParticleManager.Instance.Onec(playerStat.gameObject.transform, ParticleManager.ParticleType.HP_Recovery);
            coolTime = maxCoolTime;
            return true;
        }
        return false;
    }
}
