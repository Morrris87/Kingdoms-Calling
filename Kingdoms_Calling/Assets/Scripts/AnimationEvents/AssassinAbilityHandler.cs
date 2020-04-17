using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssassinAbilityHandler : MonoBehaviour
{
    //audio
    public AudioSource characterAudioSource;
    public AudioSource lightningStrikeSource;
    public AudioSource executionSource;
    public AudioClip footstepClip;
    public AudioClip lightningStrikeClip;
    public AudioClip executionClip;
    public AudioClip executionClip2;
    public AudioClip evasionClip;

    public GameObject assassin;
    public GameObject ChainLightningPrefab;
    public GameObject ArcherWarriorComboPrefab;
    public GameObject ArcherAssassinComboPrefab;
    public GameObject teleportLightningPrefab;

    private ThunderStrike thunderStrike;
    private Execution execution;

    // Combo variables
    private ArcherWarriorCombo archerWarriorCombo = new ArcherWarriorCombo();       // Used for calling the archer combo
    private AssassinWarriorCombo assassinWarriorCombo = new AssassinWarriorCombo(); // Used for calling the assassin combo
    private AssassinPaladinCombo assassinPaladinCombo = new AssassinPaladinCombo();
    private PaladinWarriorCombo paladinWarriorCombo = new PaladinWarriorCombo();    // Used for calling the paladin combo


    // Start is called before the first frame update
    void Start()
    {
        thunderStrike = GetComponentInParent<ThunderStrike>();
        execution = GetComponentInParent<Execution>();
    }

    public void StepEvent()
    {
        if (footstepClip != null)
        {
            characterAudioSource.clip = footstepClip;
            characterAudioSource.Play();
        }
    }

    public void EvasionAssEvent()
    {
        if (evasionClip != null)
        {
            characterAudioSource.clip = evasionClip;
            characterAudioSource.Play();
        }
    }
    public void LightningStrikeEvent()
    {
        if (lightningStrikeClip != null)
        {
            characterAudioSource.clip = lightningStrikeClip;
            characterAudioSource.Play();
        }
    }

    public void ExecutionSoundEvent()
    {
        if (executionClip != null)
        {
            characterAudioSource.clip = executionClip;
            characterAudioSource.Play();
        }
    }

    public void ExecutionSoundEvent2()
    {
        if (executionClip2 != null)
        {
            characterAudioSource.clip = executionClip2;
            characterAudioSource.Play();
        }
    }

    public void ThunderStrikeEvent()
    {
        // Teleport the player to the destination of the target
        assassin.transform.position = thunderStrike.playerDestPos.position;

        // Create the particles
        Instantiate(teleportLightningPrefab, transform.position, Quaternion.identity);
    }

    public void ThunderDamageEvent()
    {
        // Do 3x the assassin's normal damage to the target
        float dmgDealt = GetComponentInParent<BasicAttack>().CharacterAttackValue(BasicAttack.CharacterClass.Assassin) * 3;

        // Grab the enemy to damage
        Collider[] cols = Physics.OverlapBox(thunderStrike.abilityHitbox.bounds.center, thunderStrike.abilityHitbox.bounds.extents, thunderStrike.abilityHitbox.transform.rotation, LayerMask.GetMask("Enemy"));

        // Cycle through each collider in the cols array
        foreach (Collider c in cols)
        {
            // Deal damage to the enemy
            c.GetComponent<Health>().Damage((int)dmgDealt);

            // If the enemy currently has no element assigned in it's Element Manager...
            if (c.GetComponent<ElementManager>().effectedElement == ElementManager.ClassElement.NONE || c.GetComponent<ElementManager>().effectedElement == ElementManager.ClassElement.Lightning)
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
                    assassinPaladinCombo.ActivateCombo();
                }
                // If the enemy currently has a Wind proc...
                else if (c.GetComponent<ElementManager>().effectedElement == ElementManager.ClassElement.Wind)
                {
                    // Activate the Archer & Warrior combo
                    // Set the elemental proc to none
                    c.GetComponent<ElementManager>().ApplyElement(ElementManager.ClassElement.NONE);
                    
                    Instantiate(ArcherAssassinComboPrefab, transform.position, Quaternion.identity);                    
                    //GameObject.Find("Archer_Assassin_Combo").SetActive(true);
                    //Transform[] transforms = GameObject.Find("Archer_Assassin_Combo").GetComponentsInChildren<Transform>();
                    //for (int i = 0; i < transforms.Length; i++)
                    //{
                    //    transforms[i].gameObject.SetActive(true);
                    //}
                }
                // If the enemy currently has a Lightning proc...
                else if (c.GetComponent<ElementManager>().effectedElement == ElementManager.ClassElement.Fire)
                {
                    // Activate the Archer & Assassin combo
                    assassinWarriorCombo.ActivateCombo(ChainLightningPrefab, this.gameObject);
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
            int damageDealt = 6;

            // Do damage to the enemy
            c.GetComponent<Health>().Damage(damageDealt);
        }
    }
}