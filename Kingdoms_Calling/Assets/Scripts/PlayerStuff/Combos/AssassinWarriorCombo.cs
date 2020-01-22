using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinWarriorCombo : MonoBehaviour
{
    // Public Variables
    [Header("AssassinWarriorCombo Specs")]
    public float range = 1;
    public float lifeTime = 10f;
    public float damageInterval = 2f;
    //public int healValue = 1;
    public float igniteDuration = 1;
    public int damageValue = 1;
    public float igniteChance = 50;
    bool marked = false;
    int numPulses;

    private bool isUsable;          // When ability is available for use, set this to true
    private float waitTime = 40;    // Time in seconds needed to wait for ability cooldown
    private float cooldownElapsed;  // When in cooldown, increments until waitTime is reached

    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;        // Ability starts as usable
        cooldownElapsed = 0;    // Cooldown timer starts at 0
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Calling this function uses the ability
    public void UseAbility()
    {
        // Ability has been used, so it needs to cooldown
        isUsable = false;

        // Start the cooldown timer

        // Update the UI with the time remaining

        // Play the ability animation

    }
}
