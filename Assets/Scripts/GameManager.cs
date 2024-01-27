using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HudManager HudManager;

    private int TicketCount = 3;

    private void Awake()
    {
        if (HudManager == null)
        {
            var h = FindObjectOfType<HudManager>();
            HudManager = h != null ? h : throw new System.Exception("Missing HUD");
        }
    }
    public void ReceiveTickets(int tickets)
    {
        TicketCount += tickets;

        //play sound;

        HudManager.ReceiveTickets(tickets);
        if (TicketCount <= 0)
        {
            Debug.LogWarning("YOU ARE A LOSER");
        }
    }
}
