using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InteractionController : MonoBehaviour
{
    public LayerMask layers;
    public float interactionReach;
    [SerializeField] ControlsManager ControlsManager;
    PlayerController player;
    public IInteractable currentInteractable;
    [ColorUsage(true, true)]
    public Color EmissiveColor;

    [SerializeField]
    private Camera mainCamera;
    private bool use = false;

    void Awake()
    {
    }

    void Start()
    {
        this.player = GetComponent<PlayerController>();
        ControlsManager.controls.Gameplay.Use.performed += Use;
        ControlsManager.controls.Gameplay.Use.canceled += Use;

        if (mainCamera == null)
            mainCamera = Camera.main != null ? Camera.main : throw new System.Exception("Missing Camera");
    }

    public void Use(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            use = true;
        }
        if (ctx.phase == InputActionPhase.Canceled)
        {
            use = false;
        }
    }

    private void Update()
    {
        if (currentInteractable?.CanInteract(this.player) == false)
        {
            SwitchInteractable(null);
        }

        if (
            Physics.Raycast(new Ray(mainCamera.transform.position, mainCamera.transform.forward), out RaycastHit hit, interactionReach, layers)
            &&
            hit.transform.TryGetComponent(out IInteractable interactable)
        )
        {
            if (interactable != currentInteractable && interactable.CanInteract(this.player))
            {
                SwitchInteractable(interactable);
            }
        }
        else if (currentInteractable != null)
            SwitchInteractable(null);

        if (currentInteractable != null && use)
            currentInteractable.Interact(this.player);
    }

    private void SwitchInteractable(IInteractable interactable)
    {
        if ((currentInteractable as UnityEngine.Object) != null)
            UnhighlightInteractable(currentInteractable);
        currentInteractable = interactable;
        if (currentInteractable != null && currentInteractable.ShouldHighlight())
            HighlightInteractable(currentInteractable);
        //Debug.Log(currentInteractable);
    }

    private void HighlightInteractable(IInteractable interactable)
    {
        IEnumerable<Material> mats = (interactable as MonoBehaviour).GetMaterials();
        foreach (Material m in mats)
        {
            m.EnableKeyword("_EMISSION");
            m.SetColor("_EmissionColor", EmissiveColor);
        }
    }
    private void UnhighlightInteractable(IInteractable interactable)
    {
        IEnumerable<Material> mats = (interactable as MonoBehaviour).GetMaterials();
        foreach (Material m in mats)
        {
            m.DisableKeyword("_EMISSION");
        }
    }
    private void OnDisable()
    {
        currentInteractable = null;
    }
}
