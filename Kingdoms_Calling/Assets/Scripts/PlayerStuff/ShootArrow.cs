using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Shoot()
    {
        // Take away player's stamina
        GetComponent<Stamina>().DepleteStamina((int)GetComponentInParent<BasicAttack>().AttackStaminaLoss);

        // Create the arrow prefab
        GetComponentInParent<BasicAttack>().arrowPrefab.transform.rotation = transform.rotation;                                                // Set the arrow's rotation to that of the player
        GetComponentInParent<BasicAttack>().arrowPrefab.GetComponent<ProjectileDamage>().attacker = ProjectileDamage.Attacker.PLAYER;           // Set the attacker to the player
        Instantiate(GetComponentInParent<BasicAttack>().arrowPrefab, GetComponentInParent<BasicAttack>().spawner.position, Quaternion.LookRotation(transform.forward, Vector3.up)); // Fire the arrow
    }
}