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

    [HideInInspector]
    public bool effected;

    //Private Data
    ClassElement effectedElement;
    float effectedTimer;

    // Start is called before the first frame update
    void Start()
    {
        //initialize all of our variables
        effectedTimer = 0;
        effected = false;
        effectedElement = ClassElement.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        if (effected)
        {
            //countdown timer of effected element
            if (effectedTimer >= 0)
                effectedTimer -= Time.deltaTime;

            DisplayElement();
        }
    }

    /// <summary>
    /// Function to call our element setting function
    /// </summary>
    public void ApplyElement(GameObject obj, ClassElement inElement)
    {
        //Call the set element function
        SetElement(obj, inElement);
    }

    /// <summary>
    /// Function to set the object passed in with a new element
    /// </summary>
    /// <param name="obj">The object we are effecting</param>
    /// <param name="inElement">The element we are applying</param>
    void SetElement(GameObject obj, ClassElement inElement)
    {
        //Grab the objects manager we are trying to manipulate
        ElementManager eManager = obj.GetComponent<ElementManager>();
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
    public ClassElement GetElement(GameObject obj)
    {
        ClassElement targetElement = obj.GetComponent<ElementManager>().effectedElement;

        return targetElement;
    }

    /// <summary>
    /// Function to handle displaying the element marker above the targets head
    /// </summary>
    void DisplayElement()
    {

    }
}
