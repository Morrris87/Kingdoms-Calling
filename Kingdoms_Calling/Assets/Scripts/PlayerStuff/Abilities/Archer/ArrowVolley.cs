//  Name: ArrowVolley.cs
//  Author: Zac Kindy, Connor Larsen
//  Function: Places a collider at a point in front of the player which damages enemies inside over time

using UnityEngine;
using UnityEngine.UI;

public class ArrowVolley : MonoBehaviour
{
    // Public Variables
    public GameObject abilityCooldownUI;    // UI element for the ability cooldown in the HUD
    public GameObject areaOfEffect;         // The collider for the ArrowVolley ability
    public Transform colliderDestPos;       // The destination position for the collider when ability is used
    public float waitTime = 20f;            // Time in seconds needed to wait for ability cooldown
    [HideInInspector] public bool isUsable; // When ability is available for use, set this to true

    // Private Variables
    private float cooldownTimer;    // When in cooldown, increments until waitTime is reached

    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;            // Ability starts as usable
        cooldownTimer = waitTime;   // Cooldown timer starts at the value of waitTime
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

                // Update the UI with the amount of time remaining
                abilityCooldownUI.GetComponentInChildren<Text>().text = "" + ((int)cooldownTimer + 1);
            }
            // Otherwise cooldownTimer has completed
            else
            {
                isUsable = true;                    // Make ability useable again
                abilityCooldownUI.SetActive(false); // Hide the cooldown UI
                cooldownTimer = waitTime;           // Reset the cooldownTimer
            }
        }
    }

    // Calling this function uses the ability
    public void UseAbility()
    {
        // If the ability is usable...
        if (isUsable == true)
        {
            // Ability has been used, so set ability as unusable
            isUsable = false;

            // Enable the cooldown UI
            abilityCooldownUI.SetActive(true);

            // Play the ability animation
            GetComponentInChildren<Animator>().SetTrigger("ArrowVolleyUsed");

            // Place the collder for the ability in the spawn area
            Instantiate(areaOfEffect, colliderDestPos.position, Quaternion.identity);
        }
    }
}