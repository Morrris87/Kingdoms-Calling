using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAssassinCombo : MonoBehaviour
{
    // ActivateCombo is called when an ability triggers an elemental proc on an enemy it hits
    public void ActivateCombo(GameObject target, int dmg)
    {
        // Remove proc
        target.GetComponent<ElementManager>().effectedElement = ElementManager.ClassElement.NONE;

        // Double the damage value
        int newDmg = dmg * 2;

        GameObject lightning = GameObject.Find("Archer_Assassin_Combo");
        if(lightning != null)
        {
            lightning.SetActive(true);
        }
        // Send the new damage value to the enemy's health
        target.GetComponent<Health>().Damage(newDmg);
    }
}