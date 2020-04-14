using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Execution : MonoBehaviour
{
    // Public Variables
    public GameObject abilityCooldownUI;    // UI element for the ability cooldown in the HUD
    public Collider abilityHitbox;          // Variable for the hitbox the ability will use to damage an enemy
    [HideInInspector] public bool isUsable; // When ability is available for use, set this to true

    // Private Variables
    public float waitTime = 40;    // Time in seconds needed to wait for ability cooldown
    private float cooldownTimer;    // When in cooldown, increments until waitTime is reached

    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;            // Ability starts as usable
        cooldownTimer = waitTime;   // Cooldown timer starts at 0
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

                // Update the UI with the abount of time remaining
                abilityCooldownUI.GetComponentInChildren<Text>().text = "" + ((int)cooldownTimer + 1);
            }
            // Otherwise cooldownTimer has completed
            else
            {
                isUsable = true;                    // Make ability useable again
                abilityCooldownUI.transform.localScale = new Vector3(0f, 0f, 0f); // Hide the cooldown UI
                cooldownTimer = waitTime;           // Reset the cooldownTimer
            }
        }
    }

    // Calling this function uses the ability
    public void UseAbility()
    {
        abilityCooldownUI = GameObject.Find("AssassinSecondary_Cooldown");
        // If the ability is usable...
        if (isUsable == true)
        {
            // Ability has been used, so it needs to cooldown
            isUsable = false;

            // Enable the cooldown UI
            abilityCooldownUI.transform.localScale = new Vector3(1f, 1f, 1f);

            // Play the ability animation
            GetComponentInChildren<Animator>().SetTrigger("ExecutionUsed");
        }
    }
}