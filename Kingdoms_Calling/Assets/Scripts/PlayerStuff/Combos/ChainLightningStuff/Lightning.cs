using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class Lightning : MonoBehaviour
{
    public float enemyRestruckDelay = 5f;
    private GameObject LightningPrefab;
    public GameObject passObj;

    private void Start()
    {
        LightningPrefab = GameObject.Find("SimpleLightningBoltAnimatedPrefab");
        if(passObj == null)
        {
            passObj = this.gameObject;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //try and see if we have AI entering our collider
        AI enemy = other.gameObject.GetComponent<AI>();

        if (enemy == null)
        {  // It is not enemy
            return;
        }

        //Try and get the hit component of the enemy
        Hit h = other.gameObject.GetComponent<Hit>();

        // the enemy is yet to be hit
        if (h == null)
        {
            //Call you lightning strike effect / particle here
            //create a obj or prefab with the Lightning script on it then set the start pos to the current thing calling and then the end to the enemy hit

            //Create another copy of this lightning field, by doing this, it will start chaining when the condition is right
            LightningPrefab.GetComponent<Lightning>().passObj = other.gameObject;
            Instantiate(LightningPrefab, other.gameObject.transform.position, Quaternion.identity);
            
            //Mark the enemy as hit
            h = other.gameObject.AddComponent<Hit>();

            h.start = passObj;

            h.killDelay = enemyRestruckDelay;

            //Kill this gameObject once you have struck the closest enemy
            //Remove the Kill() if you want to strike everyone in the proximity
            //Kill();
        }
    }

    //Call this using an animation event, just in case the sphere strike nothing at all
    void Kill()
    {
        Destroy(gameObject);
    }
}