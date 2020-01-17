﻿//  Name: ArrowVolley.cs
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
    private float waitTime = 20;    // Time in seconds needed to wait for ability cooldown
    private float cooldownElapsed;  // When in cooldown, increments until waitTime is reached
    private float durationTimer;
    private float durationTime = 5;
    private GameObject createrArrowVolley;

    private Vector3 circleDestPos;  // The destination position for the player when ability is used

    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;        // Ability starts as usable
        cooldownElapsed = 0;    // Cooldown timer starts at 0

        //enemyTest = TargetedEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        // DEBUG
        //circleDestPos = enemyTest.transform.position;
        //createrArrowVolley = GameObject.FindGameObjectWithTag("ArrowVolley");

        if(isUsable == false)
        {
            if(cooldownElapsed <= waitTime) // Start the cooldown timer
            {
                cooldownElapsed += Time.deltaTime;
            }
            else
            {
                isUsable = true;
                abilityUI.enabled = true; //not sure if this works need to test with the targeting bull
                cooldownElapsed = 0;
            }
            if (createrArrowVolley != null)
            {
                if (durationTimer <= durationTime)
                {
                    durationTimer += Time.deltaTime;
                }
                else
                {
                    Destroy(createrArrowVolley);
                    durationTimer = 0;
                }
            }
            else
            {
                createrArrowVolley = circle;
            }
        }
    }

    // Calling this function uses the ability
    public void UseAbility(GameObject target)
    {
        if (isUsable == true)
        {
            if (target != null)
            {
                Debug.Log("Ability 1 Used (Archer)");

                // Ability has been used, so it needs to cooldown
                isUsable = false;

                // Play the ability animation and update the UI
                abilityUI.enabled = false; //not sure if this works need to test with the targeting bull

                // Draw Circle On ground Around the targeted enemys position
                Instantiate(circle, target.transform.position, Quaternion.identity);
                
                //apply damage to all enemys in circle range

                //foreach ()//enemy colliding with circle
               // {
                    
                //}

                //ADD TIMER FOR PROC AND SHIT!!!!!!!!!!!!!
                if(target.GetComponent<SkeletonStats>().proc == SkeletonStats.Proc.None)
                {
                    // Give enemies procs if appliciable
                    target.GetComponent<SkeletonStats>().proc = SkeletonStats.Proc.Wind;
                }
            }
        }

    }
    public void OnGUI()// Update the UI with the time remaining
    {
        GUI.Label(new Rect(60, 60, 30, 30), cooldownElapsed.ToString());
    }
}