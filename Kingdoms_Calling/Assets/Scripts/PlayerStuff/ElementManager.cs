/*
 * Element Manager used for combo attacks, etc
 * Resource: 
 * Created by: Bradley Williamson
 * On: 1/21/20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour
{
    //public enum ElementClass { NONE, Paladin, Warrior, Assassin, Archer };

    //Public Data
    public enum ClassElement { NONE, Earth, Fire, Lightning, Wind };

    [Header("Element Manager")]
    public ClassElement thisElement; // this enemies element if we choose to use
    public float elementTimeoutTime = 2;    //How long the element will last on a enemy
    public float igniteTick;    //How often ignite damage ticks
    public float elementRefreshRate = 2f; //How often we check to update the element objects above effected enemies
    public float igniteDuration = 5f;

    public GameObject fireMark;
    public GameObject windeMark;
    public GameObject lightningMark;
    public GameObject earthMark;

    //[HideInInspector]
    public bool effected;

    //Private Data
    public ClassElement effectedElement;

    public float effectedTimer;
    float igniteTimer;
    float igniteTotalTimePassed;
    int igniteDamage;
    bool ignited;
    float refreshRate;
    

    // Start is called before the first frame update
    void Start()
    {
        //initialize all of our variables
        effectedTimer = elementTimeoutTime;
        igniteTick = 0;
        igniteTimer = igniteDuration;
        igniteTotalTimePassed = 0;
        effected = false;
        effectedElement = ClassElement.NONE;
        ignited = false;
        refreshRate = elementRefreshRate;
    }

    // Update is called once per frame
    void Update()
    {
        //if effected by a element
        if (effected)
        {
            //countdown timer of effected element
            if (effectedTimer >= 0)
                effectedTimer -= Time.deltaTime;
            
            if (effectedTimer <= 0)
            {
                //reset our bool and element
                effected = false;
                effectedElement = ClassElement.NONE;
                //Loop through our transforms determining which one is the element marker and destroy it
                foreach (Transform child in transform.GetComponentsInChildren<Transform>())
                {
                    if(child.tag == "ElementMark")
                        GameObject.Destroy(child.gameObject);
                }
                
            }
        }
        //check if we are ignited
        if(ignited)
        {
            //update the ignite timer
            igniteTimer -= Time.deltaTime;

            //check if enough time has passed to damage 
            if(igniteTimer <= 0)
            {
                this.gameObject.GetComponent<Health>().Damage(igniteDamage);
                igniteTimer = igniteTick;
            }

            //check if the ignite should expire
            if(igniteTotalTimePassed >= igniteDuration)
            {
                //reset our values
                ignited = false;
                igniteTimer = 0;
            }
            igniteTotalTimePassed += Time.deltaTime;
        }
    }

    /// <summary>
    /// Function to call our element setting function
    /// </summary>
    public void ApplyElement(ClassElement inElement)
    {
        //Call the set element function
        SetElement(inElement);
    }

    /// <summary>
    /// Function to set the object passed in with a new element
    /// </summary>
    /// <param name="obj">The object we are effecting</param>
    /// <param name="inElement">The element we are applying</param>
    void SetElement(ClassElement inElement)
    {
        //Grab the objects manager we are trying to manipulate
        ElementManager eManager = this.GetComponent<ElementManager>();
        //If we have a element then update our variables and our timer
        if (inElement != ClassElement.NONE)
        {
            eManager.effected = true;
            eManager.effectedElement = inElement;
            eManager.effectedTimer = elementTimeoutTime;
            DisplayElement();
        }
        //Else we dont have a element
        else
        {
            eManager.effected = false;
            eManager.effectedElement = ClassElement.NONE;
            eManager.effectedTimer = 0;
            DisplayElement();
        }
    }

    /// <summary>
    /// Function to handle getting this objects effected element
    /// </summary>
    /// <returns>The element that is currently effecting the object</returns>
    public ClassElement GetElement()
    {
        ClassElement targetElement = this.GetComponent<ElementManager>().effectedElement;

        return targetElement;
    }

    /// <summary>
    /// Function to handle getting the current element effecting a object
    /// </summary>
    /// <param name="objIn">Pass in the object to get its effected element</param>
    /// <returns></returns>
    public ClassElement GetElementObject(GameObject objIn)
    {
        ClassElement targetElement = objIn.GetComponent<ElementManager>().effectedElement;

        return targetElement;
    }

    /// <summary>
    /// Function to handle displaying the element marker above the targets head
    /// </summary>
    void DisplayElement()
    {
        //We dont have a element
        if(!effected)
        {
            
        }
        // We have a element
        else if(effected)
        {
            //Check which element we are effected by
            if(effectedElement == ClassElement.Earth)
            {
                Instantiate(earthMark, transform);
            }
            else if (effectedElement == ClassElement.Fire)
            {
                Instantiate(fireMark, transform);
            }
            else if(effectedElement == ClassElement.Lightning)
            {
                Instantiate(lightningMark, transform);
            }
            else if(effectedElement == ClassElement.Wind)
            {
                Instantiate(windeMark, transform);
            }
            else
            {
                Debug.Log(this + " : Is effected by something but the element was not set.");
            }
        }
    }

    /// <summary>
    /// Function to Ignite the current gameobject that this script is on
    /// </summary>
    /// <param name="dmgValue">How much damage should be effected each tick</param>
    public void IgniteThis(int dmgValue)
    {
        ignited = true;
        igniteDamage = dmgValue;

    }
}
