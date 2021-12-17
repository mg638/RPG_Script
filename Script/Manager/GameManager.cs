using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager s_instance = null;
    public static GameManager Instance { get { Init(); return s_instance; } }
    
    InputManager _input = new InputManager();
    public static InputManager Input { get { return Instance._input; } }

    public static CursorController cursorController;

    CoolDownManager _coolDown = new CoolDownManager();

    public static CoolDownManager coolDownManager { get { return Instance._coolDown; } }

    
    ItemDataBase _itemDB = new ItemDataBase();

    public static ItemDataBase itemDataBase { get { return Instance._itemDB; } }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        _itemDB.InitItemTable();
        cursorController = GetComponent<CursorController>();
    }

    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();
        _coolDown.OnUpdate();
    }

    static void Init()
    {
        GameManager obj = FindObjectOfType<GameManager>();
        if (s_instance == null)
        {
            s_instance = obj;
            DontDestroyOnLoad(s_instance);
        }
        else
        {
            if(s_instance != obj)
            {
                Destroy(obj.gameObject);
            }
        }
    }
}
