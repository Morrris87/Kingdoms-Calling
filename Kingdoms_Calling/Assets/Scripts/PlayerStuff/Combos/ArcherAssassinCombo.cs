using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAssassinCombo : MonoBehaviour
{
    // ActivateCombo is called when an ability triggers an elemental proc on an enemy it hits
    public void ActivateCombo(GameObject target, int dmg)
    {
        // Remove proc
        target.GetComponent<ElementManager>().thisElement = ElementManager.ClassElement.NONE;

        // Double the damage value
        int newDmg = dmg * 2;

        // Send the new damage value to the enemy's health
        target.GetComponent<Health>().Damage(newDmg);
    }
}