using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CupGamePlayerController : MonoBehaviour
{
    [SerializeField]
    ControlsManager controls;
    Vector3 currentRotation;
    void Start()
    {
        currentRotation = transform.localRotation.eulerAngles;
    }

    Vector2 look;
    void Update()
    {
        look = controls.GetLook();
        currentRotation.z -= look.y;
        currentRotation.z = Mathf.Clamp(currentRotation.z, -70f, 70f);
        currentRotation.y += look.x;
        currentRotation.y = Mathf.Clamp(currentRotation.y, -70f, 70f);


        transform.localRotation = Quaternion.Euler(currentRotation);
    }
}
