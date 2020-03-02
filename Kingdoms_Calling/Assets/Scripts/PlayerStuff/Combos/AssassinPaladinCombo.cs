/*
 * Assassin Paladin Combo
 * Resource: 
 * Created by: Bradley Williamson
 * On: 1/29/20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinPaladinCombo : MonoBehaviour
{
    // Public Variables
    [Header("Totem Specs")]
    public float range = 2f;
    public float upgradeLifeTime = 10f;
    public int damageHealMultiplier = 1;

    // Private Variables
    int healingSpringsMask;
    //float totemTick;
    //private bool isUsable;          // When ability is available for use, set this to true
    //private float waitTime = 40;    // Time in seconds needed to wait for ability cooldown
    //private float cooldownElapsed;  // When in cooldown, increments until waitTime is reached


    // Start is called before the first frame update
    void Start()
    {
        //Get the player and enemy layermask id's
        healingSpringsMask = LayerMask.NameToLayer("Totem");
        //isUsable = true;        // Ability starts as usable
        //cooldownElapsed = 0;    // Cooldown timer starts at 0
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Calling this function uses the ability
    public void ActivateCombo()
    {
        //Find the totem
        GameObject t = GameObject.Find("Collider_HealingSprings(Clone)");

        //if we find a totem then boostS
        if (t)
        {
            t.gameObject.GetComponent<HealingSpringCollider>().TotemBoost(range, damageHealMultiplier);
        }

    }
}
