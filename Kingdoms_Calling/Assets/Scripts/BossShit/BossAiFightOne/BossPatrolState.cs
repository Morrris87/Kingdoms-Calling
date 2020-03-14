using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompleteBossOne;

public class BossPatrolState : BossFightOneFSMState
{
    BossFightOneAI enemyAI;

    bool donePatrol = false;

    float elapsedTime;
    float intervalTime;
    public BossPatrolState(BossFightOneAI Lich)
    {
        enemyAI = Lich;
        curSpeed = 0;
        stateID = FSMStateID.Patrol;
        //enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {
        Debug.Log("Patrolling");
        // Play move animation   

        //make lich patrol once he is finished patrol
        donePatrol = true;
    }

    public override void Reason()
    {
        if (enemyAI.bossStats.health <= 0)
        {
            enemyAI.PerformTransition(Transition.NoHealth);
            return;
        }
        if(donePatrol == true)
        {
            enemyAI.PerformTransition(Transition.ToAutoAttack);
            return;
        }
    }
}
