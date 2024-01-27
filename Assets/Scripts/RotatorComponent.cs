using System.Diagnostics;
using UnityEngine;

public class RotatorComponent : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationAmount;

    [SerializeField]
    private Transform rotationRoot;

    void Start()
    {
        this.rotationRoot = this.rotationRoot ?? this.transform;
    }

    void FixedUpdate()
    {
        this.rotationRoot.Rotate(rotationAmount * Time.fixedDeltaTime);
    }
}
