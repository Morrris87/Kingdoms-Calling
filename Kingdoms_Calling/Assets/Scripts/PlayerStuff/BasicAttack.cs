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

    [Header("Archer Assets")]
    public GameObject arrowPrefab;
    public Transform arrowSpawn;

    //Current weapons specs
    [Header("Current Attack Specs(Testing Feature)")]
    public float AttackRateSpeed = 2;
    public float AttackRange;
    public float AttackDamage;
    public float AttackHitChance;
    public float AttackStaminaLoss;

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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AttackRanged()
    {
        // Play Archer's attack animation

        // Create the arrow prefab
        arrowPrefab.transform.rotation = transform.rotation;
        Instantiate(arrowPrefab, arrowSpawn.position, Quaternion.LookRotation(transform.forward, Vector3.up));
    }

    public void AttackMelee()
    {

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
