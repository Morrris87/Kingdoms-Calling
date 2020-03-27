using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShootHandler : MonoBehaviour
{
    public AudioSource characterSource;
    public AudioSource arrowSource;

    public AudioClip windupClip;
    public AudioClip attackClip;

    private BasicAttack basicAttack;

    // Start is called before the first frame update
    void Start()
    {
        basicAttack = GetComponentInParent<BasicAttack>();
    }

    public void WindupEvent()
    {
        if (windupClip != null)
        {
            characterSource.clip = windupClip;
            characterSource.Play();
        }
    }

    public void ShootEvent()
    {
        // Take away player's stamina
        GetComponentInParent<Stamina>().DepleteStamina((int)basicAttack.AttackStaminaLoss);

        // Create the arrow prefab
        basicAttack.arrowPrefab.transform.rotation = transform.rotation;                                                // Set the arrow's rotation to that of the player
        basicAttack.arrowPrefab.GetComponent<ProjectileDamage>().attacker = ProjectileDamage.Attacker.PLAYER;           // Set the attacker to the player

        // Play Sound
        if (attackClip != null)
        {
            arrowSource.clip = attackClip;
            arrowSource.Play();
        }


        Instantiate(basicAttack.arrowPrefab, basicAttack.spawner.position, Quaternion.LookRotation(transform.forward, Vector3.up)); // Fire the arrow
        MovingEvent();
    }


    public void ReleaseLockEvent()
    {

    }

    public void MovingEvent()
    {
        GetComponent<Animator>().SetBool("performingAction", false);
    }
}