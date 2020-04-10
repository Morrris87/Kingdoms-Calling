/*
 * Warrior Raging Response
 * Created by: Bradley Williamson
 * On: 2/20/20
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RagingResponse : MonoBehaviour
{
    [Header("UI Element")]
    public GameObject abilityCooldownUI;    // UI element for the ability cooldown in the HUD

    [Header("Ability Specs")]
    public int rageDamage;       //How much damage the skill does at each tick
    public float waitTime = 20f;            // Time in seconds needed to wait for ability cooldown
    public GameObject RagingCollider;
    [HideInInspector] public bool isUsable; // When ability is available for use, set this to true

    // Private Variables
    private float cooldownTimer;    // When in cooldown, increments until waitTime is reached
    private Health health;


    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;                // Ability starts as usable
        cooldownTimer = waitTime;       // Cooldown timer starts at the value of waitTime
        health = gameObject.GetComponent<Health>();
        abilityCooldownUI.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // If ability has been used and hasn't refreshed...
        if (isUsable == false)
        {
            // If cooldownTimer hasn't completed...
            if (cooldownTimer >= 0f)
            {
                // Subtract cooldownTimer by deltaTime
                cooldownTimer -= Time.deltaTime;

                // Enable the cooldown UI
                abilityCooldownUI.transform.localScale = new Vector3(1f, 1f, 1f);

                // Update the UI with the amount of time remaining
                abilityCooldownUI.GetComponentInChildren<Text>().text = "" + ((int)cooldownTimer + 1);
            }
            // Otherwise cooldownTimer has completed
            else
            {
                isUsable = true;                    // Make ability useable again
                abilityCooldownUI.transform.localScale = new Vector3(0f, 0f, 0f); // Hide the cooldown UI
                cooldownTimer = waitTime;           // Reset the cooldownTimer
                health.ragingResponse = false;
            }
        }
    }

    // Calling this function uses the ability
    public void UseAbility()
    {
        abilityCooldownUI = GameObject.Find("WarriorEvasion_Cooldown");
        // Ability has been used, so set ability as unusable
        isUsable = false;
        Debug.Log("Warrior Evasion");

        // Enable the cooldown UI
        abilityCooldownUI.transform.localScale = new Vector3(1f, 1f, 1f);

        // Set raging response bool to true to check if we are 
        health.ragingResponse = true;
    }

    public void SpawnRagingResponse()
    {

        RagingCollider.GetComponent<RagingResponseCollider>().damage = rageDamage;
        Instantiate(RagingCollider);
    }
}