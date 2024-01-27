using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class BasketballController : MonoBehaviour
{
    [SerializeField] GameObject basketball;

    [Header("Settings")]
    [SerializeField]
    [Range(0f, 1f)]
    float accuracyRequired = 0.15f;
    [SerializeField] float minSpeed = 1.5f;
    [SerializeField] float maxSpeed = 3f;

    [Header("Objects")]
    [SerializeField] BasketballUI basketballUI;
    [SerializeField] GameObject basketballCamera;
    [SerializeField] ControlsManager controlsManager;

    [Header("Splines")]
    [SerializeField] SplineContainer splineLeft;
    [SerializeField] SplineContainer splineCenter;
    [SerializeField] SplineContainer splineRight;

    [Header("Events")]
    [SerializeField] UnityEvent onSuccess;
    [SerializeField] UnityEvent onFailure;

    private SplineAnimate basketballSplineAnimate;
    private Rigidbody basketballRigidbody;
    private bool throwing;
    private bool waiting;
    private float time;
    private float speed;

    private float CurrentPct => (Mathf.Sin(time) + 1) / 2;

    private void OnEnable()
    {
        basketballUI.gameObject.SetActive(true);
        basketballCamera.SetActive(true);

        Reset();
    }

    private void OnDisable()
    {
        // If the UI is gone then game is stopping
        if (basketballUI.IsDestroyed()) return;

        basketballUI.gameObject.SetActive(false);
        basketballCamera.SetActive(false);

        Reset();
    }

    private void Reset()
    {
        // Reset the time so we start from the center
        time = 0;

        // Reset other vars
        throwing = false;
        waiting = false;

        // Reset ball
        if (basketballRigidbody != null) basketballRigidbody.isKinematic = true;
        if (basketballSplineAnimate != null) basketballSplineAnimate.Restart(false);

        // Randomise the speed
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Start()
    {
        controlsManager.controls.Gameplay.Jump.performed += Throw_performed;

        basketballSplineAnimate = basketball.GetComponent<SplineAnimate>();
        basketballRigidbody = basketball.GetComponent<Rigidbody>();

        basketballSplineAnimate.Updated += BasketballSplineAnimate_Updated;

        Reset();
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

        // Randomise the speed
        speed = Random.Range(minSpeed, maxSpeed);
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
            onSuccess.Invoke();
        }
        else
        {
            bool right = current < 0.5f;
            basketballSplineAnimate.Container = right ? splineRight : splineLeft;
            Debug.Log("Failure!");
            onFailure.Invoke();
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

            time += Time.deltaTime * speed;
        }
    }
}
