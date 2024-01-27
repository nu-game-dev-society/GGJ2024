using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    private InteractionController interactionController;
    public TextMeshProUGUI interactText;
    // Start is called before the first frame update
    void Start()
    {
        interactionController = FindObjectOfType<InteractionController>();
    }

    // Update is called once per frame
    void Update()
    {
        interactText.text = interactionController.currentInteractable?.PopupText() ?? "";
    }
}
