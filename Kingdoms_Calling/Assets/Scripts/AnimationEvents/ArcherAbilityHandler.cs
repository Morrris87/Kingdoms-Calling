using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAbilityHandler : MonoBehaviour
{
    public AudioSource characterAudioSource;
    public AudioSource arrowVolleySource;
    public AudioSource piercingArrowSource;
    public AudioClip footstepClip;
    public AudioClip arrowVolleyClip;
    public AudioClip piercingArrowClip;
    public AudioClip evasionClip;

    private ArrowVolley arrowVolley;
    private PiercingArrow piercingArrow;

    // Start is called before the first frame update
    void Start()
    {
        arrowVolley = GetComponentInParent<ArrowVolley>();
        piercingArrow = GetComponentInParent<PiercingArrow>();
    }

    public void StepEvent()
    {
        if (footstepClip != null)
        {
            characterAudioSource.clip = footstepClip;
            characterAudioSource.Play();
        }
    }

    public void EvasionAEvent()
    {
        if (evasionClip != null)
        {
            characterAudioSource.clip = evasionClip;
            characterAudioSource.Play();
        }
    }
    public void ArrowVolleyEvent()
    {
        // Place the collder for the ability in the spawn area
        Instantiate(arrowVolley.areaOfEffect, arrowVolley.colliderDestPos.position, Quaternion.identity);
    }

    public void ArrowVolleySoundEvent()
    {
        if (arrowVolleyClip != null)
        {
            arrowVolleySource.clip = arrowVolleyClip;
            arrowVolleySource.Play();
        }
    }

    public void PiercingArrowEvent()
    {
        // Play audio
        if (piercingArrowClip != null)
        {
            piercingArrowSource.clip = piercingArrowClip;
            piercingArrowSource.Play();
        }

        // Set the arrow's rotation to that of the player
        piercingArrow.piercingArrowPrefab.transform.rotation = transform.rotation;

        // Set the attacker to the player
        piercingArrow.piercingArrowPrefab.GetComponent<ProjectileDamage>().attacker = ProjectileDamage.Attacker.PLAYER;

        // Fire the piercing arrow shot
        Instantiate(piercingArrow.piercingArrowPrefab, GetComponentInParent<BasicAttack>().spawner.position, Quaternion.LookRotation(transform.forward, Vector3.up));
    }
}
