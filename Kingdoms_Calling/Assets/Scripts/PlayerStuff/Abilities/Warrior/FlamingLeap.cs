/*
 * Warrior Flaming Leap Ability
 * Resource: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Installation.html
 * Created by: Bradley Williamson, Connor Larsen
 * On: 1/16/20
 */

using UnityEngine;
using UnityEngine.UI;

public class FlamingLeap : MonoBehaviour
{
    // Public Variables
    [Header("UI Element")]
    public GameObject abilityCooldownUI;    // UI element for the ability cooldown in the HUD
    public GameObject areaOfEffect;         // The collider for the FlamingLeap ability
    public Transform playerDestPos;         // The destination position the player will leap to when the ability is used
    public float waitTime = 20f;            // Time in seconds needed to wait for ability cooldown
    [HideInInspector] public bool isUsable; // When ability is available for use, set this to true

    // Private Variables
    private float cooldownTimer;    // When in cooldown, increments until waitTime is reached
    private Transform tempDestPos;  // Stores the position of the destination before the player moves

    // Old Variables (Might Need?)
    //public float leapDuration;     //How long the skill lasts
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
    public void UseAbility(Vector2 input)
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

            // Save the players destPos
            tempDestPos.position = playerDestPos.position;

            // Transfer the player to the destination position
            transform.position = playerDestPos.position;

            // Place the collder for the ability where the player lands
            Instantiate(areaOfEffect, transform.position, Quaternion.identity);

            //// preform the ability
            //leapCharacter(input);
            //AttackAroundCharacter();
            //GetComponent<CharacterManager>().abilityIndicator.SetActive(false);
            //GetComponent<CharacterManager>().displayLocation = false;
        }
    }

    //void leapCharacter(Vector2 inp)
    //{
    //    //Handle Moving the character
    //    //transform.position = (new Vector3(transform.position.x, transform.position.y, transform.position.z) + new Vector3(inp.x * leapDistance,0, inp.y * leapDistance));
    //    //transform.position = (new Vector3(transform.position.x, 0, transform.position.z) + new Vector3(inp.x * leapDistance, transform.position.y, inp.y * leapDistance));
    //}

    //void AttackAroundCharacter()
    //{
    //    //Debug.Log("Attacking Started");
    //    //Grab all colliders inside of the sphere which in our case acts as a circle with enemy layer mask 
    //    Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, leapRadius, 1 << enemyLayerIndex);

    //    int i = 0;
    //    while (i < hitColliders.Length)
    //    {
    //        //Debug.Log("Warroir Leap Damage " + hitColliders[i].name);
    //        hitColliders[i].gameObject.GetComponentInChildren<Health>().Damage(leapDamage); //Damage the current colliders health by the current damageValue

    //        i++;
    //    }
    //}
}
