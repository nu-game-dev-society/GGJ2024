using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    private InteractionController interactionController;
    public TextMeshProUGUI interactText;

    public GameObject ticketGUI;
    public TextMeshProUGUI ticketText;
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

    public void ReceiveTickets(int tickets)
    {
        ticketText.SetText(tickets < 0 ? $"You lost {Mathf.Abs(tickets)} tickets" : $"You won {tickets} tickets");
        ticketGUI.SetActive(true);
    }
}
