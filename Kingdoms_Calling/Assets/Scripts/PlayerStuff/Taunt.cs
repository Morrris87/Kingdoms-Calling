/*
 * Paladin Taunt Functionality
 * Resource: https://docs.unity3d.com/ScriptReference/Physics.OverlapSphere.html
 * Created by: Bradley Williamson
 * On: 1/11/20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taunt : MonoBehaviour
{
    // Public Variables
    [Header("Taunt Range")]
    public float range;
    [Header("Taunt Duration/Lifetime")]
    public float strengthLifetime;

    // Private Variables
    int enemyLayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        enemyLayerIndex = LayerMask.NameToLayer("Enemy");
    }

    public void tauntEnemies(float range, float strength)
    {
        taunt(range, strength);
    }

    void taunt(float range, float strength)
    {
        //Grab all colliders inside of the sphere which in our case acts as a circle with the enemy layer mask 
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, range, 1 << enemyLayerIndex);

        //Uncomment to determine which colliders are being chosen
        //for (int j = 0; j < hitColliders.Length; j++)
        //{
        //    Debug.Log(hitColliders[j].name);
        //}

        //Loop through the colliders
        int i = 0;
        while (i < hitColliders.Length)
        {
            hitColliders[i].gameObject.GetComponentInChildren<AI>().isTaunted = true;
            hitColliders[i].gameObject.GetComponentInChildren<AI>().tauntDuration = strength;
            i++;
        }
    }

}
