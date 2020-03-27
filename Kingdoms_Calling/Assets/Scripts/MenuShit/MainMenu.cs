/*
 * Player Input Script(Using New Input System)
 * Resource: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Installation.html
 * Created by: Bradley Williamson
 * On: 03/23/20
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject uiCanvas;
    public int maxMaxAllowedPlayers;

    private void Start()
    {
        uiCanvas = GameObject.Find("Canvas");
    }

    private void Update()
    {
        //test
        //if we dont have a ui canvas find it
        if (!uiCanvas)
        {
            uiCanvas = GameObject.Find("Canvas");
        }
        if (uiCanvas.GetComponent<PlayerInputManager>())
        {
            if (uiCanvas.GetComponent<PlayerInputManager>().playerCount > maxMaxAllowedPlayers)
            {
                if (uiCanvas.GetComponent<PlayerInputManager>().joiningEnabled == true)
                {
                    uiCanvas.GetComponent<PlayerInputManager>().DisableJoining();
                }
            }
            else if (uiCanvas.GetComponent<PlayerInputManager>().playerCount < maxMaxAllowedPlayers)
            {
                if (uiCanvas.GetComponent<PlayerInputManager>().joiningEnabled == false)
                {
                    uiCanvas.GetComponent<PlayerInputManager>().EnableJoining();
                }
            }
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


