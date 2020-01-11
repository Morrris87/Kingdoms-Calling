//  Name: Stamina.cs
//  Author: Connor Larsen
//  Function: Stamina script for the player characters

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public float maxStamina = 20f;    // Sets the max value the stamina can go to for the object this script is attached to
    public Image staminaUI;         // UI element for this object's stamina bar

    private float currentStamina; // This int keeps track of the current stamina the object currently has
    private bool isSprinting;   // True when using stamina
    private bool isDepleted;    // If currentStamina reaches 0, this bool is set to true, otherwise false

    // Start is called before the first frame update
    void Start()
    {
        isDepleted = false;             // Object starts with all stamina
        currentStamina = maxStamina;    // Transfers the value from maxStamina to currentStamina
    }

    // Update is called once per frame
    void Update()
    {
        // If holding down shift...
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Sprinting(true);    // Start sprinting
        }
        // If releasing shift...
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Sprinting(false);   // Stop sprinting
        }

        if (isSprinting)
        {
            currentStamina -= 2;   // Decrease stamina over time
            staminaUI.fillAmount = CalculateStaminaLeftPercent(currentStamina, maxStamina); // Updates the UI

            // If stamina hits 0...
            if (currentStamina <= 0)
            {
                Debug.Log("Stamina Depleted");
                currentStamina = 0;     // Reset currentStamina to 0
                isSprinting = false;    // Stop sprinting
            }
        }
        else if (!isSprinting && currentStamina < maxStamina)
        {
            currentStamina += 1;
            staminaUI.fillAmount = CalculateStaminaLeftPercent(currentStamina, maxStamina); // Updates the UI
        }
    }

    private void Sprinting(bool sprint)
    {
        isSprinting = sprint;
    }

    private float CalculateStaminaLeftPercent(float current, float max)
    {
        float energyPercent = current / max;
        return energyPercent;
    }
}