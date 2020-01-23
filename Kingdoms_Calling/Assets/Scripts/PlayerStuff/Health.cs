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

    //[HideInInspector]
    public  int currentHealth;  // This int keeps track of what HP this object is currently at
    private bool isDead;        // If currentHealth reaches 0, this bool is set to true, otherwise is false
    private bool trueDamage;    // If true, player/enemy recieves true damage, which ignores armor value
    float timerForFlash = 0;
    bool colorBool = false;
    Color tempColor;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;                 // Object starts off alive
        currentHealth = maxHealth;      // Transfers the value from startingHealth to currentHealth, keeping track of this object's max HP
        if (this.tag == "White" || this.tag == "Grey" || this.tag == "Purple")
        {
            tempColor = this.gameObject.GetComponent<Renderer>().material.color;
        }
    }

    void Update()
    {
        timerForFlash += Time.deltaTime; 
        if (this.tag == "White" || this.tag == "Grey" || this.tag == "Purple")
        {
            
            if (colorBool == true && timerForFlash >= 2)
            {
            this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", tempColor);
            timerForFlash = 0;
                colorBool = false;
            }
        }
    }

    // Damage function is used to subtract health from currentHealth when damage is taken
    public void Damage(int damage)  // Pass in the amount to subtract from currentHealth
    {
        if(this.tag == "White" || this.tag == "Grey" || this.tag == "Purple")
        {
            
            if (timerForFlash <= 2)// 2 seconds
            {
                colorBool = true;
                //flash red
                this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }         
        }

        if(this.tag == "Player")
        {
            healthUI.fillAmount = CalculateHealthLeftPercent(currentHealth, maxHealth);    // Adjusts the fill amount of the health bar based on the % of health left
        }

        if (trueDamage)
        {
            // Deal true damage to the player/enemy
            currentHealth -= damage;
        }
        else
        {
            // Deal normal damage to the player/enemy
            currentHealth -= damage;    // subtract armor from damage in this calc when implemented
        }

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

    public void ActivateTrueDamage(bool value)
    {
        trueDamage = value;
    }

    public void DebugHealthLoss()
    {
        Damage(10);
    }

    public void DebugHealthGain()
    {
        Damage(-10);
    }
}