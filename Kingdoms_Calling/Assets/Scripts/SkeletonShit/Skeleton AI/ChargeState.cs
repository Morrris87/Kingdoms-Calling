//  Name: ChargeState.cs
//  Author: ZAC KINDY
//  Function:
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;
using Complete;

public class ChargeState : FSMState
{
    AI enemyAI;

    public float speed;

    float pathTime = 0f;
    int slot = -1;
    bool inSlot = false;

    float elapsedTime;
    float intervalTime;  
    public ChargeState(AI skeleton)
    {
        enemyAI = skeleton;
        curSpeed = 0;
        stateID = FSMStateID.Chasing;
        enemyAI.navAgent.speed = curSpeed;
        
    }

    public override void Act()
    {
        enemyAI.animator.SetBool("isMoving", true);
        speed = enemyAI.skeletonStats.speed * Time.deltaTime;
        // Move to player 
        if (enemyAI.thisSkeletonClass == "Sword" || enemyAI.thisSkeletonClass == "Mace")
        {
            enemyAI.transform.LookAt(enemyAI.objPlayer.transform);
            enemyAI.transform.position += enemyAI.transform.forward * speed;
        }
        else if(enemyAI.thisSkeletonClass == "Bow" || enemyAI.thisSkeletonClass == "Mage")
        {
            //Make it run to the slot manager on the player
            //(still have to make it)
            //slot machine stff
            pathTime += Time.deltaTime;
            if (pathTime > 0.5f)
            {
                pathTime = 0f;
                var slotManager = enemyAI.objPlayer.GetComponent<SlotManager>();
                if (slotManager != null)
                {
                    if (slot == -1)
                        slot = slotManager.Reserve(enemyAI.gameObject);
                    if (slot == -1)
                        return;
                    var agent = enemyAI.GetComponent<NavMeshAgent>();
                    if (agent == null)
                        return;
                    agent.destination = slotManager.GetSlotPosition(slot);
                    inSlot = true;
                }
            }
        }
    }

    public override void Reason()
    {
        Transform skeleton = enemyAI.gameObject.transform;
        Transform player = enemyAI.objPlayer.transform;
        if (enemyAI.GetComponent<Health>().currentHealth <= 0)
        {
            enemyAI.PerformTransition(Transition.NoHealth);
            return;
        }
        if (Vector3.Distance(enemyAI.transform.position, enemyAI.objPlayer.transform.position) <= enemyAI.attackRange || inSlot == true)
        {
            enemyAI.PerformTransition(Transition.ReachPlayer);
            //Debug.Log("attacking");
            return;
        }
    }
}
