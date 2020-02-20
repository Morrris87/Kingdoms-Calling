﻿/*
 * Paladin Earth Healing Spring Functionality
 * Resource: https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html
 * Created by: Bradley Williamson, Connor Larsen
 * On: 1/9/20
 */

using UnityEngine;

public class HealingSpringCollider : MonoBehaviour
{
    // Public Variables
    [Header("Healing Spring Specs")]
    public float totalLifeTime = 10;        // Time in seconds for the collider to last before being destroyed
    public float damageHealInterval = 2f;   // Interval value for how much time passes between healing the player and damaging the enemy
    public int healValue = 10;               // Variable for the ability's healing value
    public int damageValue = 1;             // Variable for the ability's damage value

    // Private Variables
    private float effectTimer, oldHealValue, oldDamageValue, abilityLifeTimer;
    private bool cooldownActive;    // Bool which determines if the cooldown is running

    // Combo variables
    private ArcherPaladinCombo archerPaladinCombo = new ArcherPaladinCombo();       // Used for calling the archer combo
    private AssassinPaladinCombo assassinPaladinCombo = new AssassinPaladinCombo(); // Used for calling the assassin combo
    private PaladinWarriorCombo paladinWarriorCombo = new PaladinWarriorCombo();    // Used for calling the warrior combo

    // Start is called before the first frame update
    void Start()
    {
        abilityLifeTimer = totalLifeTime;   // Sets the length of the cooldown to the amount stored in timerLength
        cooldownActive = true;              // Starts the cooldown timer
        effectTimer = 0f;                   // Set the heal/damage interval timer
    }

    // Update is called once per frame
    void Update()
    {
        // If the abilityLifeTimer has time left and cooldownActive is true...
        if (abilityLifeTimer > 0f && cooldownActive)
        {
            // Decrease timer by deltaTime every frame
            abilityLifeTimer -= Time.deltaTime;

            // If the heal/damage cooldown has completed...
            if (effectTimer <= 0f)
            {
                DamageEnemiesInCollider();          // Call the damage function
                HealPlayersInCollider();            // Call the heal function
                effectTimer = damageHealInterval;   // Reset the damage interval timer
            }
            else
            {
                //Increment the damage interval
                effectTimer -= Time.deltaTime;
            }
        }

        // If the cooldown has finished...
        if (abilityLifeTimer <= 0f)
        {
            // Stops the cooldown timer
            cooldownActive = false;

            // Destroys the collider for the ability
            Destroy(gameObject);
        }
    }

    // When called, damages all enemies found in the collider and applies earth proc if appliciable
    public void DamageEnemiesInCollider()
    {
        // Grab all colliders in the hitbox of the ability
        Collider[] cols = Physics.OverlapBox(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.extents, GetComponent<Collider>().transform.rotation, LayerMask.GetMask("Enemy"));

        // Cycle through each collider in the cols array
        foreach (Collider c in cols)
        {
            // Deal damage to the enemy
            c.GetComponent<Health>().Damage(damageValue);

            // If the enemy currently has no element assigned in it's Element Manager...
            if (c.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.NONE)
            {
                // Set the elemental proc to Earth
                c.GetComponent<ElementManager>().thisElement = ElementManager.ClassElement.Earth;
            }
            else
            {
                // If the enemy currently has a Wind proc...
                if (c.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.Wind)
                {
                    // Activate the Archer & Paladin combo
                    archerPaladinCombo.ActivateCombo(c.gameObject);
                }
                // If the enemy currently has a Fire proc...
                else if (c.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.Fire)
                {
                    // Activate the Paladin & Warrior combo
                    paladinWarriorCombo.ActivateCombo(c.gameObject);
                }
                // If the enemy currently has a Lightning proc...
                else if (c.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.Lightning)
                {
                    // Activate the Assassin & Paladin combo
                    //assassinPaladinCombo.ActivateCombo();
                }
            }
        }
    }

    // When called, heals all players found in the collider
    public void HealPlayersInCollider()
    {
        // Grab all colliders in the hitbox of the ability
        Collider[] cols = Physics.OverlapBox(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.extents, GetComponent<Collider>().transform.rotation, LayerMask.GetMask("Player"));

        // Cycle through each collider in the cols array
        foreach (Collider c in cols)
        {
            // Heal the player
            c.GetComponent<Health>().Heal(healValue);
        }
    }

    public void TotemBoost(float rangeMultiplier, int damageHealMultiplier)
    {
        //update the specs of the totem and reset its life boosted     
        damageValue *= damageHealMultiplier;
        healValue *= damageHealMultiplier;
        abilityLifeTimer = totalLifeTime;
    }
}
