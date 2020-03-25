//  Name: Health.cs
//  Author: Connor Larsen
//  Date: 1/7/2020
//  Function: Universal health script for both the players and enemies

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth;       // Sets the base starting health for the object the script is attached to
    public Image healthUI;      // UI element for this object's health bar
    public bool takeDamage;
    public bool ragingResponse;

    //[HideInInspector]
    public  int currentHealth;  // This int keeps track of what HP this object is currently at
    private bool isDead;        // If currentHealth reaches 0, this bool is set to true, otherwise is false
    private bool trueDamage;    // If true, player/enemy recieves true damage, which ignores armor value
    private RagingResponse ragingResponseScript;
    [HideInInspector]
    public CharacterManager.CharacterClass characterClass;
    
    float timerForFlash = 0;
    bool colorBool = false;
    Color tempColor;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;                 // Object starts off alive
        takeDamage = true;
        ragingResponse = false;
        currentHealth = maxHealth;      // Transfers the value from startingHealth to currentHealth, keeping track of this object's max HP
        if (this.tag == "White" || this.tag == "Grey" || this.tag == "Purple")
        {
            //tempColor = this.gameObject.GetComponent<Renderer>().material.color;
        }
    }

    private void Awake()
    {
        //if we are the warrior get the raging response script
        if (characterClass == CharacterManager.CharacterClass.Warrior)
        {
            ragingResponseScript = gameObject.GetComponent<RagingResponse>();
        }
    }

    void Update()
    {
        // If the player, update their healthbar
        if (this.tag == "Player" || tag == "Boss")
        {
            healthUI.fillAmount = CalculateHealthLeftPercent(currentHealth, maxHealth);    // Adjusts the fill amount of the health bar based on the % of health left
            if (currentHealth <= 0) // When currentHealth reaches 0...
            {
                GetComponentInChildren<Animator>().SetTrigger("Dead");
            }
        }

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

        //Check take damage bool if true we take damage (Paladin Evasion)
        if(takeDamage)
        {            
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

            if (ragingResponse)
            {
                ragingResponseScript.SpawnRagingResponse();
            }
        }        

        if (currentHealth <= 0) // When currentHealth reaches 0...
        {
            GetComponentInChildren<Animator>().SetTrigger("Dead");
        }
    }

    // Heal function is used to add health to currentHealth when damage is taken
    public void Heal(int health)
    {
        // If currentHealth is less than maxHealth...
        if (currentHealth < maxHealth)
        {
            currentHealth += health;    // Give health to the object

            // If currentHealth is above maxHealth after being healed...
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;  // Set currentHealth to maxHealth
            }
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
}