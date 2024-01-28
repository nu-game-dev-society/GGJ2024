using UnityEngine;

public class HookADuckPoleInteractionComponent : MonoBehaviour, IInteractable
{
    public bool CanInteract(PlayerController interactor) => !this.isInteracting;
    private bool isInteracting = false;

    public void Interact(PlayerController interactor)
    {
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
