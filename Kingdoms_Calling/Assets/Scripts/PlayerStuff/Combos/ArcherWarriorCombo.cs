/*
 * Archer Warrior Combo Ability Functionality
 * Resource: https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html
 * Created by: Bradley Williamson
 * On: 1/21/20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class ArcherWarriorCombo : MonoBehaviour
{
    // Public Variables
    [Header("ArcherWarriorCombo Specs")]
    public float range = 1;
    public float lifeTime = 10f;
    public float damageInterval = 2f;
    public float igniteDuration = 1;
    public int damageValue = 1;
    public float igniteChance = 50;

    // Private Variables
    int enemyLayerIndex;
    float abilityTick;
    
    // Start is called before the first frame update
    void Start()
    {
        //Get the player and enemy layermask id's
        enemyLayerIndex = LayerMask.NameToLayer("Enemy");
        abilityTick = damageInterval;
    }

    // Update is called once per frame
    void Update()
    {
        abilityTick -= Time.deltaTime;

        //if we have ticked down enough time deal damage
        if(abilityTick <= 0)
        {
            //Grab all colliders inside of the sphere which in our case acts as a circle with the player and enemy layer mask 
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, range, LayerMask.GetMask("Enemy"));

            //Loop through the colliders checking if its either a player or a enemy
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].gameObject.tag == "Enemy")
                {
                    Debug.Log("Damage " + hitColliders[i].name);
                    hitColliders[i].gameObject.GetComponentInChildren<Health>().Damage(damageValue); //Damage the current colliders health by the current damageValue
                    hitColliders[i].gameObject.GetComponent<ElementManager>().ApplyElement(ElementManager.ClassElement.NONE);
                    //determine if this skeleton is going to be ignited
                    if (UsefullFunctions.RandomNumber(0.0f, 100.0f) >= igniteChance)
                    {
                        hitColliders[i].gameObject.GetComponent<ElementManager>().IgniteThis(damageValue);
                    }
                }
                i++;
            }

            //Reset the tick timer 
            abilityTick = damageInterval;

            //Play animation
            
        }

        //Timer to keep the ability alive for only its lifetime
        lifeTime -= Time.deltaTime;
        //If lifetime reachs 0 destroy the totem
        if (lifeTime <= 0)
        {
            //Remove the totem
            Destroy(this.gameObject);
        }
    }

    // Call this to activate the combo
    public void ActivateCombo()
    {

    }
}
