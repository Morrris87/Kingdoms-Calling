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

    [Header("Character Weapon(does nothing yet)")]
    public Weapon weaponType;
    //public WeaponSpecs[] weaponSpecs;

    //Current weapons specs
    [Header("Current Attack Specs(Testing Feature)")]
    public float AttackRateSpeed = 2;
    public float AttackRange;
    public float AttackDamage;
    public float AttackHitChance;
    public float AttackStaminaLoss;

    RaycastHit[] hits;
    private Vector3[] RaycastLocations = { new Vector3(0f, 0, 1.3f), new Vector3(-1f, 0, 1f), new Vector3(-0.5f, 0, 1f), new Vector3(1f, 0, 1f), new Vector3(0.5f, 0, 1f) };
    public float attackTimer = 0;

    //Current class enum type
    enum CharacterClass { NONE, Paladin, Warrior, Assassin, Archer };
    //Current class
    CharacterClass currentClass;
    //Player and enemy layer index
    int playerLayerIndex, enemyLayerIndex;

    // Start is called before the first frame update
    void Start()
    {
        //int enumCount = Enum.GetValues(typeof(Weapon)).Length;
        //weaponInfo = new WeaponSpecs[8];

        //Get the current class
        currentClass = (CharacterClass)this.gameObject.GetComponent<CharacterManager>().characterClass;
        //Get the player and enemy layermask id's
        playerLayerIndex = LayerMask.NameToLayer("Player");
        enemyLayerIndex = LayerMask.NameToLayer("Enemy");

    }

    // Update is called once per frame
    void Update()
    {
        //check if we have a class
        if (currentClass == CharacterClass.NONE)
        {
            //If our current selected class doesnt match the characters update stats
            if (currentClass != (CharacterClass)this.gameObject.GetComponent<CharacterManager>().characterClass)
                DetermineClass();
        }
        else if (currentClass != (CharacterClass)this.gameObject.GetComponent<CharacterManager>().characterClass)
        {
            DetermineClass();
        }
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
    }

    public void CallBasicAttack()
    {
        if (attackTimer <= 0)
        {
            Debug.Log("stats: " + " " + AttackRateSpeed + " " + AttackRange + " " + AttackDamage + " " + AttackHitChance + " " + AttackStaminaLoss);
            PreformBasicAttack(AttackRateSpeed, AttackRange, AttackDamage, AttackHitChance, AttackStaminaLoss);
        }
    }

    /// <summary>
    /// Preform the detection and damage dealing of a basic attack using RaycastAndDamage function
    /// </summary>
    /// <param name="atkRate">Attack rate/Tick interval</param>
    /// <param name="atkRange">Distance that the Raycast will go</param>
    /// <param name="atkDamage">Amount of damage that will be passed to the target</param>
    /// <param name="atkChance">Chance of the damage going through</param>
    /// <param name="atkStamina">Amount of stamina that will be taken by the attack</param>
    void PreformBasicAttack(float atkRate, float atkRange, float atkDamage, float atkChance, float atkStamina)
    {

        //Verify we can attack with our current stamina pool
        if (this.gameObject.GetComponent<Stamina>().GetStamina() >= atkStamina)
        {
            //Attack forward as all characters do this
            RaycastAndDamage(atkRate, atkRange, atkDamage, atkChance, atkStamina, RaycastLocations[0]);

            //Handle if we arnt the archer attacking to the left and the right to cover the front of the character
            if (currentClass != CharacterClass.Archer)
            {
                RaycastAndDamage(atkRate, atkRange, atkDamage, atkChance, atkStamina, RaycastLocations[1]);

                RaycastAndDamage(atkRate, atkRange, atkDamage, atkChance, atkStamina, RaycastLocations[2]);

                //Handle the furthest left and right raycasts to not work when you are paladin or assassin as they are smaller attackers
                if (currentClass != CharacterClass.Paladin || currentClass != CharacterClass.Assassin)
                {
                    RaycastAndDamage(atkRate, atkRange, atkDamage, atkChance, atkStamina, RaycastLocations[3]);

                    RaycastAndDamage(atkRate, atkRange, atkDamage, atkChance, atkStamina, RaycastLocations[4]);
                }
            }
            this.gameObject.GetComponent<Stamina>().UpdateStamina((int)-atkStamina);
            attackTimer = AttackRateSpeed;
        }

    }

    /// <summary>
    /// Function that handles raycasting and dealing damage to the enemies hit
    /// </summary>
    /// <param name="atkRate">Attack rate/Tick interval</param>
    /// <param name="atkRange">Distance that the Raycast will go</param>
    /// <param name="atkDamage">Amount of damage that will be passed to the target</param>
    /// <param name="atkChance">Chance of the damage going through</param>
    /// <param name="atkStamina">Amount of stamina that will be taken by the attack</param>
    /// <param name="raycastDir">Direction that the raycast will face</param>
    void RaycastAndDamage(float atkRate, float atkRange, float atkDamage, float atkChance, float atkStamina, Vector3 raycastDir)
    {
        hits = Physics.RaycastAll(transform.position, raycastDir, atkRange, 1 << enemyLayerIndex);
        foreach (RaycastHit r in hits)
        {
            Debug.Log(r.collider.name + " at : " + raycastDir);
            r.collider.gameObject.GetComponent<Health>().Damage((int)atkDamage);
        }
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
                AttackStaminaLoss = 12.0f;
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
