/*
 * Assassin Electric Dash Evasion ability
 * Created by: Bradley Williamson
 * On: 2/6/20
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectricDash : MonoBehaviour
{
    [Header("UI Element")]
    public GameObject abilityCooldownUI;    // UI element for the ability cooldown in the HUD

    [Header("Ability Specs")]
    public float dashooldown;     //Cooldown time for the ability itself
    public float dashLength;        //how far you can dash
    public float dashDuration;     //How long the dash lasts
    public int dashDamage;       //How much damage the skill does at each tick
    public float waitTime = 20f;            // Time in seconds needed to wait for ability cooldown
    public bool isActive = false;             // when the ability is inuse this is true;
    public GameObject destinationMarker;
    Vector2 leapLocation;
    [HideInInspector] public bool isUsable; // When ability is available for use, set this to true

    // Private Variables
    private float cooldownTimer;    // When in cooldown, increments until waitTime is reached


    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;                // Ability starts as usable
        cooldownTimer = waitTime;       // Cooldown timer starts at the value of waitTime
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
            }
        }
    }

    // Calling this function uses the ability
    public void UseAbility(GameObject indicatorLocation)
    {
        abilityCooldownUI = GameObject.Find("AssassinEvasion_Cooldown");
        // Ability has been used, so set ability as unusable
        isUsable = false;

        // Enable the cooldown UI
        abilityCooldownUI.transform.localScale = new Vector3(1f, 1f, 1f);

        // Play the ability animation
        StopCoroutine(MoveToPosition(transform, indicatorLocation.transform.position, dashDuration));
        if(isUsable == true)
        {
            // Start Ability
            StartCoroutine(MoveToPosition(transform, indicatorLocation.transform.position, dashDuration));
        }

    }

    /// <summary>
    /// Coroutine to move the players transform from its current position to a destination position in a given amount of time
    /// </summary>
    /// <param name="transform">Our current location</param>
    /// <param name="position">Our destination Position</param>
    /// <param name="timeToMove">Time to move to position</param>
    /// <returns></returns>
    IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        isUsable = false;   
        Vector3 currentPos = transform.position;
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }
}