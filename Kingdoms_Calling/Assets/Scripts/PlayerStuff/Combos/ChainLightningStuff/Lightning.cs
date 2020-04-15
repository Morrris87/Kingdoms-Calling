using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class Lightning : MonoBehaviour
{
    public float enemyRestruckDelay;
    public GameObject LightningPrefab;
    public GameObject passObj;

    public int damage;

    private void Start()
    {
        //LightningPrefab = GameObject.Find("SimpleLightningBoltAnimatedPrefab");        
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
            //Mark the enemy as hit
            h = other.gameObject.AddComponent<Hit>();

            //Deal damage
            enemy.GetComponent<Health>().Damage(damage);
            enemy.GetComponent<ElementManager>().ApplyElement(ElementManager.ClassElement.NONE);

            h.start = passObj;

            h.killDelay = enemyRestruckDelay;

            passObj = enemy.gameObject;

            LightningPrefab.GetComponent<Lightning>().passObj = passObj;
            Instantiate(LightningPrefab, other.gameObject.transform.position, Quaternion.identity);

            //Kill this gameObject once you have struck the closest enemy
            //Remove the Kill() if you want to strike everyone in the proximity
            //Kill();
        }
    }

    private void Update()
    {
        //Used to fix a bug where the animator stops early and doesnt kill itself
        if (GetComponent<Animator>().isActiveAndEnabled == false)
        {
            Kill();
        }
        //if(passObj == null)
        //{
        //    passObj = this.gameObject;
        //}

    }

    //Call this using an animation event, just in case the sphere strike nothing at all
    void Kill()
    {
        Destroy(gameObject);
    }
}