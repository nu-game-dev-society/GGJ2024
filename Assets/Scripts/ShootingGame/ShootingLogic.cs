using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Detection : MonoBehaviour
{
    [SerializeField] UnityEvent<Collider> onLeave;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Can"))
        {
            onLeave.Invoke(other);
            //Debug.Log(other.name + "has left the area");
        }
    }
}


