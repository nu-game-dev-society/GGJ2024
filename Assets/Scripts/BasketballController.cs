using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BasketballController : MonoBehaviour
{
    [SerializeField] float accuracyRequired = 0.15f;
    [SerializeField] float speed = 1f;

    [SerializeField] Transform basketball;
    [SerializeField] Transform barStart;
    [SerializeField] Transform barEnd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float newX = Mathf.Lerp(barStart.position.x, barEnd.position.x, (Mathf.Sin(Time.time * speed) + 1) / 2);
        basketball.position = new Vector3(newX, basketball.position.y, basketball.position.z);
    }
}
