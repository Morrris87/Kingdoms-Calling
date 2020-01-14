using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;
using System;

public class AttackState : FSMState
{
    AI enemyAI;

    float elapsedTime;
    float intervalTime;
    public AttackState(AI skeleton)
    {
        enemyAI = skeleton;
        curSpeed = 0;
        stateID = FSMStateID.Attacking;
        enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {
        // ADD TIMER
        // depending on wepon :D

        enemyAI.playerHealth.Damage(enemyAI.skeletonStats.power);
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
        if (Vector3.Distance(enemyAI.transform.position, enemyAI.objPlayer.transform.position) > enemyAI.attackRange)
        {
            enemyAI.PerformTransition(Transition.NotInAttackRange);
            Debug.Log("chasing");
            return;
        }
    }
}
