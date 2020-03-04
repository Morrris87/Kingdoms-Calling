using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssassinAbilityHandler : MonoBehaviour
{
    public GameObject assassin;
    public GameObject ChainLightningPrefab;
    public GameObject ArcherWarriorComboPrefab;

    private ThunderStrike thunderStrike;
    private Execution execution;

    // Combo variables
    private ArcherWarriorCombo archerWarriorCombo = new ArcherWarriorCombo();       // Used for calling the archer combo
    private AssassinWarriorCombo assassinWarriorCombo = new AssassinWarriorCombo(); // Used for calling the assassin combo
    //private AssassinWarriorCombo assassinWarriorCombo;
    private PaladinWarriorCombo paladinWarriorCombo = new PaladinWarriorCombo();    // Used for calling the paladin combo
    private Text comboText;


    // Start is called before the first frame update
    void Start()
    {
        thunderStrike = GetComponentInParent<ThunderStrike>();
        execution = GetComponentInParent<Execution>();
        comboText = GameObject.FindGameObjectWithTag("ComboText").GetComponent<Text>();
    }

    public void ThunderStrikeEvent()
    {
        // Teleport the player to the destination of the target
        assassin.transform.position = thunderStrike.playerDestPos.position;

        // Do 3x the assassin's normal damage to the target
        float dmgDealt = GetComponentInParent<BasicAttack>().CharacterAttackValue(BasicAttack.CharacterClass.Assassin) * 3;

        // Grab the enemy to damage
        Collider[] cols = Physics.OverlapBox(execution.abilityHitbox.bounds.center, execution.abilityHitbox.bounds.extents, execution.abilityHitbox.transform.rotation, LayerMask.GetMask("Enemy"));

        // Cycle through each collider in the cols array
        foreach (Collider c in cols)
        {
            // Deal damage to the enemy
            c.GetComponent<Health>().Damage((int)dmgDealt);

            // If the enemy currently has no element assigned in it's Element Manager...
            if (c.GetComponent<ElementManager>().effectedElement == ElementManager.ClassElement.NONE)
            {
                // Set the elemental proc to Fire
                c.GetComponent<ElementManager>().effectedElement = ElementManager.ClassElement.Lightning;
            }
            else
            {
                // If the enemy currently has an Earth proc...
                if (c.GetComponent<ElementManager>().effectedElement == ElementManager.ClassElement.Earth)
                {
                    // Activate the Paladin & Warrior combo
                    paladinWarriorCombo.ActivateCombo(c.gameObject);
                    comboText.text = "Paladin & Assassin Combo Performed";
                }
                // If the enemy currently has a Wind proc...
                else if (c.GetComponent<ElementManager>().effectedElement == ElementManager.ClassElement.Wind)
                {
                    // Activate the Archer & Warrior combo
                    // Set the elemental proc to none
                    c.GetComponent<ElementManager>().ApplyElement(ElementManager.ClassElement.NONE);
                    Instantiate(ArcherWarriorComboPrefab, transform.position, Quaternion.identity);
                    comboText.text = "Archer & Assassin Combo Performed";
                }
                // If the enemy currently has a Lightning proc...
                else if (c.GetComponent<ElementManager>().effectedElement == ElementManager.ClassElement.Fire)
                {
                    // Activate the Archer & Assassin combo
                    assassinWarriorCombo.ActivateCombo(ChainLightningPrefab);
                    comboText.text = "Assassin & Warrior Combo Performed";
                }
            }
        }
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