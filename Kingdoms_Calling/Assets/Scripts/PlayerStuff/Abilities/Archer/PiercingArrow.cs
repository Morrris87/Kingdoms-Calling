//  Name: PiercingArrow.cs
//  Author: Connor Larsen
//  Date: 1/16/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiercingArrow : MonoBehaviour
{
    // Public Variables
    public GameObject abilityCooldownUI;    // UI element for the ability cooldown in the HUD
    public GameObject piercingArrowPrefab;  // Prefab for the piercing arrow shot
    [HideInInspector] public bool isUsable; // When ability is available for use, set this to true

    // Private Variables
    private float waitTime = 40f;   // Time in seconds needed to wait for ability cooldown
    private float cooldownTimer;    // When in cooldown, increments until waitTime is reached
    private float archerDmg;        // Damage the archer does

    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;            // Ability starts as usable
        cooldownTimer = waitTime;   // Cooldown timer starts at 0

        // Grab the value of the archers damage from BasicAttack.cs
        archerDmg = GetComponent<BasicAttack>().CharacterAttackValue(BasicAttack.CharacterClass.Archer);
    }

    // Update is called once per frame
    void Update()
    {
        // If ability has been used and hasn't refreshed...
        if (isUsable == false)
        {
            // If cooldownTimer hasn't completed...
            if (cooldownTimer >= 0f)
            {
                // Subtract cooldownTimer by deltaTime
                cooldownTimer -= Time.deltaTime;

                // Update the UI with the abount of time remaining
                abilityCooldownUI.GetComponentInChildren<Text>().text = "" + ((int)cooldownTimer + 1);
            }
            // Otherwise cooldownTimer has completed
            else
            {
                isUsable = true;                    // Make ability useable again
                //abilityCooldownUI.SetActive(false); // Hide the cooldown UI
                cooldownTimer = waitTime;           // Reset the cooldownTimer
            }
        }
    }

    // Calling this function uses the ability
    public void UseAbility()
    {
        abilityCooldownUI = GameObject.Find("ArcherSecondary_Cooldown");
        // If the ability is usable...
        if (isUsable == true)
        {
            // Ability has been used, so it needs to cooldown
            isUsable = false;

            // Enable the cooldown UI
            abilityCooldownUI.SetActive(true);

            // Play the ability animation
            GetComponentInChildren<Animator>().SetTrigger("PiercingArrowUsed");
        }
    }
}