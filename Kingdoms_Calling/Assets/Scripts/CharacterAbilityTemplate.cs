//  Name: CharacterAbilityTemplate.cs
//  Author: Connor Larsen
//  Function: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAbilityTemplate : MonoBehaviour
{
    public Image ability1UI, ability2UI;    // The UI Images for the abilities

    private bool isUsable;          // When ability is available for use, set this to true
    private float waitTime = 40;    // Time in seconds needed to wait for ability cooldown
    private float cooldownElapsed;  // When in cooldown, increments until waitTime is reached
    

    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;        // Ability starts as usable
        cooldownElapsed = 0;    // Cooldown timer starts at 0
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Calling this function uses the ability
    public void UseAbility()
    {
        isUsable = false;   // Ability has been used, so it needs to cooldown
        // Start the cooldown timer
        // Update the UI with the time remaining
        // Play the ability animation
        // Give enemies procs if appliciable
    }
}