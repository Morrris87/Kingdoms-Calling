//  Name: FSM.cs
//  Author: ZAC KINDY
//  Function:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using CompleteBossOne;

public class BossFightOneAI : BossFIghtOneAdvancedFSM
{
    public enum skeletonColour { white, grey, purple, none };
    public skeletonColour colour;
    // Public Variables
    //Players
    [Header("Player Objects")]
    public GameObject archer;
    public GameObject assassin;
    public GameObject paladin;
    public GameObject warrior;

    [Header("Boss Variables")]
    public FSMStateID id;
    public int m_CharNumber = 1;
    public Animator animator;

    [HideInInspector]
    public Rigidbody rigBody;

    public int attackRange;
    public int chaseRange;

    public bool isTaunted = false;
    public float tauntDuration = 0;
    public bool isTargeted = false;

    public float bossAutoAttackCooldown = 2;
    public float skeltonSpawnCooldown = 20;
    public float screechCoolDown = 30;

    [Header("Hitboxes")]
    public Collider bossScreechHitBox;
    public Collider bossMeleeHitbox;

    [Header("Timers")]
    public float randomizePlayersInputsTimer = 3;
    public float screechTimer = 30;
    public float bossTimer = 2;
    public float SpawnSkeletonsTimer = 20;

    //MIGHT HAVE TO CHANGE TO SKELETONCOUNTER
    public bool allSkeletonsSpawned;

    GameObject[] playerArray;

    [Header("Target Symbol")]
    //Target Symbol
    public GameObject targetSymbol;

    [HideInInspector]
    public int TimerThroneHasBeenActivated = 0;
    [HideInInspector]
    public int currentHealth;

    public float seventyFivePercentHp;
    bool seventyFiveUsed;
    public float fiftyPercentHp;
    bool fiftyUsed;
    public float twentyFivePercentHp;
    bool twentyFiveUsed;
    public bool atHpThreshold;
    //Spawn Zones for the Skeletons
    [Header("Skeleton Spawn Zones")]
    public GameObject spawnZoneOne;
    public GameObject spawnZoneTwo;
    public GameObject spawnZoneThree;
    public GameObject spawnZoneFour;

    //BossPrefab
    [Header("Boss Prefab")]
    public GameObject bossPrefab;

    // i want the lich to dissapear into a puff of smoke and then reappear on the throne
    [Header("Throne")]
    public GameObject Throne;

    //Spawn Zones for the Boss Clones
    [Header("Clone Spawn Zones")]
    public GameObject CloneOneSpawn;
    public GameObject CloneTwoSpawn;
    public GameObject CloneThreeSpawn;
    public GameObject CloneFourSpawn;

    [HideInInspector]
    public BossStats bossStats;
    [HideInInspector]
    public Spawn spawnScript;
    [HideInInspector]
    public GameObject objPlayer;
    [HideInInspector]
    public Health playerHealth;

    GameObject[] patrolArray;
    public List<GameObject> PatrolList;

    // this dow all for testing crap
    [HideInInspector]
    public string skeletonClass;

    //skeletons
    GameObject skeletonWhite;
    GameObject skeletonGrey;
    GameObject skeletonPurple;

    //Pack Size
    int whitePackNumber = 2;
    int greyPackNUmber = 3;
    int purplePackNumber = 1;

    //white
    public GameObject skeletonWhiteSword;
    public GameObject skeletonWhiteMace;
    public GameObject skeletonWhiteBow;
    public GameObject skeletonWhiteMage;
    //Grey
    public GameObject skeletonGreySword;
    public GameObject skeletonGreyMace;
    public GameObject skeletonGreyBow;
    public GameObject skeletonGreyMage;
    //purple
    public GameObject skeletonPurpleSword;
    public GameObject skeletonPurpleMace;
    public GameObject skeletonPurpleBow;
    public GameObject skeletonPurpleMage;
    List<GameObject> spawnZones;
    GameObject spawnZone;
    int packSize;

    //AI skelly;

    public int bossSkellyNumber;
    public int bossSkellyUpdate;

