using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public HudManager HudManager;
    public ControlsManager ControlsManager;
    private int TicketCount = 3;
    public GameObject PauseScreen;
    public PlayerController PlayerController;
    private AudioSource Audio;
    public UnityEvent gameOver;

    public bool gameIsOver = false;

    [SerializeField]
    private AudioClip[] clownLaughs;

    private void Awake()
    {
        if (HudManager == null)
        {
            var h = FindObjectOfType<HudManager>();
            HudManager = h != null ? h : throw new System.Exception("Missing HUD");
        }
        if (ControlsManager == null)
        {
            var c = GetComponent<ControlsManager>();
            ControlsManager = c != null ? c : throw new System.Exception("Missing Control Manager");
        }
        if (Audio == null)
        {
            var c = GetComponent<AudioSource>();
            Audio = c != null ? c : throw new System.Exception("Missing Audio Source");
        }
        HudManager.DisplayTickets(TicketCount);
    }
    private void Start()
    {
        if (ControlsManager != null)
        {
            ControlsManager.controls.Gameplay.Pause.performed += TogglePause;
        }

    }
    private bool paused = false;
    private void TogglePause(InputAction.CallbackContext ctx)
    {
        Debug.Log("Paused");
        if (paused) Resume();
        else Pause();
    }
    public void ReceiveTickets(int tickets)
    {
        TicketCount += tickets;

        // Play laugh if lost tickets
        if (tickets < 0 && clownLaughs.Length > 0) {
            int laugh = Random.Range(0, clownLaughs.Length);
            Audio.clip = clownLaughs[laugh];
            Audio.Play();
        }

        HudManager.ReceiveTickets(tickets);
        HudManager.DisplayTickets(TicketCount);
        if (TicketCount <= 0)
        {
            gameIsOver = true;
            gameOver.Invoke();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Resume()
    {
        paused = false;
        PauseScreen.SetActive(false);
        Time.timeScale = 1.0f;
        PlayerController.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Pause()
    {
        paused = true;
        PauseScreen.SetActive(true);
        Time.timeScale = 0.0f;
        PlayerController.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }
}
