/*
 * Warrior Flaming Leap Ability
 * Resource: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Installation.html
 * Created by: Bradley Williamson, Connor Larsen
 * On: 1/16/20
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlamingLeap : MonoBehaviour
{
    // Public Variables
    [Header("UI Element")]
    public GameObject abilityCooldownUI;    // UI element for the ability cooldown in the HUD

    [Header("FlamingLeap Specs")]
    public float leapDuration;              //How long the skill lasts
    public GameObject areaOfEffect;         // The collider for the FlamingLeap ability
    public Transform playerDestPos;         // The destination position the player will leap to when the ability is used
    public float waitTime = 20f;            // Time in seconds needed to wait for ability cooldown
    public float leapDistance;
    public Text comboText;                  // Debug text for combos
    [HideInInspector] public bool isUsable; // When ability is available for use, set this to true

    // Private Variables
    private float cooldownTimer;    // When in cooldown, increments until waitTime is reached
    private Transform tempDestPos;  // Stores the position of the destination before the player moves

    // Old Variables (Might Need?)
    //public int leapDamage;       //How much damage the skill does at each tick
    //public float animationLength;

    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;                // Ability starts as usable
        cooldownTimer = waitTime;       // Cooldown timer starts at the value of waitTime
        tempDestPos = playerDestPos;    // Initialize the tempDestPos variable
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
    public void UseAbility(GameObject indicatorLocation)
    {
        // If the ability is usable...
        if (isUsable == true)
        {
            // Ability has been used, so set ability as unusable
            isUsable = false;

            // Enable the cooldown UI
            abilityCooldownUI.SetActive(true);

            // Play the ability animation (handle player location)
            GetComponentInChildren<Animator>().SetTrigger("FlamingLeapUsed");

            // Transfer the player to the destination position then when done call our attack
            StartCoroutine(MoveToPosition(transform, indicatorLocation.transform.position, leapDuration));
            
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
        Vector3 currentPos = transform.position;
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }

        // THIS HAS BEEN MOVED TO ANOTHER LOCATION, LEFT COMMENTED JUST IN CASE
        //// Place the collder for the ability where the player lands
        //Instantiate(areaOfEffect, transform.position, Quaternion.identity);
    }



}
