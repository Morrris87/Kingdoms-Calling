using System.Collections;
using System.Collections.Generic;
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
        if (IsInCurrentRange(skeleton, player.position, enemyAI.chaseRange))
        {
            enemyAI.PerformTransition(Transition.SawPlayer);
            Debug.Log("Chasing");
            return;
        }
    }
}