    private string GetStateString()
    {
        string state = "NONE";
        if (CurrentState != null)
        {
            if (CurrentState.ID == FSMStateID.Dead)
            {
                state = "DEAD";
            }
        }
        return state;
    }

    // Initialize the FSM for the NPC skeleton.s
    protected override void Initialize()
    {
        bossSkellyNumber = 0;
        bossAutoAttackCooldown = bossTimer;
        skeltonSpawnCooldown = SpawnSkeletonsTimer;
        screechCoolDown = screechTimer;
        allSkeletonsSpawned = false;
        PatrolList = new List<GameObject>();
        spawnZones = new List<GameObject>();

        atHpThreshold = false;
        seventyFiveUsed = false;
        fiftyUsed = false;
        twentyFiveUsed = false;

        seventyFivePercentHp = (int)(GetComponent<Health>().maxHealth * 0.75f);
        fiftyPercentHp = (int)(GetComponent<Health>().maxHealth * 0.50f);
        twentyFivePercentHp = (int)(GetComponent<Health>().maxHealth * 0.25f);

        rigBody = GetComponent<Rigidbody>();
        bossStats = gameObject.GetComponent<BossStats>();
        spawnScript = gameObject.GetComponent<Spawn>();

        playerArray = GameObject.FindGameObjectsWithTag("Player");
        patrolArray = GameObject.FindGameObjectsWithTag("PatrolSpot");
        // Create the FSM for the player.
        ConstructFSM();
        //set up a Array of players
        for (int i = 0; i < playerArray.Length; i++)
        {
            if (i == 0)
            {
                archer = playerArray[i];
            }
            else if (i == 1)
            {
                assassin = playerArray[i];
            }
            else if (i == 2)
            {
                paladin = playerArray[i];
            }
            else if (i == 3)
            {
                warrior = playerArray[i];
            }
        }
        foreach (GameObject obj in patrolArray)
        {
            PatrolList.Add(obj);
        }
        spawnZones.Add(CloneOneSpawn);
        spawnZones.Add(CloneTwoSpawn);
        spawnZones.Add(CloneThreeSpawn);
        spawnZones.Add(CloneFourSpawn);
    }    
    // Update each frame.
    protected override void FSMUpdate()
    {
        objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerHealth = objPlayer.GetComponent<Health>();
        currentHealth = GetComponent<Health>().currentHealth;
        if (bossSkellyUpdate == 0)
        {
            bossSkellyUpdate = bossSkellyNumber;
        }
        if (bossSkellyNumber != 0)
        {
            bossSkellyNumber = bossSkellyUpdate;
        }
        //bossSkellyNumber = skelly.CurrentBossSkelly;
        if (CurrentState != null)
        {
            CurrentState.Reason();
            CurrentState.Act();
            id = CurrentStateID;
        }
        if (CurrentState.ID == FSMStateID.Dead)
        {
            Debug.Log("ZacYou are dumb");
        }
        if(randomizePlayersInputsTimer <= 3)
        {
            randomizePlayersInputsTimer -= Time.deltaTime;
        }
        if(bossSkellyNumber == 0)
        {
            this.tag = "Boss";
        }
        if(currentHealth <= seventyFivePercentHp && seventyFiveUsed == false)
        {
            seventyFiveUsed = true;
            atHpThreshold = true;
        }
        else if (currentHealth <= fiftyPercentHp && fiftyUsed == false)
        {
            fiftyUsed = true;
            atHpThreshold = true;
        }
        else if (currentHealth <= twentyFivePercentHp && twentyFiveUsed == false)
        {
            twentyFiveUsed = true;
            atHpThreshold = true;
        }

        if (bossTimer <= bossAutoAttackCooldown)
        {
            bossTimer -= Time.deltaTime;
        }
        else
        {
           // bossTimer = bossAutoAttackCooldown;
        }
        if(SpawnSkeletonsTimer <= skeltonSpawnCooldown)
        {
            SpawnSkeletonsTimer -= Time.deltaTime;
        }
        else
        {
            SpawnSkeletonsTimer = skeltonSpawnCooldown;
        }
        if(screechTimer <= screechCoolDown)
        {
            screechTimer -= Time.deltaTime;
        }

        //making the target pop up on the skeleton
        if (isTargeted == true)
        {
            targetSymbol.SetActive(true);
        }
        else
        {
            targetSymbol.SetActive(false);
        }
    }

