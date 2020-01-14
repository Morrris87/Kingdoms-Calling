//  Name: ThunderStrike.cs
//  Author: Connor Larsen
//  Function: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThunderStrike : MonoBehaviour
{
    public Image abilityUI; // UI sprite for the ability in the HUD

    private bool isUsable;          // When ability is available for use, set this to true
    private float waitTime = 40;    // Time in seconds needed to wait for ability cooldown
    private float cooldownElapsed;  // When in cooldown, increments until waitTime is reached

    private Vector3 playerStartPos, playerDestPos;      // Both the starting position and destination position for the player when ability is used
    [SerializeField] private GameObject abilityHitBox;  // Drag in a game object here to act as the hit box for the ability. Enemies inside are affected.

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
        // Ability has been used, so it needs to cooldown
        isUsable = false;

        // Start the cooldown timer

        // Update the UI with the time remaining

        // Play the ability animation

        // After animation, damage enemies inside the abilityHitBox

        // Give enemies procs if appliciable
        
    }
}