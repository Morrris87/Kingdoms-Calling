using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Complete;

public class ChargeState : FSMState
{
    AI enemyAI;

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
        float speed = enemyAI.skeletonStats.speed * Time.deltaTime;
        // Move to player 
        enemyAI.transform.LookAt(enemyAI.objPlayer.transform);
        enemyAI.transform.position += enemyAI.transform.forward * speed;
    }

    public override void Reason()
    {
        Transform skeleton = enemyAI.gameObject.transform;
        Transform player = enemyAI.objPlayer.transform;
        if (enemyAI.skeletonStats.health <= 0)
        {
            enemyAI.PerformTransition(Transition.NoHealth);
            return;
        }
        if (Vector3.Distance(enemyAI.transform.position, enemyAI.objPlayer.transform.position) <= enemyAI.attackRange)
        {
            enemyAI.PerformTransition(Transition.ReachPlayer);
            Debug.Log("attacking");
            return;
        }
    }
}
