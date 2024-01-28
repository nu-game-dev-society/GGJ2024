using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookADuckGameZone : MonoBehaviour
{
    public Transform poleSocket;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<HookADuckPoleHookComponent>() is HookADuckPoleHookComponent hookComponent)
        {
            ForceExitGameZone(hookComponent);
        }
    }

    public void ForceExitGameZone(HookADuckPoleHookComponent hookComponent)
    {
        FindObjectOfType<PlayerController>().UnEquip();
        hookComponent.parentPoleTransform.parent = poleSocket;
        hookComponent.parentPoleTransform.localPosition = Vector3.zero;
        hookComponent.parentPoleTransform.localRotation = Quaternion.identity;
    }
}
