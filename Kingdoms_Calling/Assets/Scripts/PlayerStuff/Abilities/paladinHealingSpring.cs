using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class paladinHealingSpring : MonoBehaviour
{
    public Image abilityUI; // The UI Images for the abilities
    public GameObject totemPrefab;
    public GameObject player;
    public float coolDown;    // Time in seconds needed to wait for ability cooldown

    private bool isUsable;          // When ability is available for use, set this to true
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
        if (cooldownElapsed >= 0)
            cooldownElapsed -= Time.deltaTime;
        else if (cooldownElapsed <= 0 && isUsable == false)
        {
            isUsable = true;
        }
    }

    // Calling this function uses the ability
    public void UseAbility()
    {
        if (isUsable)
        {
            // Ability has been used, so it needs to cooldown
            isUsable = false;

            // Start the cooldown timer
            cooldownElapsed = coolDown;
            // Update the UI with the time remaining

            // Play the ability animation

            //instantiate the totem prefab on character location
            Instantiate(totemPrefab, player.transform.position, Quaternion.identity);
        }
    }
}
