/*
 * Paladin Earth Healing Spring Functionality
 * Resource: https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html
 * Created by: Bradley Williamson
 * On: 1/9/20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthHealingSpring : MonoBehaviour
{
    // Public Variables
    [Header("Totem Specs")]
    public float range = 1f;
    public float totalLifeTime = 10;
    public float damageHealInterval = 2f;
    public int healValue = 1;
    public int damageValue = 1;

    // Private Variables
    AssassinPaladinCombo assPalCombo;
    int playerLayerIndex, enemyLayerIndex;
    float totemTick, oldHealValue, oldDamageValue, oldRange, lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        //Get the player and enemy layermask id's
        playerLayerIndex = LayerMask.NameToLayer("Player");
        enemyLayerIndex = LayerMask.NameToLayer("Enemy");
        lifeTime = totalLifeTime;
        totemTick = damageHealInterval;
    }

    // Update is called once per frame
    void Update()
    {
        //Increment the damage interval
        totemTick -= Time.deltaTime;

        if (totemTick <= 0)
        {
            //Grab all colliders inside of the sphere which in our case acts as a circle with the player and enemy layer mask 
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, range, 1 << playerLayerIndex | 1 << enemyLayerIndex);

            //Uncomment to determine which colliders are being chosen
            //for (int j = 0; j < hitColliders.Length; j++)
            //{
            //    Debug.Log(hitColliders[j].name);
            //}

            //Loop through the colliders checking if its either a player or a enemy
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].gameObject.tag == "Player")
                {
                    Debug.Log("Heal " + hitColliders[i].name);
                    hitColliders[i].gameObject.GetComponentInChildren<Health>().Damage(healValue); //Heal the current colliders health by the current healValue
                }
                else if (hitColliders[i].gameObject.tag == "Enemy")
                {
                    Debug.Log("Damage " + hitColliders[i].name);
                    hitColliders[i].gameObject.GetComponentInChildren<Health>().Damage(-damageValue); //Damage the current colliders health by the current damageValue
                    if (hitColliders[i].GetComponent<ElementManager>().GetElementObject(hitColliders[i].gameObject) == ElementManager.ClassElement.Lightning) // if effected by lightning
                    {
                        assPalCombo.UseAbility();
                    }
                    //Else we have no element on the enemy apply the earth element
                    else
                    {
                        hitColliders[i].GetComponent<ElementManager>().ApplyElement(ElementManager.ClassElement.Earth);
                    }
                }
                i++;
            }
            //Reset the tick timer 
            totemTick = damageHealInterval;
        }
    }

    void FixedUpdate()
    {
        //Timer to keep the totem alive for only its lifetime
        lifeTime -= Time.deltaTime;
        //If lifetime reachs 0 destroy the totem
        if(lifeTime <= 0)
        {
            //Remove the totem
            Destroy(this.gameObject);
        }
    }

    public void TotemBoost(float rangeMultiplier, int damageHealMultiplier)
    {
        //hold old values
        oldRange = range;
        oldHealValue = healValue;
        oldDamageValue = damageValue;

        //update the specs of the totem and reset its life boosted
        range *= rangeMultiplier;
        damageValue *= damageHealMultiplier;
        healValue *= damageHealMultiplier;
        lifeTime = totalLifeTime;
    }
}
