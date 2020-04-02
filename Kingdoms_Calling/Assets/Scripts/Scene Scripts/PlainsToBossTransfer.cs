using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlainsToBossTransfer : MonoBehaviour
{
    public Image UIFade;

    private float fadeLength = 2f;
    private float fadeTimer;
    private bool changeScenes;

    // Start is called before the first frame update
    void Start()
    {
        fadeTimer = fadeLength;
        changeScenes = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeScenes)
        {
            fadeTimer -= Time.deltaTime;
        }

        if (fadeTimer <= 0f)
        {
            ChangeScene("BOSSFIGHT1");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            changeScenes = true;
            FadeOutScreen();
        }
    }

    public void FadeOutScreen()
    {
        UIFade.color = Color.black;
        UIFade.canvasRenderer.SetAlpha(0.0f);
        UIFade.CrossFadeAlpha(1.0f, fadeLength, false);
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
