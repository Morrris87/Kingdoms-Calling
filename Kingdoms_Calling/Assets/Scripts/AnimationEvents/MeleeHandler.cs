using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHandler : MonoBehaviour
{
    public AudioSource characterAudioSource;
    public AudioClip basicAttackClip;
    public AudioClip footstepClip;
    public AudioClip flammingLeap;
    public AudioClip axSpin;
    public AudioClip evasionClip;
    public GameObject hitbox;

    private BasicAttack basicAttack;

    // Start is called before the first frame update
    void Start()
    {
        basicAttack = GetComponentInParent<BasicAttack>();
    }

    public void EvasionWEvent()
    {
        if (evasionClip != null)
        {
            characterAudioSource.clip = evasionClip;
            characterAudioSource.Play();
        }
    }

    public void BasicAttackClipEvent()
    {
        if (basicAttackClip != null)
        {
            characterAudioSource.clip = basicAttackClip;
            characterAudioSource.Play();
        }
    }

    public void FlammingLeapEvent()
    {
        if (flammingLeap != null)
        {
            characterAudioSource.clip = flammingLeap;
            characterAudioSource.Play();
        }
    }

    public void AxSpinEvent()
    {
        if (axSpin != null)
        {
            characterAudioSource.clip = axSpin;
            characterAudioSource.Play();
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
    public void MeleeEvent()
    {
        // Take away player's stamina
        GetComponentInParent<Stamina>().DepleteStamina((int)basicAttack.AttackStaminaLoss);

        // DEBUG: Hitbox is visible
        hitbox.GetComponent<MeshRenderer>().enabled = true;

        // Freeze player movement
        GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        // Grab all colliders in the hitbox for the weapon
        Collider[] cols = Physics.OverlapBox(basicAttack.weaponHitbox.bounds.center, basicAttack.weaponHitbox.bounds.extents, basicAttack.weaponHitbox.transform.rotation, LayerMask.GetMask("Enemy"));

        // Cycle through each collider in the cols array and deal damage to each enemy inside
        foreach (Collider c in cols)
        {
            c.GetComponentInParent<Health>().Damage(1);
        }
    }

    public void ReleaseLockEvent()
    {

    }


    public void HitboxEvent()
    {
        // DEBUG: Hitbox is invisible
        hitbox.GetComponent<MeshRenderer>().enabled = false;

        GetComponent<Animator>().SetBool("performingAction", false);
    }
}