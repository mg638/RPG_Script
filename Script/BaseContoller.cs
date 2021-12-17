using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseContoller : MonoBehaviour
{

    protected Define.State myState;

    protected NavMeshAgent navi;

    protected Animator myAnim;

    protected Vector3 destPos;

    protected StatBase myStat;

    protected bool isDie = false;

    [SerializeField]
    private int dropExp;

    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

    protected abstract void Init();
    // Update is called once per fram

    private void LateUpdate()
    {
        DieCheck();
    }

    void Update()
    {
        StateEvent();
    }
    protected void StateEvent()
    {
        switch (myState)
        {
            case Define.State.Idle:
                IdleEvent();
                break;
            case Define.State.Move:
                MoveEvent();
                break;
            case Define.State.NormalAtk:

                break;
        }
        
    }

    protected virtual void LookAtEvent() { }

    protected virtual void MoveEvent() { }

    protected virtual void IdleEvent() { }

    void DieCheck() 
    { 
        if(myStat.Hp <= 0 && !isDie)
        {
            GameManager.cursorController.ChangeBasicCursor();
            navi.isStopped = true;
            isDie = true;
            ChangeAnim(Define.State.Die);
            if (WorldObjectType == Define.WorldObject.Monster)
            {
                gameObject.GetComponent<DropTable>().Drop();
                StartCoroutine(EnemyDestroy());
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>().exp += dropExp;
            }
                
        }
    }

    protected void ChangeAnim(Define.State _state)
    {
        myState = _state;
        myAnim.SetInteger("State", (int)myState);
    }

    public bool _isDie { get { return isDie; } set { isDie = _isDie; } }

    IEnumerator EnemyDestroy()
    {
        yield return new WaitForSeconds(5.0f);
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);

        StartCoroutine(transform.parent.transform.parent.GetComponent<SpawnManager>().DestroyMonster(gameObject));
    }

    public StatBase _myStat { get { return myStat; } set { myStat = _myStat;} }


}
