﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Complete
{
    public enum Transition
    {
        None = 0,
        SawPlayer,
        NotInAttackRange,
        Enable,
        ReachPlayer,
        NoHealth,
        Charge
    }
    public enum FSMStateID
    {
        None = 0,
        Idle,
        Chasing,
        Attacking,
        Dead,
    }
    public class AdvancedFSM : FSM
    {
        private List<FSMState> fsmStates;

        private FSMStateID currentStateID;
        public FSMStateID CurrentStateID { get { return currentStateID; } }

        private FSMState currentState;
        public FSMState CurrentState { get { return currentState; } }

        [HideInInspector]
        public GameObject Player;

        public AdvancedFSM()
        {
            fsmStates = new List<FSMState>();
        }
        public void AddFSMState(FSMState fsmState)
        {
            // Check for Null reference before deleting
            if (fsmState == null)
            {
                Debug.LogError("FSM ERROR: Null reference is not allowed");
            }

            // First State inserted is also the Initial state
            //   the state the machine is in when the simulation begins
            if (fsmStates.Count == 0)
            {
                fsmStates.Add(fsmState);
                currentState = fsmState;
                currentStateID = fsmState.ID;
                return;
            }

            // Add the state to the List if it´s not inside it
            foreach (FSMState state in fsmStates)
            {
                if (state.ID == fsmState.ID)
                {
                    Debug.LogError("FSM ERROR: Trying to add a state that was already inside the list");
                    return;
                }
            }

            //If no state in the current then add the state to the list
            fsmStates.Add(fsmState);
        }
        public void DeleteState(FSMStateID fsmState)
        {
            // Check for NullState before deleting
            if (fsmState == FSMStateID.None)
            {
                Debug.LogError("FSM ERROR: bull id is not allowed");
                return;
            }

            // Search the List and delete the state if it´s inside it
            foreach (FSMState state in fsmStates)
            {
                if (state.ID == fsmState)
                {
                    fsmStates.Remove(state);
                    return;
                }
            }
            Debug.LogError("FSM ERROR: The state passed was not on the list. Impossible to delete it");
        }
        public void PerformTransition(Transition trans)
        {
            // Check for NullTransition before changing the current state
            if (trans == Transition.None)
            {
                Debug.LogError("FSM ERROR: Null transition is not allowed");
                return;
            }

            // Check if the currentState has the transition passed as argument
            FSMStateID id = currentState.GetOutputState(trans);
            if (id == FSMStateID.None)
            {
                Debug.LogError("FSM ERROR: Current State does not have a target state for this transition");
                return;
            }

            // Update the currentStateID and currentState		
            currentStateID = id;
            foreach (FSMState state in fsmStates)
            {
                if (state.ID == currentStateID)
                {
                    currentState = state;
                    currentState.EnterStateInit();
                    break;
                }
            }
        }
    }
}
