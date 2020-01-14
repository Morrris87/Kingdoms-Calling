//  Name: Health.cs
//  Author: Connor Larsen
//  Function: Universal health script for both the players and enemies

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth;       // Sets the base starting health for the object the script is attached to
    public Image healthUI;      // UI element for this object's health bar

    public  int currentHealth;  // This int keeps track of what HP this object is currently at
    private bool isDead;        // If currentHealth reaches 0, this bool is set to true, otherwise is false

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;                 // Object starts off alive
        currentHealth = maxHealth;      // Transfers the value from startingHealth to currentHealth, keeping track of this object's max HP
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Damage(10); // DEBUG: When Q is pressed, 10 damage is done to player 1, changing the health bar
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Damage(-10);
        }
    }

    // Damage function is used to subtract health from currentHealth when damage is taken
    public void Damage(int damage)  // Pass in the amount to subtract from currentHealth
    {
        currentHealth -= damage;
        healthUI.fillAmount = CalculateHealthLeftPercent(currentHealth, maxHealth);    // Adjusts the fill amount of the health bar based on the % of health left

        if (currentHealth <= 0) // When currentHealth reaches 0...
        {
            isDead = true;      // Set the object as dead
        }
    }

    // Calculates the remaining health in a percentage out of 100
    public float CalculateHealthLeftPercent(int current, int starting)  // Pass in the currentHealth and the startingHealth
    {
        float damagePercent = (float)current / (float)starting;
        return damagePercent;
    }
}