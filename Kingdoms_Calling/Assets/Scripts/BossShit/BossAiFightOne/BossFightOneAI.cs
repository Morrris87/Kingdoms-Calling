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
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject playerThree;
    public GameObject playerFour;

    //Target Symbol
    public GameObject targetSymbol;


    //Spawn Zones for the Skeletons
    public GameObject spawnZoneOne;
    public GameObject spawnZoneTwo;
    public GameObject spawnZoneThree;
    public GameObject spawnZoneFour;

    //BossPrefab
    public GameObject bossPrefab;

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
        BossDeathState death = new BossDeathState(this);
        BossAutoAttackState auto = new BossAutoAttackState(this);



        AddFSMState(auto);
        //AddFSMState();
        //AddFSMState();
        AddFSMState(death);
    }
}
