using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinWarriorCombo : MonoBehaviour
{
    HealingSpringCollider totem;
    // ActivateCombo is called when an ability triggers an elemental proc on an enemy it hits
    public void ActivateCombo(GameObject target)
    {
        // Remove proc
        target.GetComponent<ElementManager>().effectedElement = ElementManager.ClassElement.NONE;

        // Sets the enemy to recieve true damage
        target.GetComponent<Health>().ActivateTrueDamage(true);

        totem = GameObject.Find("Collider_HealingSprings(Clone)").GetComponent<HealingSpringCollider>();
        totem.switchEffect();
    }
}