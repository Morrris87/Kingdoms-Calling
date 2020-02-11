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
    public float comboCooldown;

    [Header("ComboAbility Prefabs")]
    public GameObject ArcherWarriorComboPrefab;

    [HideInInspector]
    public bool isUsable;          // When ability is available for use, set this to true
    private bool inUse;          // When ability is in use, set this to false
    private float cooldownElapsed;  // When in cooldown, increments until waitTime is reached
    private int playerLayerIndex, enemyLayerIndex;      //Player and enemy layer index
    private float attackDuration, attackInterval;
    private ElementManager eManager;
    private AssassinWarriorCombo assassinWarriorCombo;


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
        //if our combo is on cooldown decrease the timer
        if(comboCooldown >=0)
        {
            comboCooldown -= Time.deltaTime;
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
            eManager = hitColliders[i].GetComponent<ElementManager>();
            //if our combo is off cooldown then check elements
            if (comboCooldown <= 0)
            {
                // Check enemy procs for combos
                if (eManager.thisElement == ElementManager.ClassElement.Wind)  // If enemy has a wind proc...
                {
                    // Activate the Archer combo
                    //instantiate the archer warrior combo prefab on target location
                    Instantiate(ArcherWarriorComboPrefab, hitColliders[i].transform.position, Quaternion.identity);
                }
                else if (eManager.thisElement == ElementManager.ClassElement.Earth)    // If enemy has a earth proc...
                {
                    // Activate the Paladin combo
                }
                else if (eManager.thisElement == ElementManager.ClassElement.Lightning) // If enemy has a lightning proc...
                {
                    // Activate the Assassin combo
                    assassinWarriorCombo.UseAbility();
                }
            }
            //else our combo is on cooldown just do the normal damage
            else
            {
                // Enemy has no proc and ability happens as normal
                //Debug.Log("Whirlwind Damage " + hitColliders[i].name);
                hitColliders[i].gameObject.GetComponentInChildren<Health>().Damage(whirlwindDamage); //Damage the current colliders health by the current damageValue

                // Give enemies procs if appliciable
                if (eManager.thisElement == ElementManager.ClassElement.NONE)
                {
                    eManager.thisElement = ElementManager.ClassElement.Fire;
                }
            }

            i++;
        }
        //Reset the tick timer 
        attackInterval = whirlwindAttackSpeed;
        //Debug.Log("Attacking Ended");
    }
}
