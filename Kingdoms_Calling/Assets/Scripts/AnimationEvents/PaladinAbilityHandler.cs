using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinAbilityHandler : MonoBehaviour
{
    private HealingSpring healingSpring;
    private Taunt taunt;

    // Start is called before the first frame update
    void Start()
    {
        healingSpring = GetComponentInParent<HealingSpring>();
        taunt = GetComponentInParent<Taunt>();
    }

    public void HealingSpringEvent()
    {
        // Instantiate the ability collider prefab on character location
        Instantiate(healingSpring.areaOfEffect, transform.position, Quaternion.identity);
    }

    public void TauntEvent()
    {
        // Place the collder for the ability in the spawn area
        Instantiate(taunt.areaOfEffect, transform.position, Quaternion.identity);
    }
}