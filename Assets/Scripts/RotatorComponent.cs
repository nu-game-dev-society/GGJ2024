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
        this.rotationRoot.Rotate(this.rotationAmount * Time.fixedDeltaTime);
    }

    public void ApplyRotationFuzz(Vector3 fuzzAmount)
    {
        this.rotationAmount = Vector3.Scale(this.rotationAmount, fuzzAmount);
    }
}
