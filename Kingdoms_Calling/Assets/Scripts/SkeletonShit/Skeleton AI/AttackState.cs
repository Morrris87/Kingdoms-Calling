using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

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
    }

    public override void Reason()
    {
        Transform skeleton = enemyAI.gameObject.transform;
        Transform player = enemyAI.Player.transform;
        if (enemyAI.skeletonStats.health <= 0)
        {
            enemyAI.PerformTransition(Transition.NoHealth);
            return;
        }
        if (IsInCurrentRange(skeleton, player.position, enemyAI.attackRange))
        {
            enemyAI.PerformTransition(Transition.NotInAttackRange);
            Debug.Log("chasing");
            return;
        }
    }
}
