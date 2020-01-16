/*
 * Warrior axe whirlwind ability
 * Resource: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Installation.html
 * Created by: Bradley Williamson
 * On: 1/15/20
 */
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
    private int playerLayerIndex, enemyLayerIndex;      //Player and enemy layer index
    public float attackDuration, attackInterval;


    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;        // Ability starts as usable
        inUse = true;        // Ability starts as not in use
        cooldownElapsed = 0;    // Cooldown timer starts at 0 (Not on cooldown)
        attackDuration = whirlwindDuration; // Set the duration to the inpsector value
        attackInterval = whirlwindAttackSpeed; // Set the interval for attacking

        //Get the player and enemy layermask id's
        playerLayerIndex = LayerMask.NameToLayer("Player");
        enemyLayerIndex = LayerMask.NameToLayer("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        //if unusable decrease the timer
        if (isUsable == false)
        {
            cooldownElapsed -= Time.deltaTime;                  // Count the cooldown down
            if (cooldownElapsed <= 0)
            {
                isUsable = true;                                // Reset the isUsable field
            }
            if (inUse)                                           // If we are currently using the ability
            {
                if (attackDuration >= 0)
                    attackDuration -= Time.deltaTime;               // Count the duration left down
                else
                {
                    inUse = false;                                  // Disable the ability
                }
                if (attackInterval >= 0)
                    attackInterval -= Time.deltaTime;               // Count down attack interval
                else if (attackInterval <= 0)
                {
                    AttackAroundCharacter();                        //If timer is less than or 0 we attack around the character
                }
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
            isUsable = false;

            // Start the cooldown timer
            cooldownElapsed = whirlwindCooldown;
            // Update the UI with the time remaining

            // Play the ability animation            

            // Change the inUse bool
            inUse = true;
        }
    }

    void AttackAroundCharacter()
    {
        //Debug.Log("Attacking Started");
        //Grab all colliders inside of the sphere which in our case acts as a circle with enemy layer mask 
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, whirlwindRadius, 1 << enemyLayerIndex);

        //Loop through the colliders checking if its either a player or a enemy
        int i = 0;
        while (i < hitColliders.Length)
        {
            //Debug.Log("Whirlwind Damage " + hitColliders[i].name);
            hitColliders[i].gameObject.GetComponentInChildren<Health>().Damage(whirlwindDamage); //Damage the current colliders health by the current damageValue

            i++;
        }
        //Reset the tick timer 
        attackInterval = whirlwindAttackSpeed;
        //Debug.Log("Attacking Ended");
    }
}
