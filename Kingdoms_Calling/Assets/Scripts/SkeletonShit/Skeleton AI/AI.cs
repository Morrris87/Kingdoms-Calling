using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Complete;

public class AI : AdvancedFSM
{
    
    public FSMStateID id;

    public static int SLOT_DIST = 1;
    public static int WAYPOINT_DIST = 1;

    public int m_CharNumber = 1;
    //public SlotManager coverPositionsSlotManager;
    public NavMeshAgent navAgent;

    [HideInInspector]
    public Rigidbody rigBody;

    public int attackRange = 2;
    public int chaseRange = 10;

    public bool isTaunted = false;
    public float tauntDuration = 0;
    public bool isTargeted = false;

    [HideInInspector]
    public SkeletonStats skeletonStats;
    

    //private SlotManager playerSlotManager;

    //public SlotManager GetPlayerSlot()
    //{
    //    return playerSlotManager;
    //}
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

    // Initialize the FSM for the NPC tank.
    protected override void Initialize()
    {
        // Find the Player and init appropriate data.
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        //playerSlotManager = objPlayer.GetComponent<SlotManager>();

        rigBody = GetComponent<Rigidbody>();
        skeletonStats = gameObject.GetComponent<SkeletonStats>();
        // Create the FSM for the player.
        ConstructFSM();

    }

    // Update each frame.
    protected override void FSMUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.Reason();
            CurrentState.Act();
            id = CurrentStateID;
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
