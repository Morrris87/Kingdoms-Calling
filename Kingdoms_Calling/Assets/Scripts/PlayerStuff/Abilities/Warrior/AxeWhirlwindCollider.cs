//  Name: AxeWhirlwindCollider.cs
//  Author: Connor Larsen
//  Function: Places a collider at the point the player is standing which damages enemies inside over time

using UnityEngine;

public class AxeWhirlwindCollider : MonoBehaviour
{
    // Public Variables
    public float timerLength = 5f;  // Time in seconds for the collider to last before being destroyed
    public GameObject playerPos;    // Keeps track of the player's position

    // Private Variables
    private float damageInterval = 0.5f;    // Interval value for how much time passes between damage being dealt
    private float damageTimer;              // Timer for timing damage intervals
    private float abilityLifeTimer;         // The ability timer
    private float warriorDmg;               // Variable for the archer's attack damage
    private bool cooldownActive;            // Bool which determines if the cooldown is running

    // Combo Variables
    private ArcherWarriorCombo archerWarriorCombo;      // Used for calling the archer combo
    private AssassinWarriorCombo assassinWarriorCombo;  // Used for calling the assassin combo
    private PaladinWarriorCombo paladinWarriorCombo;    // Used for calling the paladin combo

    // Start is called before the first frame update
    void Start()
    {
        abilityLifeTimer = timerLength; // Sets the length of the cooldown to the amount stored in timerLength
        cooldownActive = true;          // Starts the cooldown timer
        damageTimer = 0f;               // Set the damage interval timer

        // Grab the player
        playerPos = GameObject.FindGameObjectWithTag("AW_Tracker");

        // Sets archerDmg to the stored value in BasicAttack and halves it
        warriorDmg = (FindObjectOfType<BasicAttack>().CharacterAttackValue(BasicAttack.CharacterClass.Warrior));
    }

    // Update is called once per frame
    void Update()
    {
        // If the abilityLifeTimer has time left and cooldownActive is true...
        if (abilityLifeTimer > 0f && cooldownActive)
        {
            // Decrease timer by deltaTime every frame
            abilityLifeTimer -= Time.deltaTime;

            // Move collider to the player
            transform.position = playerPos.transform.position;

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

    public void DamageEnemiesInCollider()
    {
        // Grab all colliders in the hitbox of the ability
        Collider[] cols = Physics.OverlapBox(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.extents, GetComponent<Collider>().transform.rotation, LayerMask.GetMask("Enemy"));

        // Cycle through each collider in the cols array
        foreach (Collider c in cols)
        {
            // Deal damage to the enemy
            c.GetComponent<Health>().Damage((int)warriorDmg);
        }
    }
}