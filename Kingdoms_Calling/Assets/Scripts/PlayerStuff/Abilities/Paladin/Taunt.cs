/*
 * Paladin Taunt Functionality
 * Resource: https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html
 * Created by: Bradley Williamson, Connor Larsen
 * On: 1/11/20
 */

using UnityEngine;
using UnityEngine.UI;

public class Taunt : MonoBehaviour
{
    // Public Variables
    public GameObject abilityCooldownUI;    // UI Element for the ability cooldown in the HUD
    public GameObject areaOfEffect;         // The collider for the Taunt ability
    public float waitTime = 40f;            // Time in seconds needed to wait for ability cooldown
    [HideInInspector] public bool isUsable; // When ability is available for use, set this to true

    // Private Variables
    private float cooldownTimer;    // When in cooldown, increments until waitTime is reached

    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;            // Ability starts as usable
        cooldownTimer = waitTime;   // Cooldown timer starts at the value of waitTime
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

    public void UseAbility()
    {
        abilityCooldownUI = GameObject.Find("PaladinSecondary_Cooldown");
        // If the ability is usable...
        if (isUsable == true)
        {
            // Ability has been used, so set ability as unusable
            isUsable = false;

            // Enable the cooldown UI
            abilityCooldownUI.transform.localScale = new Vector3(1f, 1f, 1f);

            // Play the ability animation
            GetComponentInChildren<Animator>().SetTrigger("TauntUsed");
        }
    }
}
