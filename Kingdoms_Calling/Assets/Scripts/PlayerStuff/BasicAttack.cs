using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class BasicAttack : MonoBehaviour
{

    [System.Serializable]
    public class WeaponSpecs
    {
        public string name;//your name variable to edit
        public string Stat;
        public int Data;
    }

    //Current weapon enum type (Does nothing rn)
    public enum Weapon { NONE, MACE, SWORD, BOW, WARRIORAXE, HAMMER, DAGGERS, ARCHERBOW };
    //static int enumCount = Enum.GetValues(typeof(Weapon)).Length;

    [Header("Character Weapon")]
    public Weapon weaponType;
    //public WeaponSpecs[] weaponSpecs;

    [Header("Melee Assets")]
    public MeleeDamage hitBox;

    [Header("Archer Assets")]
    public GameObject arrowPrefab;
    public Transform arrowSpawn;

    //Current weapons specs
    private float AttackRateSpeed;
    private float AttackRange;
    private float AttackDamage;
    private float AttackHitChance;
    private float AttackStaminaLoss;

    [Header("Cooldown Variables")]
    public float attackTimer;
    private float cooldown;
    private bool cooldownActive;

    //Current class enum type
    enum CharacterClass { NONE, Paladin, Warrior, Assassin, Archer };
    //Current class
    CharacterClass currentClass;
    //Player and enemy layer index
    int playerLayerIndex, enemyLayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = attackTimer;
        cooldownActive = true;
        DetermineClass();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0.0f && cooldownActive)
        {
            cooldown -= Time.deltaTime;
        }

        if (cooldown <= 0.0f)
        {
            cooldownActive = false;
            hitBox.Attacking(false);
        }
    }

    public void AttackRanged()
    {
        // Check to see if player has enough stamina
        if (GetComponent<Stamina>().GetStamina() >= AttackStaminaLoss && cooldown <= 0f)
        {
            // Reset timer
            cooldownActive = true;
            cooldown = attackTimer;

            // Play Archer's attack animation
            GetComponentInChildren<Animator>().SetTrigger("Attacked");

            // Take away player's stamina
            GetComponent<Stamina>().DepleteStamina((int)AttackStaminaLoss);

            // Create the arrow prefab
            arrowPrefab.transform.rotation = transform.rotation;
            Instantiate(arrowPrefab, arrowSpawn.position, Quaternion.LookRotation(transform.forward, Vector3.up));
        }
    }

    public void AttackMelee()
    {
        // Reset timer
        cooldownActive = true;
        cooldown = attackTimer;

        // Play character's attack animation
        GetComponentInChildren<Animator>().SetTrigger("Attacked");

        // Take away player's stamina
        GetComponent<Stamina>().DepleteStamina((int)AttackStaminaLoss);

        // Activate the character's hitbox
        hitBox.Attacking(true);
    }

    void DetermineClass()
    {
        currentClass = (CharacterClass)this.gameObject.GetComponent<CharacterManager>().characterClass;
        if (currentClass != CharacterClass.NONE)
        {
            if (currentClass == CharacterClass.Paladin)
            {
                AttackRateSpeed = 2f;
                AttackRange = 1;
                AttackDamage = 1;
                AttackHitChance = 100;
                AttackStaminaLoss = 0;
            }
            else if (currentClass == CharacterClass.Assassin)
            {
                AttackRateSpeed = 0.5f;
                AttackRange = 1;
                AttackDamage = 1;
                AttackHitChance = 100;
                AttackStaminaLoss = 0;
            }
            else if (currentClass == CharacterClass.Archer)
            {
                AttackRateSpeed = 1f;
                AttackRange = 5.0f;
                AttackDamage = 1.0f;
                AttackHitChance = 80.0f;
                AttackStaminaLoss = 10.0f;
            }
            else if (currentClass == CharacterClass.Warrior)
            {
                AttackRateSpeed = 0.5f;
                AttackRange = 1;
                AttackDamage = 1.0f;
                AttackHitChance = 100;
                AttackStaminaLoss = 10.0f;
            }
            attackTimer = AttackRateSpeed;
        }
    }
}
