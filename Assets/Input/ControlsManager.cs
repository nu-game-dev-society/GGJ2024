using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class ControlsManager : MonoBehaviour
{
    public Controls controls;
    // Start is called before the first frame update
    void Awake()
    {
        controls = new Controls();
        controls.Enable();
    }


    public Vector2 GetMovement()
    {
        return controls.Gameplay.Movement.ReadValue<Vector2>();
    }
    public Vector2 GetLook()
    {
        return controls.Gameplay.Look.ReadValue<Vector2>();
    }
    public bool GetJump()
    {
        return controls.Gameplay.Jump.ReadValue<bool>();
    }
}