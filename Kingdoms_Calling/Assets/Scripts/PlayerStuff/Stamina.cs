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
    public float maxStamina = 100f;  // Sets the max value the stamina can go to for the object this script is attached to
    public Image staminaUI;         // UI element for this object's stamina bar

    private float currentStamina;   // This int keeps track of the current stamina the object currently has
    private float cooldown;          // Value for the stamina cooldown
    private bool isDepleted;        // If currentStamina reaches 0, this bool is set to true, otherwise false

    // Start is called before the first frame update
    void Start()
    {
        isDepleted = false;             // Object starts with all stamina
        currentStamina = maxStamina;    // Transfers the value from maxStamina to currentStamina
    }

    // Update is called once per frame
    void Update()
    {
        if (currentStamina < maxStamina && cooldown <= 0f)
        {
            currentStamina += 1;
            staminaUI.fillAmount = CalculateStaminaLeftPercent(currentStamina, maxStamina); // Updates the UI
        }

        if (cooldown > 0f)
        {
            cooldown -= Time.deltaTime;
        }
    }

    public float GetStamina()
    {
        return currentStamina;
    }

    public void DepleteStamina(int value)
    {
        currentStamina -= value;
        staminaUI.fillAmount = CalculateStaminaLeftPercent(currentStamina, maxStamina); // Updates the UI
        cooldown = 2f;
    }

    private float CalculateStaminaLeftPercent(float current, float max)
    {
        float energyPercent = current / max;
        return energyPercent;
    }
}