//  Name: BasicAttack.cs
//  Author: Connor Larsen, Bradley Williamson
//  Date: 2/7/2020

using UnityEngine;
using Complete;
using System.Collections;

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
    public Collider weaponHitbox;

    [Header("Archer Assets")]
    public GameObject arrowPrefab;
    public Transform spawner;

    //Current weapons specs
    [HideInInspector] public float AttackRateSpeed;
    [HideInInspector] public float AttackRange;
    [HideInInspector] public float AttackDamage;
    [HideInInspector] public float AttackHitChance;
    [HideInInspector] public float AttackStaminaLoss;

    [Header("Cooldown Variables")]
    public float attackTimer;
    private float cooldown;
    private bool cooldownActive;
    private LeapOfFaith leapOfFaith;

    FocusShot shot;
    //Current class enum type
    public enum CharacterClass { NONE, Paladin, Warrior, Assassin, Archer };
    //Current class
    CharacterClass currentClass;
    //Player and enemy layer index
    int playerLayerIndex, enemyLayerIndex;


    //zac stuff
    [HideInInspector]
    public bool zacAttackBool;
    // Start is called before the first frame update
    void Start()
    {
        zacAttackBool = false;
        shot = GetComponent<FocusShot>();
        DetermineClass();           // Fills in weapon specs based on which class the character is
        cooldown = AttackRateSpeed; // Sets up the cooldown timer based on the attack rate speed
        cooldownActive = true;      // Sets the cooldown check to true
        leapOfFaith = this.gameObject.GetComponent<LeapOfFaith>();
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
        }
        if(zacAttackBool == true && shot.Timer <= 12)
        {
            zacAttackBool = false;
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

            // Stops movement
            GetComponentInChildren<Animator>().SetBool("performingAction", true);

            if (!leapOfFaith.isActive)
            {                
                // Play Archer's attack animation
                GetComponentInChildren<Animator>().SetTrigger("Attacked");
            }

            //zac stuff
            //might have to make a nother bool that is set true here and false when in projectile damage after checking if its true
            if (zacAttackBool == false && shot.Timer >= 12)
            {
                shot.PassiveIndicator.SetActive(false);
                shot.passiveReady = false;
                zacAttackBool = true;
                shot.Timer = 0;
            }
        }
    }
    public void AttackMelee()
    {
        if (GetComponent<Stamina>().GetStamina() >= AttackStaminaLoss && cooldown <= 0f)
        {
            // Reset timer
            cooldownActive = true;
            cooldown = attackTimer;

            // Stops movement
            GetComponentInChildren<Animator>().SetBool("performingAction", true);

            // Play character's attack animation
            GetComponentInChildren<Animator>().SetTrigger("Attacked");
        }
    }

    public float CharacterAttackValue(CharacterClass characterClass)
    {
        if (currentClass != CharacterClass.NONE)
        {
            if (currentClass == CharacterClass.Paladin)
            {
                return 1f;
            }
            else if (currentClass == CharacterClass.Assassin)
            {
                return 1f;
            }
            else if (currentClass == CharacterClass.Archer)
            {
                return 1f;
            }
            else if (currentClass == CharacterClass.Warrior)
            {
                return 1f;
            }
            else
            {
                return 0f;
            }
        }
        else
        {
            return 0f;
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
                AttackDamage = 1.0f;
                AttackHitChance = 100;
                AttackStaminaLoss = 10.0f;
            }
            else if (currentClass == CharacterClass.Assassin)
            {
                AttackRateSpeed = 0.5f;
                AttackRange = 1;
                AttackDamage = 1.0f;
                AttackHitChance = 100;
                AttackStaminaLoss = 10.0f;
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
    private void OnGUI()
    {
        GUI.Label(new Rect(90, 60, 30, 30), zacAttackBool.ToString());
    }
}
