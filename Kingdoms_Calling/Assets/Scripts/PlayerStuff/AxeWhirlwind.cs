using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AxeWhirlwind : MonoBehaviour
{
    [Header("UI Element")]
    public Image abilityUI; // The UI Images for the abilities

    [Header("Ability Specs")]
    public float whirlwindCooldown;     //Cooldown time for the ability itself
    public float whirlwindRadius;       //How big the AOE is
    public float whirlwindDuration;     //How long the skill lasts
    public int whirlwindDamage;       //How much damage the skill does at each tick
    public float whirlwindAttackSpeed;  //How often damage ticks on enemies in the area

    private bool isUsable;          // When ability is available for use, set this to true
    private bool inUse;          // When ability is in use, set this to false
    private float cooldownElapsed;  // When in cooldown, increments until waitTime is reached
    private SphereCollider whirlwindCollider;   // Collider to handle enemies in the AOE  
    private int playerLayerIndex, enemyLayerIndex;      //Player and enemy layer index
    private float attackDuration, attackInterval;


    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;        // Ability starts as usable
        inUse = true;        // Ability starts as not in use
        cooldownElapsed = 0;    // Cooldown timer starts at 0 (Not on cooldown)
        attackDuration = whirlwindDuration; // Set the duration to the inpsector value
        attackInterval = whirlwindAttackSpeed; // Set the interval for attacking

        //Grab and setup the sphere collider responsible for the ability
        whirlwindCollider = this.gameObject.GetComponent<SphereCollider>();
        whirlwindCollider.enabled = false;
        whirlwindCollider.radius = whirlwindRadius;

        //Get the player and enemy layermask id's
        playerLayerIndex = LayerMask.NameToLayer("Player");
        enemyLayerIndex = LayerMask.NameToLayer("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        //if unusable decrease the timer
        if(isUsable == false)
        {
            cooldownElapsed -= Time.deltaTime;                  // Count the cooldown down
            if(cooldownElapsed <= 0)
            {
                isUsable = true;                                // Reset the isUsable field
            }
            if(inUse)                                           // If we are currently using the ability
            {
                attackDuration -= Time.deltaTime;               // Count the duration left down
                attackInterval -= Time.deltaTime;               // Count down attack interval
            }            
        }
    }

    // Calling this function uses the ability
    public void UseAbility()
    {
        Debug.Log("Whirlwind Used (Warroir)");

        if (isUsable == true)
        {
            // Ability has been used, so it needs to cooldown
            //isUsable = false;

            // Start the cooldown timer
            cooldownElapsed = whirlwindCooldown;
            // Update the UI with the time remaining

            // Play the ability animation            

            // Enable sphere collider on player which handles everything and change the inUse bool
            whirlwindCollider.enabled = true;
            inUse = true;   

            // Give enemies procs if appliciable
            //if (target.GetComponent<SkeletonStats>().proc == SkeletonStats.Proc.None)
            //{
            //    target.GetComponent<SkeletonStats>().proc = SkeletonStats.Proc.Lightning;
            //}

        }
    }

    void OnCollisionStay(Collision collision)
    {
        //Check if we are dealing with a enemy
        if(collision.gameObject.layer == enemyLayerIndex)
        {
            if(attackInterval <= 0) // Check if we are ready to attack again
            {
                Debug.Log("Whirlwind attempting to attack: " + collision.gameObject.name);
                collision.gameObject.GetComponent<Health>().Damage(-whirlwindDamage); // Deal damage to the enemy
                attackInterval = whirlwindAttackSpeed;                                // Reset our attack interval timer
            }
        }
    }
}
