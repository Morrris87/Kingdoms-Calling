//  Name: AI.cs
//  Author: ZAC KINDY
//  Function:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Complete;

public class AI : AdvancedFSM
{
    public FSMStateID id;

    public int m_CharNumber = 1;
    public NavMeshAgent navAgent;

    [HideInInspector]
    public Rigidbody rigBody;

    public string thisSkeletonClass; // might have to use this to set up classes 

    public int attackRange;
    public int chaseRange;

    public bool isTaunted = false;
    public float tauntDuration = 0;
    public bool isTargeted = false;

    public Animator animator;

    public GameObject targetSymbol;

    [HideInInspector]
    public SkeletonStats skeletonStats;
    [HideInInspector]
    public GameObject objPlayer;
    [HideInInspector]
    public Health playerHealth;

    GameObject[] Players;

    Spawn spawn;
    //public int CurrentBossSkelly;

    [HideInInspector]
    public GameObject thisSkeleton;
    int randOne = -1;

    [HideInInspector]
    public GameObject boss;

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
    //-------------------------------------
    //
    //think need to make the skeleton turn red when live deplets 
    //
    //-------------------------------------
  
    // Initialize the FSM for the NPC skeleton.
    protected override void Initialize()
    {
        rigBody = GetComponent<Rigidbody>();
        skeletonStats = gameObject.GetComponent<SkeletonStats>();
        boss = GameObject.FindGameObjectWithTag("Immune");
        // Create the FSM for the player.
        ConstructFSM();
        thisSkeleton = this.gameObject;

        //this line needs to be a player array

        Players = GameObject.FindGameObjectsWithTag("Player");
        if (randOne == -1)
        {
            randOne = Random.Range(0, Players.Length + 1);    
        }
    }
    // Update each frame.
    void Update()
    {  

        if (randOne == 1)
        {
            if(Players[0].activeSelf)
            objPlayer = Players[0];
        }
        else if (randOne == 2)
        {
            if (Players[1].activeSelf)
                objPlayer = Players[1];
        }
        else if (randOne == 3)
        {
            if (Players[2].activeSelf)
                objPlayer = Players[2];
        }
        else if (randOne == 4)
        {
            if (Players[3].activeSelf)
                objPlayer = Players[3];
        }
        else
        {
            if (Players[0].activeSelf)
                objPlayer = Players[0];
            else if(Players[1].activeSelf)
                objPlayer = Players[1];
            else if (Players[2].activeSelf)
                objPlayer = Players[2];
            else if (Players[3].activeSelf)
                objPlayer = Players[3];
        }
        if(isTaunted == true)
        {
            foreach(GameObject player in Players)
            {
                if(player.name == "Character_Paladin")
                {
                    objPlayer = player;
                }
            }
        }
        
        if (objPlayer != null)
        {
            playerHealth = objPlayer.GetComponent<Health>();
            if (playerHealth.isDead == true)
            {
                foreach (GameObject player in Players)
                {
                    if (player.GetComponent<Health>().isDead == true)
                    {
                        player.GetComponent<CharacterManager>().speed = 0;
                        
                    }
                }
                //Players = GameObject.FindGameObjectsWithTag("Player");
                randOne = Random.Range(0, Players.Length + 1);
            }
        }
        if (CurrentState != null)
        {
            CurrentState.Reason();
            CurrentState.Act();
            id = CurrentStateID;
        }
        if(CurrentState.ID == FSMStateID.Dead)
        {
            boss.GetComponent<BossFightOneAI>().bossSkellyUpdate -= 1;
            Debug.Log(boss.GetComponent<BossFightOneAI>().bossSkellyUpdate.ToString());
            Destroy(this.gameObject);
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


        IdleState idle = new IdleState(this);
        idle.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        idle.AddTransition(Transition.SawPlayer, FSMStateID.Chasing);

        ChargeState charge = new ChargeState(this);
        charge.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        charge.AddTransition(Transition.ReachPlayer, FSMStateID.Attacking);

        AttackState attack = new AttackState(this);
        attack.AddTransition(Transition.NoHealth, FSMStateID.Dead);
        attack.AddTransition(Transition.NotInAttackRange, FSMStateID.Chasing);

        DeathState death = new DeathState(this);

        
        AddFSMState(idle);
        AddFSMState(charge);
        AddFSMState(attack);
        AddFSMState(death);

        navAgent.speed = 3.0f;
    }

    private void OnEnable()
    {
        if (navAgent)
            navAgent.isStopped = false;
        if (CurrentState != null)
            PerformTransition(Transition.Enable);
    }
    private void OnDisable()
    {
        if (navAgent && navAgent.isActiveAndEnabled)
            navAgent.isStopped = true;
    }
}
