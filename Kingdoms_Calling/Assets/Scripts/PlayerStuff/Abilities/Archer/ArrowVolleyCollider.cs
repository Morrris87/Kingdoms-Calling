//  Name: ArrowVolleyCollider.cs
//  Author: Connor Larsen
//  Date: 2/8/2020

using UnityEngine;

public class ArrowVolleyCollider : MonoBehaviour
{
    // Public Variables
    public float timerLength = 5f;  // Time in seconds for the collider to last before being destroyed

    // Private Variables
    private float damageInterval = 1f;  // Interval value for how much time passes between damage being dealt
    private float damageTimer;          // Timer for timing damage intervals
    private float abilityLifeTimer;     // The ability timer
    private float archerDmg;              // Variable for the archer's attack damage
    private bool cooldownActive;        // Bool which determines if the cooldown is running

    // Combo variables
    private ArcherAssassinCombo archerAssassinCombo;    // Used for calling the assassin combo
    private ArcherWarriorCombo archerWarriorCombo;      // Used for calling the warrior combo
    private ArcherPaladinCombo archerPaladinCombo;      // Used for calling the paladin combo

    // Start is called before the first frame update
    void Start()
    {
        abilityLifeTimer = timerLength; // Sets the length of the cooldown to the amount stored in timerLength
        cooldownActive = true;          // Starts the cooldown timer

        // Sets archerDmg to the stored value in BasicAttack
        archerDmg = FindObjectOfType<BasicAttack>().CharacterAttackValue(BasicAttack.CharacterClass.Archer);
    }

    // Update is called once per frame
    void Update()
    {
        // If the abilityLifeTimer has time left and cooldownActive is true...
        if (abilityLifeTimer > 0f && cooldownActive)
        {
            // Decrease timer by deltaTime every frame
            abilityLifeTimer -= Time.deltaTime;

            // If the damage cooldown has completed...
            if (damageTimer <= 0f)
            {
                DamageEnemiesInCollider();      // Call the damage function
                damageTimer = damageInterval;   // Reset the damage interval timer
            }
            else
            {
                damageTimer -= Time.deltaTime;  // Decrease timer by deltaTime every frame
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

    // When called, damages all enemies found in the collider and applies wind proc if appliciable
    public void DamageEnemiesInCollider()
    {
        // Grab all colliders in the hitbox of the ability
        Collider[] cols = Physics.OverlapBox(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.extents, GetComponent<Collider>().transform.rotation, LayerMask.GetMask("Enemy"));

        // Cycle through each collider in the cols array
        foreach (Collider c in cols)
        {
            // Deal damage to the enemy
            c.GetComponent<Health>().Damage((int)archerDmg);

            // If the enemy currently has no element assigned in it's Element Manager
            if (c.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.NONE)
            {
                // Set the elemental proc to Wind
                c.GetComponent<ElementManager>().thisElement = ElementManager.ClassElement.Wind;
            }
            else
            {
                // If the enemy currently has an Earth proc...
                if (c.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.Earth)
                {
                    // Activate the Archer & Paladin combo
                    archerPaladinCombo.ActivateCombo(c.gameObject);
                }
                // If the enemy currently has a Fire proc...
                else if (c.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.Fire)
                {
                    // Activate the Archer & Warrior combo
                    archerWarriorCombo.ActivateCombo();
                }
                // If the enemy currently has a Lightning proc...
                else if (c.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.Lightning)
                {
                    // Activate the Archer & Assassin combo
                    archerAssassinCombo.ActivateCombo(c.gameObject, (int)archerDmg);
                }
            }
        }
    }
}