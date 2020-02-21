﻿//  Name: ArrowVolleyCollider.cs
//  Author: Connor Larsen
//  Function: 

using UnityEngine;
using UnityEngine.UI;

public class FlamingLeapCollider : MonoBehaviour
{
    // Public Variables
    public float timerLength = 1f;  // Time in seconds for the collider to last before being destroyed

    // Private Variables
    private float abilityLifeTimer;     // The ability timer
    private float warriorDmg;           // Variable for the warrior's attack damage
    private bool cooldownActive;        // Bool which determines if the cooldown is running
    public GameObject ChainLightningPrefab;

    // Combo variables
    private ArcherWarriorCombo archerWarriorCombo = new ArcherWarriorCombo();       // Used for calling the archer combo
    private AssassinWarriorCombo assassinWarriorCombo = new AssassinWarriorCombo(); // Used for calling the assassin combo
    //private AssassinWarriorCombo assassinWarriorCombo;
    private PaladinWarriorCombo paladinWarriorCombo = new PaladinWarriorCombo();    // Used for calling the paladin combo
    private Text comboText;


    // Start is called before the first frame update
    void Start()
    {
        //assassinWarriorCombo = new AssassinWarriorCombo();

        abilityLifeTimer = timerLength; // Sets the length of the cooldown to the amount stored in timerLength
        cooldownActive = true;          // Starts the cooldown timer
        comboText = GameObject.FindGameObjectWithTag("ComboText").GetComponent<Text>();

        // Sets warriorDmg to the stored value in BasicAttack
        warriorDmg = FindObjectOfType<BasicAttack>().CharacterAttackValue(BasicAttack.CharacterClass.Warrior);

        // Damage all enemies in the collider when placed
        DamageEnemiesInCollider();
    }

    // Update is called once per frame
    void Update()
    {
        // If the abilityLifeTimer has time left and cooldownActive is true...
        if (abilityLifeTimer > 0f && cooldownActive)
        {
            // Decrease timer by deltaTime every frame
            abilityLifeTimer -= Time.deltaTime;
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

            // If the enemy currently has no element assigned in it's Element Manager...
            if (c.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.NONE)
            {
                // Set the elemental proc to Fire
                c.GetComponent<ElementManager>().thisElement = ElementManager.ClassElement.Fire;
            }
            else
            {
                // If the enemy currently has an Earth proc...
                if (c.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.Earth)
                {
                    // Activate the Paladin & Warrior combo
                    paladinWarriorCombo.ActivateCombo(c.gameObject);
                    comboText.text = "Paladin & Warrior Combo Performed";
                }
                // If the enemy currently has a Wind proc...
                else if (c.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.Wind)
                {
                    // Activate the Archer & Warrior combo
                    archerWarriorCombo.ActivateCombo();
                    comboText.text = "Archer & Warrior Combo Performed";
                }
                // If the enemy currently has a Lightning proc...
                else if (c.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.Lightning)
                {
                    // Activate the Archer & Assassin combo
                    assassinWarriorCombo.ActivateCombo(ChainLightningPrefab);
                    comboText.text = "Assassin & Warrior Combo Performed";
                }
            }
        }
    }
}