/*
 * Warrior Axe Whirlwind Ability
 * Resource: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Installation.html
 * Created by: Bradley Williamson, Connor Larsen
 * On: 1/15/20
 */

using UnityEngine;
using UnityEngine.UI;

public class AxeWhirlwind : MonoBehaviour
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
            GetComponentInChildren<Animator>().SetTrigger("SpinningAxeUsed");

            // Place the collder for the ability in the spawn area
            Instantiate(areaOfEffect, transform.position, Quaternion.identity);
        }
    }

    //void AttackAroundCharacter()
    //{
    //    //Debug.Log("Attacking Started");
    //    //Grab all colliders inside of the sphere which in our case acts as a circle with enemy layer mask 
    //    Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, whirlwindRadius, 1 << enemyLayerIndex);

    //    //Loop through the colliders checking if its either a player or a enemy
    //    int i = 0;
    //    while (i < hitColliders.Length)
    //    {
    //        eManager = hitColliders[i].GetComponent<ElementManager>();
    //        //if our combo is off cooldown then check elements
    //        if (comboCooldown <= 0)
    //        {
    //            // Check enemy procs for combos
    //            if (eManager.thisElement == ElementManager.ClassElement.Wind)  // If enemy has a wind proc...
    //            {
    //                // Activate the Archer combo
    //                //instantiate the archer warrior combo prefab on target location
    //                Instantiate(ArcherWarriorComboPrefab, hitColliders[i].transform.position, Quaternion.identity);
    //            }
    //            else if (eManager.thisElement == ElementManager.ClassElement.Earth)    // If enemy has a earth proc...
    //            {
    //                // Activate the Paladin combo
    //            }
    //            else if (eManager.thisElement == ElementManager.ClassElement.Lightning) // If enemy has a lightning proc...
    //            {
    //                // Activate the Assassin combo
    //                assassinWarriorCombo.UseAbility();
    //            }
    //        }
    //        //else our combo is on cooldown just do the normal damage
    //        else
    //        {
    //            // Enemy has no proc and ability happens as normal
    //            //Debug.Log("Whirlwind Damage " + hitColliders[i].name);
    //            hitColliders[i].gameObject.GetComponentInChildren<Health>().Damage(whirlwindDamage); //Damage the current colliders health by the current damageValue

    //            // Give enemies procs if appliciable
    //            if (eManager.thisElement == ElementManager.ClassElement.NONE)
    //            {
    //                eManager.thisElement = ElementManager.ClassElement.Fire;
    //            }
    //        }

    //        i++;
    //    }
    //    //Reset the tick timer 
    //    attackInterval = whirlwindAttackSpeed;
    //    //Debug.Log("Attacking Ended");
    //}
}
