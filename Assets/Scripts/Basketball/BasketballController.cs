using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class BasketballController : MonoBehaviour
{
    [SerializeField] GameObject basketball;

    [Header("Settings")]
    [SerializeField] float accuracyRequired = 0.15f;
    [SerializeField] float speed = 1f;

    [Header("UI")]
    [SerializeField] BasketballUI basketballUI;

    [Header("Splines")]
    [SerializeField] SplineContainer splineLeft;
    [SerializeField] SplineContainer splineCenter;
    [SerializeField] SplineContainer splineRight;

    // TODO Remove when gamemanager exists
    [Header("Temp")]
    [SerializeField] ControlsManager controlsManager;

    private SplineAnimate basketballSplineAnimate;
    private Rigidbody basketballRigidbody;
    private bool throwing = false;
    private bool waiting = false;
    private float time = 0;

    private float CurrentPct => (Mathf.Sin(time * speed) + 1) / 2;

    // Start is called before the first frame update
    void Start()
    {
        controlsManager.controls.Gameplay.Jump.performed += Throw_performed; ;

        basketballSplineAnimate = basketball.GetComponent<SplineAnimate>();
        basketballRigidbody = basketball.GetComponent<Rigidbody>();

        basketballSplineAnimate.Updated += BasketballSplineAnimate_Updated;
    }

    private void BasketballSplineAnimate_Updated(Vector3 arg1, Quaternion arg2)
    {
        // Reset the ball once animation is over
        if (!basketballSplineAnimate.IsPlaying && throwing && !waiting)
        {
            waiting = true;
            StartCoroutine(BallDrop());
        }
    }

    private IEnumerator BallDrop()
    {
        basketballRigidbody.isKinematic = false;

        // Let the ball drop for 2s
        yield return new WaitForSeconds(2);

        basketballRigidbody.isKinematic = true;

        // Reset everything for next throw
        throwing = false;
        basketballSplineAnimate.Restart(false);
        waiting = false;
    }

    private void Throw_performed(InputAction.CallbackContext obj)
    {
        if (throwing) return;

        float lower = 0.5f - accuracyRequired;
        float upper = 0.5f + accuracyRequired;
        float current = CurrentPct;
        //Debug.Log($"{lower} < {current} > {upper}");
        if (current > lower && current < upper)
        {
            basketballSplineAnimate.Container = splineCenter;
            Debug.Log("Success");
        }
        else
        {
            bool right = current < 0.5f;
            Debug.Log("Failure!");
            basketballSplineAnimate.Container = right ? splineRight : splineLeft;
        }

        basketballSplineAnimate.Play();
        throwing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!throwing && !waiting)
        {
            float targetX = Mathf.Lerp(basketballUI.barStart.position.x, basketballUI.barEnd.position.x, CurrentPct);
            basketballUI.ball.position = new Vector3(targetX, basketballUI.ball.position.y, basketballUI.ball.position.z);

            time += Time.deltaTime;
        }
    }
}
