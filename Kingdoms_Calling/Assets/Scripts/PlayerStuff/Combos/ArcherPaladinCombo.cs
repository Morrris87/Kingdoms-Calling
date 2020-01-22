using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherPaladinCombo : MonoBehaviour
{
    // ActivateCombo is called when an ability triggers an elemental proc on an enemy it hits
    public void ActivateCombo(GameObject target, int dmg, ElementManager.ClassElement element)
    {
        // Determine who activated the combo (Archer or Paladin)
        if (element == ElementManager.ClassElement.Wind)    // If archer...
        {

        }
        else if (element == ElementManager.ClassElement.Earth)  // If paladin...
        {

        }
    }
}