/*
 * Warrior flaming leap ability
 * Resource: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Installation.html
 * Created by: Bradley Williamson
 * On: 1/16/20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlamingLeap : MonoBehaviour
{
    [Header("UI Element")]
    public Image abilityUI; // The UI Images for the abilities

    [Header("Ability Specs")]
    public float leapCooldown;     //Cooldown time for the ability itself
    public float leapRadius;       //How big the AOE is
    public float leapDuration;     //How long the skill lasts
    public int leapDamage;       //How much damage the skill does at each tick
    public float animationLength;
    public float leapDistance;
    public GameObject destinationMarker;
    Vector2 leapLocation;   

    private bool isUsable;          // When ability is available for use, set this to true
    private float cooldownElapsed;  // When in cooldown, increments until waitTime is reached
    private int playerLayerIndex, enemyLayerIndex;      //Player and enemy layer index
    float attackDuration, attackInterval;

    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;        // Ability starts as usable
        cooldownElapsed = 0;    // Cooldown timer starts at 0 (Not on cooldown)

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
        }
    }

    // Calling this function uses the ability
    public void UseAbility(Vector2 input)
    {
        //Debug.Log("flame leap Used (Warroir)");

        if (isUsable == true)
        {
            // Ability has been used, so it needs to cooldown
            isUsable = false;

            // Start the cooldown timer
            cooldownElapsed = leapCooldown;

            // Update the UI with the time remaining

            // Play the ability animation (handle player location)

            // get the input 
            leapLocation = input;
            // Debug.Log(input);

            // preform the ability
            leapCharacter(input);
            AttackAroundCharacter();
        }
    }

    void leapCharacter(Vector2 inp)
    {
        transform.position = (new Vector3(transform.position.x, 0, transform.position.z) + new Vector3(inp.x * leapDistance, transform.position.y, inp.y * leapDistance));
    }

    void AttackAroundCharacter()
    {
        //Debug.Log("Attacking Started");
        //Grab all colliders inside of the sphere which in our case acts as a circle with enemy layer mask 
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, leapRadius, 1 << enemyLayerIndex);

        int i = 0;
        while (i < hitColliders.Length)
        {
            //Debug.Log("Warroir Leap Damage " + hitColliders[i].name);
            hitColliders[i].gameObject.GetComponentInChildren<Health>().Damage(leapDamage); //Damage the current colliders health by the current damageValue

            i++;
        }
    }
}
