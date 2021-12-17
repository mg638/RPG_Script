using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : MonoBehaviour
{
    enum GameObject
    {
        HPBar
    }

    Canvas canvas;

    StatBase stat;

    private void Start()
    {
        canvas = transform.GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        stat = transform.parent.GetComponent<StatBase>();
    }

    private void LateUpdate()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;
        if (stat != null)
            SetHpRatio(stat.Hp / (float)stat.MaxHp);
    }

    private void SetHpRatio(float ratio)
    {
        transform.GetChild(0).GetComponent<Slider>().value = ratio;
        if (stat.Hp <= 0)
            gameObject.SetActive(false);
    }
}
