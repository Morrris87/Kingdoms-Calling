using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherPaladinCombo : MonoBehaviour
{
    // ActivateCombo is called when an ability triggers an elemental proc on an enemy it hits
    public void ActivateCombo(GameObject target, ElementManager.ClassElement element)
    {
        // Determine who activated the combo (Archer or Paladin)
        if (element == ElementManager.ClassElement.Wind)    // If archer...
        {
            // Remove proc
            target.GetComponent<ElementManager>().thisElement = ElementManager.ClassElement.NONE;

            // Stop enemy movement (change AI state?)
            // Play rooting animation
            // Remove any damage modifiers (enemies take normal damage in this state)
        }
        else if (element == ElementManager.ClassElement.Earth)  // If paladin...
        {
            // Remove proc
            target.GetComponent<ElementManager>().thisElement = ElementManager.ClassElement.NONE;

            // Stop enemy movement (change AI state?)
            // Play rooting animation
            // Remove any damage modifiers (enemies take normal damage in this state)
        }
    }
}