using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    private InteractionController interactionController;

    [Header("Interaction")]
    public TextMeshProUGUI interactText;

    [Header("Ticket popup")]
    public GameObject ticketGUI;
    public TextMeshProUGUI ticketCollectText;

    [Header("Ticket display")]
    public TextMeshProUGUI ticketCountText;
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
        ticketCollectText.SetText(tickets < 0 ? $"You lost {Mathf.Abs(tickets)} tickets" : $"You won {tickets} tickets");
        ticketGUI.SetActive(true);
    }

    public void DisplayTickets(int tickets)
    {
        ticketCountText.SetText(tickets.ToString());
    }
}