    public GameObject ChooseSkeletonClass(GameObject sword, GameObject mace, GameObject bow, GameObject mage)
    {
        if (Random.value > 0.4)
        {
            skeletonClass = "Sword";
        }//50% sword
        else if (Random.value > 0.3)
        {
            skeletonClass = "Mace";
        }//30% mace
        else if (Random.value > 0.2)
        {
            skeletonClass = "Bow";
        }
        else if (Random.value > 0.1)//10% Mage
        {
            skeletonClass = "Mage";
        }

        if (skeletonClass == "Sword")
        {
            return sword;
        }//returns the Sword skeleton Prefab
        else if (skeletonClass == "Mace")
        {
            return mace;
        }//returns the Mace skeleton Prefab
        else if (skeletonClass == "Bow")
        {
            return bow;
        }
        else if (skeletonClass == "Mage")
        {
            return mage;
        }
        //returns the Bow skeleton Prefab
        return null;
    }

    public void spawnSkeletonsForBoss(string tempColour)
    {
        for (int b = 0; b < 4; b++)
        {
            spawnZone = spawnZones[b];
            if (tempColour == "white")
            {
                packSize = whitePackNumber;
                for (int i = 0; i < packSize; i++)
                {
                    if (i == 0)
                    {
                        skeletonWhite = ChooseSkeletonClass(skeletonWhiteSword, skeletonWhiteMace, skeletonWhiteBow, skeletonWhiteMage);
                        Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x - 2f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                        skeletonWhite.transform.localScale = new Vector3(2, 2, 2);
                        skeletonWhite.GetComponent<AI>().chaseRange = 100;
                        bossSkellyNumber += 1;
                    }
                    else if (i == 1)
                    {
                        skeletonWhite = ChooseSkeletonClass(skeletonWhiteSword, skeletonWhiteMace, skeletonWhiteBow, skeletonWhiteMage);
                        Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x - 2f, spawnZone.transform.position.y, spawnZone.transform.position.z - 8f), Quaternion.identity);
                        skeletonWhite.transform.localScale = new Vector3(2, 2, 2);
                        skeletonWhite.GetComponent<AI>().chaseRange = 100;
                        bossSkellyNumber += 1;
                    }
                    else if (i == 2)
                    {
                        skeletonWhite = ChooseSkeletonClass(skeletonWhiteSword, skeletonWhiteMace, skeletonWhiteBow, skeletonWhiteMage);
                        Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x - 2f, spawnZone.transform.position.y, spawnZone.transform.position.z - 4f), Quaternion.identity);
                        skeletonWhite.transform.localScale = new Vector3(2, 2, 2);
                        skeletonWhite.GetComponent<AI>().chaseRange = 100;
                        bossSkellyNumber += 1;
                    }
                    else if (i == 3)
                    {
                        skeletonWhite = ChooseSkeletonClass(skeletonWhiteSword, skeletonWhiteMace, skeletonWhiteBow, skeletonWhiteMage);
                        Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x + 2f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                        skeletonWhite.transform.localScale = new Vector3(2, 2, 2);
                        skeletonWhite.GetComponent<AI>().chaseRange = 100;
                        bossSkellyNumber += 1;
                    }
                    else if (i == 4)
                    {
                        skeletonWhite = ChooseSkeletonClass(skeletonWhiteSword, skeletonWhiteMace, skeletonWhiteBow, skeletonWhiteMage);
                        Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x + 2f, spawnZone.transform.position.y, spawnZone.transform.position.z - 8f), Quaternion.identity);
                        skeletonWhite.transform.localScale = new Vector3(2, 2, 2);
                        skeletonWhite.GetComponent<AI>().chaseRange = 100;
                        bossSkellyNumber += 1;
                    }
                    else if (i == 5)
                    {
                        skeletonWhite = ChooseSkeletonClass(skeletonWhiteSword, skeletonWhiteMace, skeletonWhiteBow, skeletonWhiteMage);
                        Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x + 2f, spawnZone.transform.position.y, spawnZone.transform.position.z - 4f), Quaternion.identity);
                        skeletonWhite.transform.localScale = new Vector3(2, 2, 2);
                        skeletonWhite.GetComponent<AI>().chaseRange = 100;
                        bossSkellyNumber += 1;
                    }
                }
            }
            else if (tempColour == "grey")
            {
                packSize = greyPackNUmber;
                for (int i = 0; i < packSize; i++)
                {
                    if (i == 0)
                    {
                        skeletonGrey = ChooseSkeletonClass(skeletonGreySword, skeletonGreyMace, skeletonGreyBow, skeletonGreyMage);
                        Instantiate(skeletonGrey, spawnZone.transform.position, Quaternion.identity);
                        skeletonGrey.transform.localScale = new Vector3(2, 2, 2);
                        skeletonGrey.GetComponent<AI>().chaseRange = 100;
                        bossSkellyNumber += 1;
                    }
                    else if (i == 1)
                    {
                        skeletonGrey = ChooseSkeletonClass(skeletonGreySword, skeletonGreyMace, skeletonGreyBow, skeletonGreyMage);
                        Instantiate(skeletonGrey, new Vector3(spawnZone.transform.position.x + 4f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                        skeletonGrey.transform.localScale = new Vector3(2, 2, 2);
                        skeletonGrey.GetComponent<AI>().chaseRange = 100;
                        bossSkellyNumber += 1;
                    }
                    else if (i == 2)
                    {
                        skeletonGrey = ChooseSkeletonClass(skeletonGreySword, skeletonGreyMace, skeletonGreyBow, skeletonGreyMage);
                        Instantiate(skeletonGrey, new Vector3(spawnZone.transform.position.x - 4f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                        skeletonGrey.transform.localScale = new Vector3(2, 2, 2);
                        skeletonGrey.GetComponent<AI>().chaseRange = 100;
                        bossSkellyNumber += 1;
                    }
                }
            }
            else if (tempColour == "purple")
            {
                packSize = purplePackNumber;
                for (int i = 0; i < packSize; i++)
                {
                    skeletonPurple = ChooseSkeletonClass(skeletonPurpleSword, skeletonPurpleMace, skeletonPurpleBow, skeletonPurpleMage);
                    Instantiate(skeletonPurple, spawnZone.transform.position, Quaternion.identity);
                    //skeletonPurple.GetComponent<AI>().thisSkeletonClass = skeletonClass;
                    skeletonPurple.GetComponent<AI>().chaseRange = 100;
                    bossSkellyNumber += 1;
                }
            }
        }
    }

    private void ConstructFSM()
    {
        // Add transitions
        
        BossAutoAttackState auto = new BossAutoAttackState(this);
        auto.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        auto.AddTransition(Transition.CastMultiply, FSMStateID.Multiply);
        auto.AddTransition(Transition.CastSpawnSkeletons, FSMStateID.SpawnSkeletons);
        auto.AddTransition(Transition.CastScreech, FSMStateID.Sceach);

        BossSpawnSkeletonsState spawn = new BossSpawnSkeletonsState(this);
        spawn.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        spawn.AddTransition(Transition.ToPatrol, FSMStateID.Patrol);

        BossPatrolState patrol = new BossPatrolState(this);
        patrol.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        patrol.AddTransition(Transition.ToAutoAttack, FSMStateID.AutoAttack);

        BossMultiplyState multply = new BossMultiplyState(this);
        multply.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        multply.AddTransition(Transition.ToPatrol, FSMStateID.Patrol);


        BossScreechState screech = new BossScreechState(this);
        screech.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        screech.AddTransition(Transition.ToPatrol, FSMStateID.Patrol);

        SitOnThroneState sit = new SitOnThroneState(this);
        sit.AddTransition(Transition.BossSkeletonsDead, FSMStateID.AutoAttack);
        BossDeathState death = new BossDeathState(this);


        AddFSMState(auto);
        AddFSMState(patrol);
        AddFSMState(spawn);
        AddFSMState(multply);
        AddFSMState(screech);
        AddFSMState(death);
    }
}
