/*
 * Assassin Warrior Combo
 * Resource: C# recursion
 * Created by: Bradley Williamson
 * On: 1/23/20
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    bool onlyMarked = false;
    int maxChains = 10;

    //private bool isUsable;          // When ability is available for use, set this to true
    private float waitTime = 40;    // Time in seconds needed to wait for ability cooldown
    //private float cooldownElapsed;  // When in cooldown, increments until waitTime is reached
    private int chainCount;
    private int playerLayerIndex, enemyLayerIndex;      //Player and enemy layer index
    Dictionary<float, GameObject> distDic = new Dictionary<float, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //isUsable = true;        // Ability starts as usable
        //cooldownElapsed = 0;    // Cooldown timer starts at 0
        enemyLayerIndex = LayerMask.NameToLayer("Enemy");
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Calling this function uses the ability
    public void UseAbility()
    {

        // Start the cooldown timer if we need to implement for balancing

        LightingChain(this.gameObject, ref chainCount);
    }

    void LightingChain(GameObject obj , ref int chainCount)
    {
        //increment number of times we have chained
        chainCount++;
        //check if we are at our max chain length
        if (chainCount > maxChains)
        {
            //do nothing because we are out of chains
        }
        //if we arn't then keep chaining
        else
        {
            //Grab all colliders inside of the sphere which in our case acts as a circle with the player and enemy layer mask
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, range, 1 << enemyLayerIndex);

            //loop through colliders and fill a dictionary with distance as their keys
            foreach (Collider col in hitColliders)
            {
                //if we are only checking marked enemies
                if(onlyMarked)
                {
                    //check if the object we are checking distance is effected by a element AND it has the element of fire or lightning
                    if (col.gameObject.GetComponent<ElementManager>().effected && col.gameObject.GetComponent<ElementManager>().GetElement() == ElementManager.ClassElement.Fire || col.gameObject.GetComponent<ElementManager>().GetElement() == ElementManager.ClassElement.Lightning)
                    {
                        //calculate the distance between the current position and the current target
                        float dist = Vector3.Distance(obj.transform.position, col.transform.position);

                        //add to the dictionary
                        distDic.Add(dist, col.gameObject);
                    }
                }
                //else we check all colliders near the player
                else
                {
                    //calculate the distance between the current position and the current target
                    float dist = Vector3.Distance(obj.transform.position, col.transform.position);

                    //add to the dictionary
                    distDic.Add(dist, col.gameObject);
                }
            }

            //pull the distances from the dictionary
            List<float> distances = distDic.Keys.ToList();

            //sort the list
            distances.Sort();

            //Our closet collider will be the first in our dictionary
            GameObject closestEnemey = distDic[distances[0]];

            // Play the ability animation

            //Damage closest enemy
            closestEnemey.GetComponent<Health>().Damage(damageValue);
            //reset their effected status
            closestEnemey.GetComponent<ElementManager>().ApplyElement(ElementManager.ClassElement.NONE);

            //Recall lightning chain to keep chaining untill max chain count
            LightingChain(closestEnemey, ref chainCount);
        }
    }
}
