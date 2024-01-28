using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    float xRotation = 0f;

    CharacterController controller;
    [SerializeField]
    ControlsManager controls;

    private Camera playerCamera;

    [SerializeField]
    private AudioSource asFootsteps;
    [SerializeField] private float stepSpeed = 0.3f;
    private float nextStepTime = 0f;
    [SerializeField]
    private Vector2 move;
    [SerializeField]
    private Vector2 look;

    private GameObject equippedObject = null;
    private HookADuckPoleInteractionComponent equippedHookADuckPole;

    [SerializeField]
    private GameObject hookADuckPoleSocket;

    void Start()
    {
        playerCamera = Camera.main;

        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        xRotation = playerCamera.transform.localRotation.x;
    }

    void GetInputs()
    {
        move = controls.GetMovement();
        look = controls.GetLook();
    }

    void Update()
    {
        GetInputs();

        float mouseX = look.x;
        float mouseY = look.y;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);

        Vector3 moveDirection = (transform.right * move.x) + (transform.forward * move.y) + Physics.gravity;

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        bool moving = controller.velocity.magnitude > 0.5f;
        if (moving && Time.time >= nextStepTime)
        {
            asFootsteps.pitch = Random.Range(2f, 2.5f);
            asFootsteps.Play();
            nextStepTime = Time.time + (stepSpeed);
        }

        Vector2 scroll = controls.GetScroll();
        if (equippedHookADuckPole != null)
        {
            float newY = Mathf.Clamp(
                equippedHookADuckPole.transform.localPosition.y + (scroll.y * equippedHookADuckPole.ScrollModifier * Time.deltaTime),
                equippedHookADuckPole.MinDistance,
                equippedHookADuckPole.MaxDistance
            );

            equippedHookADuckPole.transform.localPosition = new Vector3(
                equippedHookADuckPole.transform.localPosition.x,
                newY,
                equippedHookADuckPole.transform.localPosition.z
            );
        }
    }

    public bool TryEquip(GameObject objectToEquip)
    {
        if (this.equippedObject != null)
        {
            return false;
        }

        this.equippedObject = objectToEquip;

        GameObject socket = null;
        if (equippedHookADuckPole = objectToEquip.GetComponent<HookADuckPoleInteractionComponent>())
        { // above is deliberately assignment
            socket = hookADuckPoleSocket;
        }

        socket = socket != null ? socket : playerCamera.gameObject;
        objectToEquip.transform.parent = socket.transform;
        objectToEquip.transform.localPosition = Vector3.zero;
        objectToEquip.transform.localRotation = Quaternion.Euler(Vector3.zero);
        return true;
    }

    public void UnEquip()
    {
        if (this.equippedObject.GetComponent<HookADuckPoleInteractionComponent>() is HookADuckPoleInteractionComponent interactionComponent)
        {
            interactionComponent.isInteracting = false;
        }

        this.equippedObject = null;
    }
}

