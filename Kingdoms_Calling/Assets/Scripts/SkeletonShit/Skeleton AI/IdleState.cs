//  Name: IdleState.cs
//  Author: ZAC KINDY
//  Function:
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Complete;

public class IdleState : FSMState
{
    AI enemyAI;

    float elapsedTime;
    float intervalTime;

    public IdleState(AI skeleton)
    {
        enemyAI = skeleton;
        curSpeed = 0;
        stateID = FSMStateID.Idle;
        enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {
        // just wait!
        // so nothing here?
        // think so
        enemyAI.animator.SetBool("isMoving", false);
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
        if (Vector3.Distance(enemyAI.transform.position, enemyAI.objPlayer.transform.position) <= enemyAI.chaseRange)
        {
            enemyAI.PerformTransition(Transition.SawPlayer);
            //Debug.Log("Chasing");
            return;
        }
    }
}
