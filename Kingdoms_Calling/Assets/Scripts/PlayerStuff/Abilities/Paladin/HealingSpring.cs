﻿//  Name: HealingSpring.cs
//  Author: Bradley Williamson, Connor Larsen
//  Function: Spawns a healing spring when the Paladin's Ability 1 is used

using UnityEngine;
using UnityEngine.UI;

public class HealingSpring : MonoBehaviour
{
    // Public Variables
    public GameObject abilityCooldownUI;    // UI element for the ability cooldown in the HUD
    public GameObject areaOfEffect;         // The collider for the HealingSpring ability
    public float waitTime = 20f;            // Time in seconds needed to wait for ability cooldown

    // Private Variables
    private bool isUsable;          // When ability is available for use, set this to true
    private float cooldownTimer;    // When in cooldown, increments until waitTime is reached

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
            if (cooldownTimer >= 0f)
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

            // Instantiate the ability collider prefab on character location
            Instantiate(areaOfEffect, transform.position, Quaternion.identity);
        }
    }
}
