using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action OnMouseClick;
    public Action<Define.MouseEvent> MouseActuon;

    public Action OnKeyDwon;

    bool _pressed = false;
    float _pressedTime = 0;

    public void OnUpdate()
    {

        //키 입력이 없다면 리턴
        if (Input.anyKey  && OnMouseClick != null)
            OnMouseClick.Invoke();

        if (Input.anyKeyDown)
            OnKeyDwon.Invoke();





        if (MouseActuon != null)
        {
            if(Input.GetMouseButton(0) && Define.rayContoller == Define.RayContoller.Start)
            {

                if (!_pressed)
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                        return;
                    MouseActuon.Invoke(Define.MouseEvent.PointerDown);
                    _pressedTime = Time.time;
                }
                MouseActuon.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            { 
                if (_pressed && Define.rayContoller == Define.RayContoller.Start)
                {
                    if(Time.time < _pressedTime + 0.2f)
                    {
                        MouseActuon.Invoke(Define.MouseEvent.Click);
                    }
                    MouseActuon.Invoke(Define.MouseEvent.PointerUp);
                    _pressed = false;
                    _pressedTime = 0;
                }
                   
            }
        }
    }



}
