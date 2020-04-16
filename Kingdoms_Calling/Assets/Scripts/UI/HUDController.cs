//  Name: HUDController.cs
//  Author: Connor Larsen
//  Date: 1/28/2020

using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    // Public Variables
    public GameObject[] players;    // Array for the 4 players
    public GameObject[] playerHUD;  // Array for the 4 player's HUDs

    public GameObject gameHUD;      // Drag game HUD panel here in inspector
    public GameObject pauseScreen;  // Drag pause screen panel here in inspector

    public static bool isPaused;    // Bool to keep track of whether game is paused

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;   // When game starts up, game is running
    }

    // Update is called once per frame
    void Update()
    {
        if(players.Length == 4)
        {
            if (playerHUD[0].GetComponent<CharacterManager>().isdead == true && playerHUD[1].GetComponent<CharacterManager>().isdead == true && playerHUD[2].GetComponent<CharacterManager>().isdead == true && playerHUD[3].GetComponent<CharacterManager>().isdead == true)
            {
                SceneManager.LoadScene("LoseScreen");
            }
        }
    }

	public void QuitGame()
	{
		Application.Quit();
		Debug.Log("Quit");
	}

    public void PauseGame()
    {
        if (isPaused)   // If game is paused...
        {
            // Swap canvas panels
            gameHUD.SetActive(true);
            pauseScreen.SetActive(false);

            // Turn time scale on
            Time.timeScale = 1f;

            // Game is running, set isPaused to false
            isPaused = false;
        }
        else
        {
            // Swap canvas panels
            gameHUD.SetActive(false);
            pauseScreen.SetActive(true);

            // Turn time scale off
            Time.timeScale = 0f;

            // Game is paused, set isPaused to true
            isPaused = true;
        }
    }
}