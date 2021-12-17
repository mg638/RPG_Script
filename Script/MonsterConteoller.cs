using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterConteoller : BaseContoller
{
    [SerializeField]
    float rotSpeed = 5;

    [SerializeField]
    float ScanRange = 10;

    [SerializeField]
    float AttackRange = 1.0f;

    GameObject Player;

    bool _PlayerIsOnAtkRange = false;

    bool _isReturn = false;

    [SerializeField]
    Vector3 LootPos;
    protected override void Init()
    {
        myAnim = GetComponent<Animator>();
        navi = gameObject.GetComponent<NavMeshAgent>();
        navi.updateRotation = false;
        myStat = transform.GetComponent<StatBase>();
        myStat.Init();
        myState = Define.State.Idle;
        navi.speed = myStat.MoveSpeed;
        Player = GameObject.FindGameObjectWithTag("Player");
        WorldObjectType = Define.WorldObject.Monster;
        LootPos = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    protected override void LookAtEvent()
    {
        Vector3 dir;
        if(_isReturn)
        dir = LootPos - transform.position;
        else
        dir = Player.transform.position - transform.position;
        Vector3 FrezzingXZ = Quaternion.Slerp(Quaternion.Euler(transform.rotation.eulerAngles), Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime).eulerAngles;
        FrezzingXZ = new Vector3(0.0f, FrezzingXZ.y, 0.0f);
        transform.rotation = Quaternion.Euler(FrezzingXZ);

    }

    protected override void MoveEvent()
    {
        LookAtEvent();
        ChangeAnim(Define.State.Move);

        if ((LootPos - transform.position).magnitude > 15.0f)
        {
            if(!_isReturn)
            _isReturn = true;
        }

        if (navi.isStopped)
            navi.isStopped = false;

        if (_isReturn)
        {
            navi.SetDestination(LootPos);

            if (navi.remainingDistance <= 0.2f && navi.velocity.magnitude >= 0.2f)
            {
                ChangeAnim(Define.State.Idle);
                _isReturn = false;
                _PlayerIsOnAtkRange = false;
                navi.isStopped = true;
                return;
            }
        }
        else
        {
            navi.SetDestination(Player.transform.position);

            if (navi.remainingDistance <= AttackRange && navi.velocity.magnitude >= AttackRange)
            {
                ChangeAnim(Define.State.Idle);
                _PlayerIsOnAtkRange = true;
                navi.isStopped = true;
                return;
            }
        }


    }

    protected override void IdleEvent()
    {
        if (Player == null)
            return;


        if(myStat.Hp <= 0)
        {
            ChangeAnim(Define.State.Die);
        }

        float disinte = (Player.transform.position - transform.position).magnitude; ;

        if (disinte <= ScanRange && _PlayerIsOnAtkRange == false && !Player.transform.GetComponent<PlayerContoller>()._isDie)
        {
            ChangeAnim(Define.State.Move);
        }
    }

    void OnHitEvent()
    {
        Player.GetComponent<StatBase>().TakeDamage(myStat.AtkDamage);
        ChangeAnim(Define.State.Idle);
    }    

    void AtkRangeCheckEvent()
    {
        if((transform.position - Player.transform.position).magnitude <= AttackRange && !Player.transform.GetComponent<PlayerContoller>()._isDie)
        {
            transform.LookAt(Player.transform.position);
            ChangeAnim(Define.State.NormalAtk);
        }
        else
        {
            _PlayerIsOnAtkRange = false;
        }
    }

}
