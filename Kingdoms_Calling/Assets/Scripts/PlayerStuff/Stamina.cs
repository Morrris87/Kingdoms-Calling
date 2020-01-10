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
    public int maxStamina;      // Sets the max value the stamina can go to for the object this script is attached to
    public Image staminaUI;     // UI element for this object's stamina bar

    private int currentStamina; // This int keeps track of the current stamina the object currently has
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
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Deplete(10);
        }
    }

    private void Deplete(int energy)
    {
        currentStamina -= energy;   // Subtracts energy spent from currentStamina
        staminaUI.fillAmount = CalculateStaminaLeftPercent(currentStamina, maxStamina);

        if (currentStamina <= 0)
        {

        }
    }

    private float CalculateStaminaLeftPercent(int current, int max)
    {
        float energyPercent = (float)current / (float)max;
        return energyPercent;
    }
}