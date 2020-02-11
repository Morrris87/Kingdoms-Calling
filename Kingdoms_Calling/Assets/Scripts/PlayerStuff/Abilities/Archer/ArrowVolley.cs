//  Name: ArrowVolley.cs
//  Author: Zac Kindy, Connor Larsen
//  Function: 

using UnityEngine;
using UnityEngine.UI;

public class ArrowVolley : MonoBehaviour
{
    // Public Variables
    public Image abilityUI;             // UI sprite for the ability in the HUD
    public GameObject areaOfEffect;     // The collider for the ArrowVolley ability
    public Transform colliderDestPos;   // The destination position for the player when ability is used

    // Private Variables
    private bool isUsable;          // When ability is available for use, set this to true
    private float waitTime = 20;    // Time in seconds needed to wait for ability cooldown
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
            }
            // Otherwise cooldownTimer has completed
            else
            {
                isUsable = true;            // Make ability useable again
                cooldownTimer = waitTime;   // Reset the cooldownTimer
            }
        }
    }

    // Calling this function uses the ability
    public void UseAbility()
    {
        if (isUsable == true)
        {
            // Ability has been used, so start the cooldownTimer
            isUsable = false;

            // Play the ability animation

            // Place the collder for the ability in the spawn area
            Instantiate(areaOfEffect, colliderDestPos.position, Quaternion.identity);
        }
    }
}