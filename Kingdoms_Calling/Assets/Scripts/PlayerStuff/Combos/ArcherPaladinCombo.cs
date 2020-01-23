using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherPaladinCombo : MonoBehaviour
{
    // ActivateCombo is called when an ability triggers an elemental proc on an enemy it hits
    public void ActivateCombo(GameObject target, ElementManager.ClassElement element)
    {
        // Remove proc
        target.GetComponent<ElementManager>().thisElement = ElementManager.ClassElement.NONE;

        // Stop enemy movement
        // Play rooting animation
        // Remove any damage modifiers (enemies take normal damage in this state)
        target.GetComponent<Health>().ActivateTrueDamage(false);
    }
}