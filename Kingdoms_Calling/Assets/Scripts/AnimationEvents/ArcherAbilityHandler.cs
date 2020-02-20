using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAbilityHandler : MonoBehaviour
{
    private ArrowVolley arrowVolley;
    private PiercingArrow piercingArrow;

    // Start is called before the first frame update
    void Start()
    {
        arrowVolley = GetComponentInParent<ArrowVolley>();
        piercingArrow = GetComponentInParent<PiercingArrow>();
    }

    public void ArrowVolleyEvent()
    {
        // Place the collder for the ability in the spawn area
        Instantiate(arrowVolley.areaOfEffect, arrowVolley.colliderDestPos.position, Quaternion.identity);
    }

    public void PiercingArrowEvent()
    {
        // Set the arrow's rotation to that of the player
        piercingArrow.piercingArrowPrefab.transform.rotation = transform.rotation;

        // Set the attacker to the player
        piercingArrow.piercingArrowPrefab.GetComponent<ProjectileDamage>().attacker = ProjectileDamage.Attacker.PLAYER;

        // Fire the piercing arrow shot
        Instantiate(piercingArrow.piercingArrowPrefab, GetComponentInParent<BasicAttack>().spawner.position, Quaternion.LookRotation(transform.forward, Vector3.up));
    }
}
