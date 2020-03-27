using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinAbilityHandler : MonoBehaviour
{
    private HealingSpring healingSpring;
    private Taunt taunt;

    public AudioSource characterAudioSource;
    public AudioSource springsSoundSource;
    public AudioSource tauntSoundSource;
    public AudioClip springsSound;
    public AudioClip tauntSound;
    public AudioClip basicAttackSound;
    public AudioClip footstepClip;
    public AudioClip evasionClip;

    // Start is called before the first frame update
    void Start()
    {
        healingSpring = GetComponentInParent<HealingSpring>();
        taunt = GetComponentInParent<Taunt>();
    }

    public void springsSoundEvent()
    {
        if (springsSound != null)
        {
            springsSoundSource.clip = springsSound;
            springsSoundSource.Play();
        }
    }

    public void EvasionPEvent()
    {
        if (evasionClip != null)
        {
            characterAudioSource.clip = evasionClip;
            characterAudioSource.Play();
        }
    }

    public void tauntSoundEvent()
    {
        if (tauntSound != null)
        {
            tauntSoundSource.clip = tauntSound;
            tauntSoundSource.Play();
        }
    }
    public void StepEvent()
    {
        if (footstepClip != null)
        {
            characterAudioSource.clip = footstepClip;
            characterAudioSource.Play();
        }
    }
    public void BasicAttackSoundEvent()
    {
        if (basicAttackSound != null)
        {
            characterAudioSource.clip = basicAttackSound;
            characterAudioSource.Play();
        }
    }

    public void HealingSpringEvent()
    {
        // Instantiate the ability collider prefab on character location
        Instantiate(healingSpring.areaOfEffect, transform.position, Quaternion.identity);
    }

    public void TauntEvent()
    {
        // Place the collder for the ability in the spawn area
        Instantiate(taunt.areaOfEffect, transform.position, Quaternion.identity);
    }
}