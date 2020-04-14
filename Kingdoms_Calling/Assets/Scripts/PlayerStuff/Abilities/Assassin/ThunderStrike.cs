//  Name: ThunderStrike.cs
//  Author: Connor Larsen
//  Function: Teleports to a nearby enemy, dealing three times the damage than normal

using UnityEngine;
using UnityEngine.UI;

public class ThunderStrike : MonoBehaviour
{
    // Public Variables
    public GameObject abilityCooldownUI;    // UI element for the ability cooldown in the HUD
    public float waitTime = 20;             // Time in seconds needed to wait for ability cooldown
    public Transform playerDestPos;         // The destination position for the player when ability is used
    public Text comboText;                  // Debug text for combos
    public Collider abilityHitbox;
    [HideInInspector] public bool isUsable; // When ability is available for use, set this to true

    // Private Variables
    private float cooldownTimer;    // When in cooldown, increments until waitTime is reached
    private float assassinDmg;      // Variable for the assassin's attack damage

    // Combo variables
    private ArcherAssassinCombo archerAssassinCombo;    // Used for calling the archer combo
    private AssassinWarriorCombo assassinWarriorCombo;  // Used for calling the warrior combo
    private AssassinPaladinCombo assassinPaladinCombo;  // Used for calling the paladin combo

    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;            // Ability starts as usable
        cooldownTimer = waitTime;   // Cooldown timer starts at the value of waitTime

        // Grab the value of the archers damage from BasicAttack.cs
        assassinDmg = GetComponent<BasicAttack>().CharacterAttackValue(BasicAttack.CharacterClass.Assassin);
        abilityCooldownUI.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // If ability has been used and hasn't refreshed...
        if (isUsable == false)
        {
            // If cooldownTimer hasn't completed...
            if (cooldownTimer >= 0)
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
    public void UseAbility(GameObject indicator)
    {
        abilityCooldownUI = GameObject.Find("AssassinPrimary_Cooldown");
        // If the ability is usable...
        if (isUsable == true)
        {
            // Ability has been used, so set ability as unusable
            isUsable = false;

            // Enable the cooldown UI
            abilityCooldownUI.transform.localScale = new Vector3(1f, 1f, 1f);

            playerDestPos = indicator.transform;

            // Play the ability animation
            GetComponentInChildren<Animator>().SetTrigger("ThunderStrikeUsed");
        }
    }
}