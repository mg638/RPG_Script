using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerContoller : BaseContoller
{

    [SerializeField]
    private GameObject movePoint = null;
    [SerializeField]
    private float rotSpeed = 5.0f;
    Define.CursorType myCursorType;
    [SerializeField]
    GameObject lookPos;

    [SerializeField]
    public GameObject spawnPos;
    [SerializeField]
    AbilityBase boomAkt;

    [SerializeField]
    public GameObject[] Weapon;

    [SerializeField]
    public GameObject[] Armor;

    [SerializeField]
    public GameObject lootItem;

    int mask = (1 << (int)Define.LayerMask.Ground) | (1 << (int)Define.LayerMask.Monster) | (1 << (int)Define.LayerMask.Item | (1 << (int)Define.LayerMask.Npc));
    GameObject lockGarget;

    bool _stopAtk = false;


    protected override void Init()
    {
        myAnim = GetComponent<Animator>();
        navi = gameObject.GetComponent<NavMeshAgent>();
        navi.updateRotation = false;
        myStat = transform.GetComponent<PlayerStat>();
        myStat.Init();
        myState = Define.State.Idle;
        myStat.MoveSpeed = 5.0f;
        navi.speed = myStat.MoveSpeed;
        GameManager.Input.MouseActuon -= ClickEvent;
        GameManager.Input.MouseActuon += ClickEvent;
        GameManager.Input.OnKeyDwon -= KeyDownEvent;
        GameManager.Input.OnKeyDwon += KeyDownEvent;
        WorldObjectType = Define.WorldObject.Player;

        //boomAkt.Initalize(spawnPos);
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }


    void ClickEvent(Define.MouseEvent evt)
    {
        switch(myState)
        {
            case Define.State.Idle:
                ClickEvent_Move(evt);
                break;
            case Define.State.Move:
                ClickEvent_Move(evt);
                break;
            case Define.State.NormalAtk:
                if(evt == Define.MouseEvent.PointerUp || TargetIsDieCheck())
                {
                    _stopAtk = true;
                }
                break;
            case Define.State.Die:
                break;

        }
    }
    
    //이동 관련 함수
    private void ClickEvent_Move(Define.MouseEvent evt)
    {
      

        if (myState == Define.State.Die 
           || (myAnim.GetInteger("State") != (int)Define.State.Idle && (myAnim.GetInteger("State") != (int)Define.State.Move)) 
           || (!myAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !myAnim.GetCurrentAnimatorStateInfo(0).IsName("Run")))
            return;



        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        bool rayHit = Physics.Raycast(ray, out hit, 100.0f, mask);


        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    if (rayHit)
                    {
                        movePoint.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                        if ((hit.collider.gameObject.layer == (int)Define.LayerMask.Monster && !hit.collider.gameObject.GetComponent<BaseContoller>()._isDie))
                        {
                            lockGarget = hit.collider.gameObject;
                            _stopAtk = false;
                        }
                        else if (hit.collider.gameObject.layer == (int)Define.LayerMask.Item || hit.collider.gameObject.layer == (int)Define.LayerMask.Npc)
                        {
                            lockGarget = hit.collider.gameObject;
                        }
                        else if(hit.collider.gameObject.layer == (int)Define.LayerMask.Ground && (hit.point - transform.position).magnitude >= 1.2f)
                        {
                            lockGarget = null;
                            _stopAtk = true;
                        }
                    }
                    break;
                }
            case Define.MouseEvent.PointerUp:
                {
                    _stopAtk = true;
                    break;
                }
            case Define.MouseEvent.Click:
                {

                    break;
                }
            case Define.MouseEvent.Press:
                {
                    if (lockGarget != null)
                    {
                        myState = Define.State.Move;
                        destPos = lockGarget.transform.position;
                    }
                    else
                    {
                        if(rayHit)
                        {
                            myState = Define.State.Move;
                            destPos = hit.point;
                        }
                    }
                    break;
                }
        }
    }


    protected override void LookAtEvent()
    {
        Vector3 dir;
        dir = destPos - transform.position;
        Vector3 FrezzingXZ = Quaternion.Slerp(Quaternion.Euler(transform.rotation.eulerAngles), Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime).eulerAngles;
        FrezzingXZ = new Vector3(0.0f, FrezzingXZ.y, 0.0f);
        transform.rotation = Quaternion.Euler(FrezzingXZ);
    }

    protected override void MoveEvent()
    {

        LookAtEvent();
        if (lockGarget != null)
        {
            float _destEnemy = (destPos - transform.position).magnitude;
            
            if (_destEnemy <= 2.0f)
            {
                if (lockGarget.gameObject.layer == (int)Define.LayerMask.Item)
                {
                    DropItem lootItem = lockGarget.GetComponent<DropItem>();
                    if (UIManager.instance.Inventory.GetComponent<Inventory>().AddItme(lootItem.item, lootItem.dropCost) == Define.Success)
                    {
                        transform.LookAt(destPos, Vector3.up);
                        Destroy(lockGarget);
                        lockGarget = null;
                        navi.isStopped = true;
                        ChangeAnim(Define.State.Idle);
                    }
                }
                else if(lockGarget.gameObject.layer == (int)Define.LayerMask.Npc)
                {
                    lockGarget = null;
                    navi.isStopped = true;
                    ChangeAnim(Define.State.Idle);
                }
                else
                {
                    transform.LookAt(destPos, Vector3.up);
                    navi.isStopped = true;
                    ChangeAnim(Define.State.NormalAtk);
                }
                return;
            }
        }
        ChangeAnim(Define.State.Move);
        navi.SetDestination(destPos);
        if (navi.isStopped)
            navi.isStopped = false;


        if (navi.remainingDistance <= 0.2f && navi.velocity.magnitude >= 0.2f)
        {
            movePoint.SetActive(false);
            ChangeAnim(Define.State.Idle);
            return;
        }
    }
    void KeyDownEvent()
    {
        if (isDie)
            return;

        if(Input.GetKey(KeyCode.Alpha1))
        {
            UIManager.instance.UseSlot(KeyCode.Alpha1, this);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UIManager.instance.UseSlot(KeyCode.Alpha2, this);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UIManager.instance.UseSlot(KeyCode.Alpha3, this);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UIManager.instance.UseSlot(KeyCode.Alpha4, this);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UIManager.instance.UseSlot(KeyCode.Alpha5, this);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            UIManager.instance.UseSlot(KeyCode.Alpha6, this);
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            _myStat.TakeDamage(10);
        }
        else if(Input.GetKeyDown(KeyCode.I))
        {
            UIManager.instance.OnOffUI(Define.UIType.Inventory);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            UIManager.instance.OnOffUI(Define.UIType.Stat);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            UIManager.instance.OnOffUI(Define.UIType.Skill);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if(lootItem != null)
            {
                UIManager.instance.Inventory.AddItme(lootItem.GetComponent<DropItem>().item);
                Destroy(lootItem);
            }
        }
    }

    bool TargetIsDieCheck()
    {
        return lockGarget.GetComponent<BaseContoller>()._isDie;
    }

//--------------------------------Animation Event--------------------------------------//
    void SetIdel()
    {
        ChangeAnim(Define.State.Idle);
    }

    void OnhitEvent()
    {
        lockGarget.GetComponent<StatBase>().TakeDamage(myStat.AtkDamage);

        if (_stopAtk || lockGarget.GetComponent<StatBase>().Hp <= 0)
        {
            if (lockGarget != null)
                lockGarget = null;
            ChangeAnim(Define.State.Idle);
        }
        else
        {
            ChangeAnim(Define.State.NormalAtk);
        }
    }

    public void UseNoneTargetSkill(Define.State _state)
    {
        if (myState == _state)
            return;

        navi.isStopped = true;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 100.0f);
        transform.LookAt(hit.point, Vector3.up);
        ChangeAnim(_state);
    }
    void BoomAtkEvent()
    {
        SkillDataBase.instance.boomAtk.gameObject.SetActive(true);
        SkillDataBase.instance.boomAtk.Use();
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == (int)Define.LayerMask.Item)
        {
            lootItem = other.gameObject;
        }
    }
}
