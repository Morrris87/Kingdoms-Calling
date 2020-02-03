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

    public GameObject targetSymbol;

    [HideInInspector]
    public SkeletonStats skeletonStats;
    [HideInInspector]
    public GameObject objPlayer;
    [HideInInspector]
    public Health playerHealth;

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

    }

    // Update each frame.
    protected override void FSMUpdate()
    {

        objPlayer = GameObject.FindGameObjectWithTag("Player");
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

        ////slot machine stff
        //pathTime += Time.deltaTime;
        //if (pathTime > 0.5f)
        //{
        //    pathTime = 0f;
        //    var slotManager = objPlayer.GetComponent<SlotManager>();
        //    if (slotManager != null)
        //    {
        //        if (slot == -1)
        //            slot = slotManager.Reserve(gameObject);
        //        if (slot == -1)
        //            return;
        //        var agent = GetComponent<NavMeshAgent>();
        //        if (agent == null)
        //            return;
        //        agent.destination = slotManager.GetSlotPosition(slot);
        //    }
        //}

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
