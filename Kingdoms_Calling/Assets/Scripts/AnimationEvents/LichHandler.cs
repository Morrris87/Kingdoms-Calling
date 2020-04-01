﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LichHandler : MonoBehaviour
{
    // Public Variables
    public GameObject magicProjectile;
    public Transform spawner;


   
    public AudioSource lichAudioSource;
    public AudioClip lichScreamClip;
    public AudioClip lichMeleeClip;
    public AudioClip lichMagicClip;
    public AudioClip LichCloneClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DeathEvent()
    {
        // When the Lich dies, transfer the players to the next scene
        SceneManager.LoadScene("GraveYardMap");
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