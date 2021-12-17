
public class Define
{
    static public bool Success = true;
    static public RayContoller rayContoller = RayContoller.Start;
    public enum SkillType
    {
        Active,
        Passive
    }

    public enum MonsterType
    {
        Skeleton
    }

    public enum WorldObject
    {
        Unknown,
        Player,
        Monster
    }
    public enum LayerMask
    {
        Monster = 8,
        Ground = 9,
        Block = 10,
        Item = 11,
        Npc = 12
    }
    
    public enum MouseEvent
    {
        Click,
        PointerDown,
        PointerUp,
        Press
    }


    public enum CameraMode
    {
        QuaterView

    }

    public enum State
    {
        Idle,
        Move,
        NormalAtk,
        Skill1,
        Die = 99
    }
    public enum CursorType
    {
        Basic,
        Attack
    }

    public enum ItemType
    {
        None,
        Use,
        Etc,
        Weapon,
        Armor
    }
    public enum EquipType
    {
        Weapon,
        Armor
    }

    public enum PotionType
    {
        Hp,
        Mp
    }

    public enum UIType
    {
        Inventory,
        Stat,
        Skill,
        Shop
    }

    public enum PickUpType
    {
        none,
        item,
        Skill
    }
    
    public enum RayContoller
    {
        Stop,
        Start
    }

    public enum SlotType
    { 
        InventorySlot,
        EquipSlot
    }

}
