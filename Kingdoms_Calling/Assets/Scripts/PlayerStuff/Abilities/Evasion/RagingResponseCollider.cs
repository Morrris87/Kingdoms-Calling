/*
 * Warrior Raging Response collider script
 * Created by: Bradley Williamson
 * On: 2/20/20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagingResponseCollider : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        if(damage == 0)
        {
            damage = 10;
        }
        // Damage all enemies in the collider when placed
        DamageEnemiesInCollider();

        Destroy(this, 1f); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DamageEnemiesInCollider()
    {
        // Grab all colliders in the hitbox of the ability
        Collider[] cols = Physics.OverlapBox(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.extents, GetComponent<Collider>().transform.rotation, LayerMask.GetMask("Enemy"));

        // Cycle through each collider in the cols array
        foreach (Collider c in cols)
        {
            c.gameObject.GetComponent<Health>().Damage(damage);
        }
    }
}
