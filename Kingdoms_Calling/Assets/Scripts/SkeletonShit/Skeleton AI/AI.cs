﻿//  Name: AI.cs
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

    [HideInInspector]
    public GameObject thisSkeleton;

    //slot manager stuff
    //float pathTime = 0f;
    //int slot = -1;

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
        // Create the FSM for the player.
        ConstructFSM();
        thisSkeleton = this.gameObject;
        //thisSkeletonClass = spawn.skeletonClass;


        //this line needs to be a player array
        Players = GameObject.FindGameObjectsWithTag("Player");

    }

    // Update each frame.
    protected override void FSMUpdate()
    {
        //float time = 0f;
        //int rand =  Random.Range(1, 4);
        objPlayer = GameObject.FindGameObjectWithTag("Player");
        //if (time <= 0)
        //{
        //    if (rand == 1)
        //    {
        //        objPlayer = Players[1];
        //        time = 10f;
        //    }
        //    else if (rand == 2)
        //    {
        //        objPlayer = Players[2];
        //        time = 10f;
        //    }
        //    else if (rand == 3)
        //    {
        //        objPlayer = Players[3];
        //        time = 10f;
        //    }
        //    else if (rand == 4)
        //    {
        //        objPlayer = Players[4];
        //        time = 10f;
        //    }
        //}
        //else
        //{
        //    time -= Time.deltaTime;
        //}

        if (objPlayer != null)
        {
            playerHealth = objPlayer.GetComponent<Health>();
        }
        if (CurrentState != null)
        {
            CurrentState.Reason();
            CurrentState.Act();
            id = CurrentStateID;
        }
        if(CurrentState.ID == FSMStateID.Dead)
        {
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
