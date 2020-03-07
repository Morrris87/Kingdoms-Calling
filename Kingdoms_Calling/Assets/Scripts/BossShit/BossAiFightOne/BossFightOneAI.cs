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
    public FSMStateID id;

    public int m_CharNumber = 1;

    [HideInInspector]
    public Rigidbody rigBody;

    public int attackRange;
    public int chaseRange;

    public bool isTaunted = false;
    public float tauntDuration = 0;
    public bool isTargeted = false;

    public float bossAutoAttackCooldown = 2;

    public GameObject bossScreechHitBox;
    public float randomizPlayersInputsTimer = 3;
    public float screechTimer = 2;

    //Players
    [HideInInspector]
    public GameObject playerOne;
    [HideInInspector]
    public GameObject playerTwo;
    [HideInInspector]
    public GameObject playerThree;
    [HideInInspector]
    public GameObject playerFour;

    GameObject[] playerArray;
    //Target Symbol
    public GameObject targetSymbol;

    [HideInInspector]
    public int TimerThroneHasBeenActivated = 0;
    [HideInInspector]
    public int currentHealth;

    //Spawn Zones for the Skeletons
    public GameObject spawnZoneOne;
    public GameObject spawnZoneTwo;
    public GameObject spawnZoneThree;
    public GameObject spawnZoneFour;

    //BossPrefab
    public GameObject bossPrefab;


    // i want the lich to dissapear into a puff of smoke and then reappear on the throne
    public GameObject Throne;

    //boss Timer
    public float bossTimer = 2;

    //Spawn Zones for the Boss Clones
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
        bossAutoAttackCooldown = bossTimer;

        rigBody = GetComponent<Rigidbody>();
        bossStats = gameObject.GetComponent<BossStats>();
        spawnScript = gameObject.GetComponent<Spawn>();
        // Create the FSM for the player.
        ConstructFSM();
        //set up a Array of players
        for (int i = 0; i < playerArray.Length; i++)
        {
            if (i == 1)
            {
                playerOne = playerArray[i];
            }
            else if (i == 2)
            {
                playerTwo = playerArray[i];
            }
            else if (i == 3)
            {
                playerThree = playerArray[i];
            }
            else if (i == 4)
            {
                playerFour = playerArray[i];
            }
        }

    }

    // Update each frame.
    protected override void FSMUpdate()
    {
        objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerHealth = objPlayer.GetComponent<Health>();
        if (CurrentState != null)
        {
            CurrentState.Reason();
            CurrentState.Act();
            id = CurrentStateID;
        }
        if (CurrentState.ID == FSMStateID.Dead)
        {
        }
        if(randomizPlayersInputsTimer <= 3)
        {
            randomizPlayersInputsTimer -= Time.deltaTime;
        }

        if(bossTimer <= bossAutoAttackCooldown)
        {
            bossTimer -= Time.deltaTime;
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
        spawn.AddTransition(Transition.AllClonesKilled, FSMStateID.AutoAttack);


        BossMultiplyState multply = new BossMultiplyState(this);
        multply.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        multply.AddTransition(Transition.AllClonesKilled, FSMStateID.AutoAttack);


        BossScreechState screech = new BossScreechState(this);
        screech.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        screech.AddTransition(Transition.ScreechOnCooldown, FSMStateID.AutoAttack);

        SitOnThroneState sit = new SitOnThroneState(this);
        sit.AddTransition(Transition.BossSkeletonsDead, FSMStateID.AutoAttack);
        BossDeathState death = new BossDeathState(this);


        AddFSMState(auto);
        AddFSMState(spawn);
        AddFSMState(multply);
        AddFSMState(screech);
        AddFSMState(death);
    }
}
