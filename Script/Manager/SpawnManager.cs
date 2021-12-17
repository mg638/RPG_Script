using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    Transform[] SpawnParent;

    [SerializeField]
    Define.MonsterType monsterType;

    [SerializeField]
    GameObject[] monsterGroup;

    [SerializeField]
    float SpwnTime = 5.0f;

    [SerializeField]
    bool[] isSpawn;

    // Start is called before the first frame update
    void Start()
    {
        SpawnParent = new Transform[gameObject.transform.childCount];
        monsterGroup = new GameObject[gameObject.transform.childCount];
        isSpawn = new bool[gameObject.transform.childCount];
        for (int idx = 0; idx < gameObject.transform.childCount; idx++)
        {
            SpawnParent[idx] = gameObject.transform.GetChild(idx);
            isSpawn[idx] = false;
        }
        ReSpawn();
    }

    // Update is called once per frame
    void Update()
    {

    }


    GameObject Spawn(string path)
    {
        return Resources.Load<GameObject>("Prefab/" + path);
    }

    void ReSpawn()
    {
        int MaxCnt = gameObject.transform.childCount;

        for(int idx = 0; idx < SpawnParent.Length; idx++)
        {
            if(monsterGroup[idx] == null)
            {
                monsterGroup[idx] = Instantiate(Spawn(monsterType.ToString()), SpawnParent[idx]);
            }
        }
    }

    public IEnumerator DestroyMonster(GameObject obj)
    {
        yield return new WaitForSeconds(5.0f);
        for (int idx = 0; idx < SpawnParent.Length; idx++)
        {
            if (monsterGroup[idx] == obj)
            {
                monsterGroup[idx] = null;
            }
        }
        Destroy(obj);
        ReSpawn();
    }

    

}
