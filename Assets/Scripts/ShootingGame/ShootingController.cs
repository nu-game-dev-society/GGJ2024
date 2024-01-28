using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ShootingController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] GameObject cupsPrefab;
    [SerializeField] float bulletSpeed = 20f;
    [SerializeField] int bullets = 10;

    [Header("Objects")]
    [SerializeField] GameObject shootingCamera;
    [SerializeField] Transform cupsSpawn;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] ControlsManager controlsManager;

    [Header("Events")]
    [SerializeField] UnityEvent onSuccess;
    [SerializeField] UnityEvent onFailure;

    private Transform cameraTransform;
    private GameObject currentCups;
    private int bulletsLeft;
    private Vector3 currentRotation;
    private Vector2 look;

    private List<Collider> cups;
    private List<GameObject> bulletObjects = new();

    // Start is called before the first frame update
    void Start()
    {
        controlsManager.controls.Gameplay.Shoot.performed += Shoot_performed;

        cameraTransform = shootingCamera.transform;

        currentRotation = cameraTransform.localRotation.eulerAngles;

        Reset();
    }

    private void Reset()
    {
        bulletsLeft = bullets;

        foreach (var bulletObject in bulletObjects)
        {
            Destroy(bulletObject);
        }

        bulletObjects.Clear();

        if (currentCups) Destroy(currentCups);
        currentCups = Instantiate(cupsPrefab, cupsSpawn);
        cups = currentCups.GetComponentsInChildren<Collider>().ToList();
    }

    private void OnEnable()
    {
        shootingCamera.SetActive(true);
        Reset();
    }

    private void OnDisable()
    {
        shootingCamera.SetActive(false);

        // Wait 1s to let things not break
        StartCoroutine(ResetLater());
    }


    private IEnumerator ResetLater()
    {
        yield return new WaitForSeconds(1);
        Reset();
    }

    private void Shoot_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!enabled) return; // This fixes some broken bullshit

        if (bulletsLeft <= 0)
        {
            onFailure.Invoke();
        }

        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        bulletObjects.Add(bullet);

        bulletsLeft--;
    }
    void Update()
    {
        look = controlsManager.GetLook();
        currentRotation.x -= look.y;
        currentRotation.x = Mathf.Clamp(currentRotation.x, -70f, 70f);
        currentRotation.y += look.x;
        currentRotation.y = Mathf.Clamp(currentRotation.y, -70f, 70f);

        cameraTransform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void CupLeave(Collider cup)
    {
        cups.Remove(cup);

        if (cups.Count == 0)
        {
            onSuccess.Invoke();
        }
    }
}
