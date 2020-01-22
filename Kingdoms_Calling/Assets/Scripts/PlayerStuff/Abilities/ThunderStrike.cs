//  Name: ThunderStrike.cs
//  Author: Connor Larsen
//  Function: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThunderStrike : MonoBehaviour
{
    public Image abilityUI; // UI sprite for the ability in the HUD

    private bool isUsable;          // When ability is available for use, set this to true
    private float waitTime = 40;    // Time in seconds needed to wait for ability cooldown
    private float cooldownElapsed;  // When in cooldown, increments until waitTime is reached

    // Combo variables
    private ArcherAssassinCombo archerAssassinCombo;    // Used for calling the archer combo
    // private AssassinWarriorCombo assassinWarriorCombo;   // Used for calling the warrior combo
    // private AssassinPaladinCombo assassinPaladinCombo;   // Used for calling the paladin combo

    private Vector3 playerDestPos;  // The destination position for the player when ability is used

    // DEBUG
    private int assassinDmg = 10;   // Debug value for assassin damage to be used until stats are fully implemented

    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;        // Ability starts as usable
        cooldownElapsed = 0;    // Cooldown timer starts at 0
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Calling this function uses the ability
    public void UseAbility(GameObject target)
    {
        Debug.Log("Ability 1 Used (Assassin)");

        if (isUsable == true)
        {
            // Ability has been used, so it needs to cooldown
            isUsable = false;

            if (target != null)
            {
                // Start the cooldown timer

                // Update the UI with the time remaining

                // Play the ability animation

                // Find the targeted enemy
                playerDestPos = target.transform.position;

                // Teleport the player to the enemy targeted if enemy has been found
                if (playerDestPos != null)
                {
                    this.transform.position = playerDestPos;

                    // Do 3x damage to the enemy hit
                    int dmgDealt = assassinDmg * 3;

                    // Check enemy procs for combos
                    if (target.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.Wind)  // If enemy has a wind proc...
                    {
                        // Activate the Archer combo
                        archerAssassinCombo.ActivateCombo(target, dmgDealt, ElementManager.ClassElement.Lightning);
                    }
                    else if (target.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.Earth)    // If enemy has a lightning proc...
                    {
                        // Activate the Paladin combo
                    }
                    else if (target.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.Fire) // If enemy has a fire proc...
                    {
                        // Activate the Warrior combo
                    }
                    else
                    {
                        // Enemy has no proc and ability happens as normal
                        target.GetComponent<Health>().Damage(dmgDealt);

                        // Give enemies procs if appliciable
                        if (target.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.NONE)
                        {
                            target.GetComponent<ElementManager>().thisElement = ElementManager.ClassElement.Lightning;
                        }
                    }
                }
            }
        }
    }
}