using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LichHandler : MonoBehaviour
{
    // Public Variables
    public GameObject magicProjectile;
    public Transform spawner;

    public Image UIFade;

   
    public AudioSource lichAudioSource;
    public AudioClip lichScreamClip;
    public AudioClip lichMeleeClip;
    public AudioClip lichMagicClip;
    public AudioClip LichCloneClip;

    private bool changeScenes;
    private float fadeLength = 2f;
    private float fadeTimer;
    // Start is called before the first frame update
    void Start()
    {
        changeScenes = false;
        fadeTimer = fadeLength;
    }

    void Update()
    {
        if (changeScenes)
        {
            fadeTimer -= Time.deltaTime;
        }

        if (fadeTimer <= 0f)
        {
            if(SceneManager.GetActiveScene().name == "BOSSFIGHT1")
            {
                SceneManager.LoadScene("GraveYardMap");
            }
            else if (SceneManager.GetActiveScene().name == "GraveYardMap")
            {
                SceneManager.LoadScene("GraveYardMap BOSS");
            }
            else if (SceneManager.GetActiveScene().name == "GraveYardMap BOSS")
            {
                SceneManager.LoadScene("GraveYardMap BOSS DEFEATED_FLAT");
            }
            else if (SceneManager.GetActiveScene().name == "BOSSFIGHT3THRONE")
            {
                SceneManager.LoadScene("WinScreen");
            }
        }
    }

    public void DeathEvent()
    {
        // Fade Screen to Black
        UIFade.color = Color.black;
        UIFade.canvasRenderer.SetAlpha(0.0f);
        UIFade.CrossFadeAlpha(1.0f, fadeLength, false);
        changeScenes = true;
    }

    public void SceneSwitchEvent()
    {
        // When the Lich dies, transfer the players to the next scene
        //SceneManager.LoadScene("GraveYardMap");
    }

    public void LichMeleeEvent()
    {
        if (lichMeleeClip != null)
        {
            lichAudioSource.clip = lichMeleeClip;
            lichAudioSource.Play();
        }
    }

    public void LichScreamEvent()
    {
        if (lichMeleeClip != null)
        {
            lichAudioSource.clip = lichScreamClip;
            lichAudioSource.Play();
        }
    }
    public void LichMagicEvent()
    {
        if (lichMeleeClip != null)
        {
            lichAudioSource.clip = lichMagicClip;
            lichAudioSource.Play();
        }
    }
    public void LichCloneEvent()
    {
        if (lichMeleeClip != null)
        {
            lichAudioSource.clip = LichCloneClip;
            lichAudioSource.Play();
        }
    }
    public void MagicAttackEvent()
    {
        Instantiate(magicProjectile, spawner.position, Quaternion.LookRotation(transform.forward, Vector3.up)); // Fire the projectile
    }
}