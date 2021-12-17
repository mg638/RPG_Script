using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBookMain : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UI_OnOff()
    {
        if (gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            Close();
        }
        else
        {
            Open();
            transform.SetAsLastSibling();
        }
    }

    public void Close()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        Define.rayContoller = Define.RayContoller.Start;
    }

    public void Open()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
