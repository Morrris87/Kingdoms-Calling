//  Name: ArrowVolley.cs
//  Author: ZAC KINDY
//  Function: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowVolley : MonoBehaviour
{
    public Image abilityUI; // UI sprite for the ability in the HUD

    // DEBUG
    public GameObject enemyTest;
    public GameObject circle;

    private bool isUsable;          // When ability is available for use, set this to true
    private float waitTime = 40;    // Time in seconds needed to wait for ability cooldown
    private float cooldownElapsed;  // When in cooldown, increments until waitTime is reached

    private Transform circleDestPos;  // The destination position for the player when ability is used

    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;        // Ability starts as usable
        cooldownElapsed = 0;    // Cooldown timer starts at 0
    }

    // Update is called once per frame
    void Update()
    {
        // DEBUG
        circleDestPos = enemyTest.transform;
    }

    // Calling this function uses the ability
    public void UseAbility()
    {
        Debug.Log("Ability 1 Used (Archer)");

        // Ability has been used, so it needs to cooldown
        isUsable = false;

        // Start the cooldown timer

        // Update the UI with the time remaining

        // Play the ability animation

        // Find the targeted enemy

        // Draw Circle On ground Around the targeted enemys position
        Instantiate(circle,circleDestPos);
        // Give enemies procs if appliciable

    }
}