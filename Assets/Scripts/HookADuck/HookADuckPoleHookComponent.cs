using UnityEngine;

public class HookADuckPoleHookComponent : MonoBehaviour
{
    [SerializeField]
    private float timeToAcquireTarget = 3.0f;

    private HookADuckDuckHookComponent currentTargetDuckHookComponent;

    private float timeLeftUntilCurrentTargetIsAcquired;

    private void Update()
    {
        if (this.currentTargetDuckHookComponent == null)
        {
            return;
        }

        if (this.timeLeftUntilCurrentTargetIsAcquired > 0f)
        {
            Debug.Log($"Time left: {this.timeLeftUntilCurrentTargetIsAcquired}");
            this.timeLeftUntilCurrentTargetIsAcquired -= Time.deltaTime;
            return;
        }

        this.AcquireTarget(this.currentTargetDuckHookComponent);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<HookADuckDuckHookComponent>() is not HookADuckDuckHookComponent duckHookComponent)
        {
            return;
        }

        if (this.currentTargetDuckHookComponent != duckHookComponent)
        {
            this.currentTargetDuckHookComponent = duckHookComponent;
            this.ResetTimeLeftUntilCurrentTargetIsAcquired();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<HookADuckDuckHookComponent>() is not HookADuckDuckHookComponent duckHookComponent)
        {
            return;
        }

        if (this.currentTargetDuckHookComponent == duckHookComponent)
        {
            this.currentTargetDuckHookComponent = null;
        }
    }

    private void ResetTimeLeftUntilCurrentTargetIsAcquired()
    {
        Debug.Log($"Time reset!");
        this.timeLeftUntilCurrentTargetIsAcquired = this.timeToAcquireTarget;
    }

    private void AcquireTarget(HookADuckDuckHookComponent targetToAcquire)
    {
        Debug.Log($"Acquired {targetToAcquire.gameObject.name}");
        targetToAcquire.gameObject.transform.parent = this.gameObject.transform;
    }
}
