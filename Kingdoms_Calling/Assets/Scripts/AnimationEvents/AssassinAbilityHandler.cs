using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssassinAbilityHandler : MonoBehaviour
{
    public GameObject assassin;

    private ThunderStrike thunderStrike;
    private Execution execution;

    // Start is called before the first frame update
    void Start()
    {
        thunderStrike = GetComponentInParent<ThunderStrike>();
        execution = GetComponentInParent<Execution>();
    }

    public void ThunderStrikeEvent()
    {
        // Teleport the player to the destination of the target
        assassin.transform.position = thunderStrike.playerDestPos.position;

        // Do 3x the assassin's normal damage to the target
        float dmgDealt = GetComponentInParent<BasicAttack>().CharacterAttackValue(BasicAttack.CharacterClass.Assassin) * 3;

        //// Check enemy procs for combos
        //if (target.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.Wind)  // If enemy has a wind proc...
        //{
        //    // Activate the Archer combo
        //    archerAssassinCombo.ActivateCombo(target, dmgDealt);
        //}
        //else if (target.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.Earth)    // If enemy has a lightning proc...
        //{
        //    // Activate the Paladin combo
        //}
        //else if (target.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.Fire) // If enemy has a fire proc...
        //{
        //    // Activate the Warrior combo
        //}
        //else
        //{
        //    // Enemy has no proc and ability happens as normal
        //    target.GetComponent<Health>().Damage(dmgDealt);

        //    // Give enemies procs if appliciable
        //    if (target.GetComponent<ElementManager>().thisElement == ElementManager.ClassElement.NONE)
        //    {
        //        target.GetComponent<ElementManager>().thisElement = ElementManager.ClassElement.Lightning;
        //    }
        //}
    }

    public void ExecutionEvent()
    {
        // Grab the enemy to damage
        Collider[] cols = Physics.OverlapBox(execution.abilityHitbox.bounds.center, execution.abilityHitbox.bounds.extents, execution.abilityHitbox.transform.rotation, LayerMask.GetMask("Enemy"));

        // Cycle through each collider in the cols array
        foreach (Collider c in cols)
        {
            // Grab the enemy's health remaining
            int enemyHealth = c.GetComponent<Health>().currentHealth;

            // Subtract the enemyHealth from the enemy's max health
            int damageDealt = c.GetComponent<Health>().maxHealth - enemyHealth;

            // Do damage to the enemy
            c.GetComponent<Health>().Damage(damageDealt);
        }
    }
}