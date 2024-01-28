using UnityEngine;
using UnityEngine.Events;

public class HookADuckPoleInteractionComponent : MonoBehaviour, IInteractable
{
    public UnityEvent interactEvent = new UnityEvent(); 
    public bool CanInteract(PlayerController interactor) => !this.isInteracting;
    public bool isInteracting = false;

    public void Interact(PlayerController interactor)
    {
        interactEvent.Invoke();
        this.isInteracting = interactor.TryEquip(this.gameObject);
    }

    public string PopupText()
    {
        return "Play 'Hook A Duck'";
    }

    public bool ShouldHighlight() => !this.isInteracting;

    public float ScrollModifier;
    public float MinDistance;
    public float MaxDistance;
}
