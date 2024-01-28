using System.Linq;
using UnityEngine;

public class DuckComponent : MonoBehaviour
{
    public HookADuckMiniGameManager duckMiniGameManager;
    public Transform duckTransform;

    private readonly ComponentCache componentCache = new ComponentCache();

    void Start()
    {
        this.componentCache.Populate(this.gameObject);

        float fuzzDelta = 0.25f;
        float fuzzAmount = Random.Range(1 - fuzzDelta, 1 + fuzzDelta);

        var fuzzAmountVector = Vector3.up * fuzzAmount;
        if (new System.Random().NextDouble() >= 0.5)
        {
            fuzzAmountVector *= -1f;
        }
        this.componentCache.GetComponents<RotatorComponent>().ElementAt(1).ApplyRotationFuzz(fuzzAmountVector);

        this.componentCache.GetComponent<BobComponent>().ApplyBobAmountFuzz(fuzzAmount);
        this.componentCache.GetComponent<BobComponent>().ApplyBobSpeedFuzz(fuzzAmount);
    }

    public void OnPickup(HookADuckPoleHookComponent hookComponent)
    {
        var duckyboi = Instantiate(this.duckTransform.GetChild(0).gameObject);
        this.duckTransform.gameObject.SetActive(false);

        duckyboi.transform.parent = hookComponent.hookTransform;
        duckyboi.transform.localPosition = Vector3.zero;
        duckyboi.transform.localRotation = Quaternion.identity;

        duckMiniGameManager.AcquireDuck(this);
        Destroy(duckyboi,1.0f);

        this.componentCache.GetComponents<RotatorComponent>().ElementAt(0).enabled = false;
        this.componentCache.GetComponents<RotatorComponent>().ElementAt(1).enabled = false;
        this.componentCache.GetComponent<BobComponent>().enabled = false;
    }
}
