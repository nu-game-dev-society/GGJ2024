using UnityEngine;

public class BobComponent : MonoBehaviour
{
    [SerializeField]
    private float bobAmount = 1f;

    [SerializeField]
    private float bobSpeed = 1f;

    private readonly ComponentCache componentCache = new ComponentCache();

    private new Transform transform => this.componentCache.GetComponent<Transform>();

    void Start()
    {
        
    }

    void Update()
    {
        var yPos = Mathf.Sin(Time.time * bobSpeed) * bobAmount;
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, yPos, this.transform.localPosition.z);
    }

    public void ApplyBobAmountFuzz(float fuzzAmount)
    {
        this.bobAmount *= fuzzAmount;
    }

    public void ApplyBobSpeedFuzz(float fuzzAmount)
    {
        this.bobSpeed *= fuzzAmount;
    }
}
