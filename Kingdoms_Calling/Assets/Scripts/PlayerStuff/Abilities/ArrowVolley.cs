//  Name: ArrowVolley.cs
//  Author: ZAC KINDY
//  Function: 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowVolley : MonoBehaviour
{
    public Image abilityUI; // UI sprite for the ability in the HUD
    public GameObject areaOfEffect;

    private bool isUsable;          // When ability is available for use, set this to true
    private float waitTime = 20;    // Time in seconds needed to wait for ability cooldown
    private float cooldownElapsed;  // When in cooldown, increments until waitTime is reached
    private float durationTimer;
    private float durationTime = 5;
    private GameObject createrArrowVolley;
    private Collider[] enemiesInTarget;

    // Combo variables
    private ArcherAssassinCombo archerAssassinCombo;    // Used for calling the assassin combo
    private AssassinWarriorCombo archerWarriorCombo;    // Used for calling the warrior combo
    private AssassinPaladinCombo archerPaladinCombo;    // Used for calling the paladin combo

    private Vector3 circleDestPos;  // The destination position for the player when ability is used

    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;        // Ability starts as usable
        cooldownElapsed = 0;    // Cooldown timer starts at 0

        //enemyTest = TargetedEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        // DEBUG
        //circleDestPos = enemyTest.transform.position;
        //createrArrowVolley = GameObject.FindGameObjectWithTag("ArrowVolley");

        if(isUsable == false)
        {
            if(cooldownElapsed <= waitTime) // Start the cooldown timer
            {
                cooldownElapsed += Time.deltaTime;
            }
            else
            {
                isUsable = true;
                abilityUI.enabled = true; //not sure if this works need to test with the targeting bull
                cooldownElapsed = 0;
            }
            if (createrArrowVolley != null)
            {
                if (durationTimer <= durationTime)
                {
                    durationTimer += Time.deltaTime;
                }
                else
                {
                    Destroy(createrArrowVolley);
                    durationTimer = 0;
                }
            }
            else
            {
                createrArrowVolley = areaOfEffect;
            }
        }
    }

    // Calling this function uses the ability
    public void UseAbility(GameObject target)
    {
        if (isUsable == true)
        {
            // Ability has been used, so it needs to cool down
            isUsable = false;

            if (target != null)
            {
                // Start the cooldown timer

                // Update the UI with time remaining

                // Play the ability animation

                // Draw Circle On ground Around the targeted enemys position
                Instantiate(areaOfEffect, target.transform.position, Quaternion.identity);

                // Create an array of all enemies caught in the areaOfEffect
                ScanForEnemies(areaOfEffect.GetComponent<SphereCollider>());

                // Deal damage to each enemy in the array
                foreach (Collider enemy in enemiesInTarget)
                {
                    // Check enemy proc for combos
                    if (enemy.GetComponentInParent<ElementManager>().thisElement == ElementManager.ClassElement.Lightning)  // If enemy has a lightning proc...
                    {
                        // Activate the Assassin combo
                        archerAssassinCombo.ActivateCombo(target, archerDmg);
                    }
                    else if (enemy.GetComponentInParent<ElementManager>().thisElement == ElementManager.ClassElement.Earth)    // If enemy has a lightning proc...
                    {
                        // Activate the Paladin combo
                        //archerPaladinCombo.ActivateCombo(target, archerDmg);
                    }
                    else if (enemy.GetComponentInParent<ElementManager>().thisElement == ElementManager.ClassElement.Fire) // If enemy has a fire proc...
                    {
                        // Activate the Warrior combo
                        //archerWarriorCombo.ActivateCombo(target, archerDmg);
                    }
                    else
                    {
                        // Enemy has no proc and ability happens as normal
                        enemy.GetComponentInParent<Health>().Damage(archerDmg);

                        // Give enemies procs if appliciable
                        if (enemy.GetComponentInParent<ElementManager>().thisElement == ElementManager.ClassElement.NONE)
                        {
                            enemy.GetComponentInParent<ElementManager>().thisElement = ElementManager.ClassElement.Wind;
                        }
                    }
                }
            }
        }
    }

    // When called, scans the areaOfEffect for all enemies
    private void ScanForEnemies(SphereCollider targetArea)
    {
        Vector3 center = targetArea.transform.position + targetArea.center;
        float radius = targetArea.radius;

        enemiesInTarget = Physics.OverlapSphere(center, radius);
    }

    public void OnGUI()// Update the UI with the time remaining
    {
        //GUI.Label(new Rect(60, 60, 30, 30), cooldownElapsed.ToString());
    }
}