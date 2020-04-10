//  Name: IronWall.cs
//  Author: Bradley Williamson
//  Date: 2/20/2020
//  Function: Paladins Evasion ability blocking damage from infront of the paladin

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IronWall : MonoBehaviour
{
    [Header("UI Element")]
    public GameObject abilityCooldownUI;    // UI element for the ability cooldown in the HUD
    public bool isActive;             // when the ability is inuse this is true;

    private Health health;
    private bool isUsable;          // When ability is available for use, set this to true
    private float waitTime = 40;    // Time in seconds needed to wait for ability cooldown



    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;        // Ability starts as usable
        isActive = false;
        health = gameObject.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        // If ability has been used and is currently held down...
        if (isUsable == false && isActive == true)
        {
            //Make the paladin immune to damage atm
            health.takeDamage = false;
        }
    }

    // Calling this function uses the ability
    public void UseAbility()
    {
        abilityCooldownUI = GameObject.Find("PaladinEvasion_Cooldown");
        // Ability has been used, so it needs to cooldown
        isUsable = false;
        isActive = true;
    }

    public void EndAbility()
    {
        abilityCooldownUI = GameObject.Find("PaladinEvasion_Cooldown");
        health.takeDamage = true;
        isUsable = true;
        isActive = false;
    }
}