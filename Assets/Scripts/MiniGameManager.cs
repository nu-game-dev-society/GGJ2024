using UnityEngine;
using UnityEngine.Events;

public class MiniGameManager : MonoBehaviour, IInteractable
{
    public string GameName;
    public UnityEvent StartGame = new UnityEvent();

    public bool CanInteract(PlayerController interactor) => true;

    public void Interact(PlayerController interactor)
    {
        StartGame?.Invoke();
    }

    public string PopupText()
    {
        return GameName;
    }

    public bool ShouldHighlight() => false;
}
