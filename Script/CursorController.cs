using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    enum CursorType
    {
        Basic,
        Attack,
        Loot
    }

    [SerializeField]
    Texture2D AtkIcon;
    [SerializeField]
    Texture2D MouseIcon;
    [SerializeField]
    Texture2D LootIcon;


    CursorType myCursorType;
    // Start is called before the first frame update
    void Start()
    {
        AtkIcon = Resources.Load<Texture2D>("UI/Cursor/Attack");
        MouseIcon = Resources.Load<Texture2D>("UI/Cursor/Basic");
        LootIcon = Resources.Load<Texture2D>("UI/Cursor/Loot");
        Cursor.SetCursor(MouseIcon, new Vector2(MouseIcon.width / 3, 0), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMouseCursor();
    }

    int mask = (1 << (int)Define.LayerMask.Ground) | (1 << (int)Define.LayerMask.Monster ) | ( 1 << (int)Define.LayerMask.Item) | (1 << (int)Define.LayerMask.Npc);
    void UpdateMouseCursor()
    {

        if (Input.GetMouseButton(0))
        {
            return;
        }
        if (Define.rayContoller == Define.RayContoller.Stop)
            return;

        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.LayerMask.Monster && !hit.collider.gameObject.GetComponent<BaseContoller>()._isDie)
            {
                UIManager.instance.toolTipHandler.closeLootItem();
               ChangeAttackCursor();
            }
            else if (hit.collider.gameObject.layer == (int)Define.LayerMask.Item || hit.collider.gameObject.layer == (int)Define.LayerMask.Npc)
            {
                if(hit.collider.gameObject.layer == (int)Define.LayerMask.Item)
                {
                    UIManager.instance.toolTipHandler.PopUpLootItme(hit.collider.gameObject.GetComponent<DropItem>().item);
                }
                else
                {
                    UIManager.instance.toolTipHandler.closeLootItem();
                }
                ChangeLootCursor();
            }
            else
            {
                UIManager.instance.toolTipHandler.closeLootItem();
                ChangeBasicCursor();
            }
        }
    }

    public void ChangeAttackCursor()
    {
        if (myCursorType != CursorType.Attack)
        {
            myCursorType = CursorType.Attack;
            Cursor.SetCursor(AtkIcon, new Vector2(AtkIcon.width / 3, 1), CursorMode.Auto);
        }
    }

    public void ChangeBasicCursor()
    {
        if (myCursorType != CursorType.Basic)
        {
            myCursorType = CursorType.Basic;
            Cursor.SetCursor(MouseIcon, new Vector2(MouseIcon.width / 3, 1), CursorMode.Auto);
        }
    }

    public void ChangeLootCursor()
    {
        if(myCursorType != CursorType.Loot)
        {
            myCursorType = CursorType.Loot;
            Cursor.SetCursor(LootIcon, new Vector2(LootIcon.width / 3, 1), CursorMode.Auto);
        }
    }

}
