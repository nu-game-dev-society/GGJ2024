using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupGameInteractable : MonoBehaviour, IInteractable
{
    public int CupNumber;
    public CupGameManager Manager;

    public bool CanInteract(PlayerController interactor) => Manager.CanPick();


    public void Interact(PlayerController interactor)
    {
        StartCoroutine(Manager.SubmitAnswer(CupNumber));
    }

    public string PopupText()
    {
        return "Cup " + CupNumber;
    }

    public bool ShouldHighlight() => Manager.CanPick();

}
