using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherPaladinCombo : MonoBehaviour
{
    // ActivateCombo is called when an ability triggers an elemental proc on an enemy it hits
    public void ActivateCombo(GameObject target)
    {
        // Remove proc
        target.GetComponent<ElementManager>().thisElement = ElementManager.ClassElement.NONE;

        // Stop enemy movement
        target.GetComponent<AI>().enabled = false;

        // Play rooting animation
        target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 1, target.transform.position.z);

        // Remove any damage modifiers (enemies take normal damage in this state)
        target.GetComponent<Health>().ActivateTrueDamage(false);
    }
}