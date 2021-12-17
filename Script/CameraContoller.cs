using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode cameraMode;
    [SerializeField]
    Transform Target;
    private void Start()
    {
        cameraMode = Define.CameraMode.QuaterView;
        Target = FindObjectOfType<PlayerContoller>().transform;
    }


    private void LateUpdate()
    {
        transform.position = new Vector3(Target.position.x, Target.position.y + 6.0f, Target.position.z - 5.5f);
    }
}
