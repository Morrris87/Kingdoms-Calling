//  Name: AttackState.cs
//  Author: ZAC KINDY
//  Function:
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
        intervalTime = 2f;
        elapsedTime = intervalTime;
        stateID = FSMStateID.Attacking;
        enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {
        // ADD TIMER
        if (elapsedTime > 0f)
        {
            elapsedTime -= Time.deltaTime;
        }
        else
        {
            if (enemyAI.GetComponent<SkeletonHandler>().magicShot != null)
            {
                enemyAI.animator.SetTrigger("MagicAttack");
                elapsedTime = intervalTime;
            }
            else
            {
                enemyAI.playerHealth.Damage(enemyAI.skeletonStats.power);
                enemyAI.animator.SetTrigger("Attacked");
                elapsedTime = intervalTime;
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
        if (Vector3.Distance(enemyAI.transform.position, enemyAI.objPlayer.transform.position) >= enemyAI.attackRange)
        {
            enemyAI.PerformTransition(Transition.NotInAttackRange);
            //Debug.Log("chasing");
            return;
        }
    }
}
