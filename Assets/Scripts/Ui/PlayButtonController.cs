using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        bool isGame = SceneManager.GetActiveScene().name == "Game";
        GetComponent<TextMeshProUGUI>().text = isGame ? "Resume" : "Play";
    }
}
