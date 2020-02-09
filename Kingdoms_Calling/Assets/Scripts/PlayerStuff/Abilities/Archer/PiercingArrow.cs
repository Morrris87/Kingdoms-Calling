//  Name: PiercingArrow.cs
//  Author: Connor Larsen
//  Date: 1/16/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PiercingArrow : MonoBehaviour
{
    public Image abilityUI; // The UI Images for the abilities

    private bool isUsable;          // When ability is available for use, set this to true
    private float waitTime = 40;    // Time in seconds needed to wait for ability cooldown
    private float cooldownElapsed;  // When in cooldown, increments until waitTime is reached
    private RaycastHit[] hits;      // Array to store all enemies hit by the ability
    private int enemyLayerIndex;    // Index for the enemy layer
    private int damage;             // Damage the ability does

    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;                                    // Ability starts as usable
        cooldownElapsed = 0;                                // Cooldown timer starts at 0
        enemyLayerIndex = LayerMask.NameToLayer("Enemy");   // Set the index to the value of the enemy layer
        damage = 10;                                        // Set the damage of the ability
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Calling this function uses the ability
    public void UseAbility()
    {
        //Debug.Log("Piercing Arrow Used");

        // Ability has been used, so it needs to cooldown
        isUsable = false;

        // Start the cooldown timer

        // Update the UI with the time remaining

        // Play the ability animation

        // Make a raycast for the projectile
        hits = Physics.RaycastAll(this.transform.position, this.transform.forward, 50, 1 << enemyLayerIndex);
        foreach (RaycastHit r in hits)
        {
            Debug.Log(r.collider.name);
            r.collider.gameObject.GetComponent<Health>().Damage(damage);
        }

        // DEBUG
        // Draw a debug ray to see the line
        Debug.DrawRay(this.transform.position, this.transform.forward * 50, Color.red);
    }
}