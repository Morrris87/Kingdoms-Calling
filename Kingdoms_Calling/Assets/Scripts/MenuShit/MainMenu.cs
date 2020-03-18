using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject uiCanvas;

    private void Start()
    {
        uiCanvas = GameObject.Find("Canvas");
    }

    private void Update()
    {
        //if we dont have a ui canvas find it
        if (!uiCanvas)
        {
            uiCanvas = GameObject.Find("Canvas");
        }
        if (uiCanvas.GetComponent<PlayerInputManager>())
        {
            if (uiCanvas.GetComponent<PlayerInputManager>().playerCount > 0)
                uiCanvas.GetComponent<PlayerInputManager>().DisableJoining();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}

