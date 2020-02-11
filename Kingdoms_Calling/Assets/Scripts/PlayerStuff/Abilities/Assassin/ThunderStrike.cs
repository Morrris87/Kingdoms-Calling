//  Name: ThunderStrike.cs
//  Author: Connor Larsen
//  Function: Teleports to a nearby enemy, dealing damage based on enemy health remaining

using UnityEngine;
using UnityEngine.UI;

public class ThunderStrike : MonoBehaviour
{
    // Public Variables
    public GameObject abilityCooldownUI;            // UI element for the ability cooldown in the HUD
    public float waitTime = 20;                     // Time in seconds needed to wait for ability cooldown
    [HideInInspector] public bool isUsable;         // When ability is available for use, set this to true

    // Private Variables
    private float cooldownTimer;    // When in cooldown, increments until waitTime is reached
    private float assassinDmg;      // Variable for the assassin's attack damage
    private Vector3 playerDestPos;  // The destination position for the player when ability is used


    // Combo variables
    private ArcherAssassinCombo archerAssassinCombo;    // Used for calling the archer combo
    private AssassinWarriorCombo assassinWarriorCombo;  // Used for calling the warrior combo
    private AssassinPaladinCombo assassinPaladinCombo;  // Used for calling the paladin combo

    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;            // Ability starts as usable
        cooldownTimer = waitTime;   // Cooldown timer starts at the value of waitTime
    }

    // Update is called once per frame
    void Update()
    {
        // If ability has been used and hasn't refreshed...
        if (isUsable == false)
        {
            // If cooldownTimer hasn't completed...
            if (cooldownTimer >= 0)
            {
                // Subtract cooldownTimer by deltaTime
                cooldownTimer -= Time.deltaTime;

                // Update the UI with the amount of time remaining
                abilityCooldownUI.GetComponentInChildren<Text>().text = "" + ((int)cooldownTimer + 1);
            }
            // Otherwise cooldownTimer has completed
            else
            {
                isUsable = true;                    // Make ability useable again
                abilityCooldownUI.SetActive(false); // Hide the cooldown UI
                cooldownTimer = waitTime;           // Reset the cooldownTimer
            }
        }
    }

    // Calling this function uses the ability
    public void UseAbility()
    {
        // If the ability is usable...
        if (isUsable == true)
        {
            // Ability has been used, so set ability as unusable
            isUsable = false;

            // Enable the cooldown UI
            abilityCooldownUI.SetActive(true);

            // Play the ability animation

            // OLD CODE
            //// Find the targeted enemy
            //playerDestPos = target.transform.position;

            //this.transform.position = playerDestPos;

            //// Do 3x damage to the enemy hit
            //int dmgDealt = assassinDmg * 3;

            //// Check enemy procs for combos
            //if (target.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.Wind)  // If enemy has a wind proc...
            //{
            //    // Activate the Archer combo
            //    archerAssassinCombo.ActivateCombo(target, dmgDealt);
            //}
            //else if (target.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.Earth)    // If enemy has a lightning proc...
            //{
            //    // Activate the Paladin combo
            //}
            //else if (target.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.Fire) // If enemy has a fire proc...
            //{
            //    // Activate the Warrior combo
            //}
            //else
            //{
            //    // Enemy has no proc and ability happens as normal
            //    target.GetComponent<Health>().Damage(dmgDealt);

            //    // Give enemies procs if appliciable
            //    if (target.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.NONE)
            //    {
            //        target.GetComponent<ElementManager>().thisElement = ElementManager.ClassElement.Lightning;
            //    }
            //}
        }
    }
}