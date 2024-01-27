using UnityEngine;

public class HookADuckPoleInteractionComponent : MonoBehaviour, IInteractable
{
    public bool CanInteract(PlayerController interactor) => true;

    public void Interact(PlayerController interactor)
    {
        
    }

    public string PopupText()
    {
        return "Play 'Hook A Duck'";
    }

    public bool ShouldHighlight() => true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
