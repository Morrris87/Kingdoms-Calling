using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
	//Put stuff in me in unity
	public GameObject loadingScreen;
	public Slider slider;
	public Text progressTex;

	//calls coroutine to start load level
	public void loadLevel(string SceneName)
	{
		StartCoroutine(loadAsync(SceneName));
	}

	//loads the level and brings up loading screen
	IEnumerator loadAsync (string SceneName)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(SceneName);

		loadingScreen.SetActive(true);

		//changes text on slider to percentage
		while(!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / .9f);

			slider.value = progress;
			progressTex.text = progress * 100f + "%";

			yield return null;
		}
	}
}
