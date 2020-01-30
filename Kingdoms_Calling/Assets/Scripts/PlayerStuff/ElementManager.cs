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
    public ClassElement thisElement;
    public float elementTimeoutTime = 0;    
    public float igniteTick = 0;

    [HideInInspector]
    public bool effected;

    //Private Data
    ClassElement effectedElement;

    float effectedTimer;
    float igniteTimer;
    float igniteTotalTimePassed;
    float igniteDuration;
    int igniteDamage;
    bool ignited;
    

    // Start is called before the first frame update
    void Start()
    {
        //initialize all of our variables
        effectedTimer = 0;
        igniteDuration = 0;
        igniteTick = 0;
        effected = false;
        effectedElement = ClassElement.NONE;
        ignited = false;
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

            //update the element effect graphics
            DisplayElement();
            if(effectedTimer <= 0)
            {
                effected = false;
                effectedElement = ClassElement.NONE;
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
        }
        //Else we dont have a element
        else
        {
            eManager.effected = false;
            eManager.effectedElement = ClassElement.NONE;
            eManager.effectedTimer = 0;
        }
    }

    /// <summary>
    /// Function to handle getting the current element effecting a object
    /// </summary>
    /// <param name="obj">The object we are checking</param>
    /// <returns>The element that is currently effecting the object</returns>
    public ClassElement GetElement()
    {
        ClassElement targetElement = this.GetComponent<ElementManager>().effectedElement;

        return targetElement;
    }

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

    }

    public void IgniteThis(int dmgValue)
    {
        ignited = true;
        igniteDamage = dmgValue;
    }
}
