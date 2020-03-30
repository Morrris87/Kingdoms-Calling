using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winner_Loser_Screen : MonoBehaviour
{
	public void OnApplicationQuit()
	{
		Debug.Log("Quit");
		Application.Quit();
	}

	public void BackToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
