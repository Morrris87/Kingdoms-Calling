using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompleteBossOne;

public class BossScreechState : BossFightOneFSMState
{
    BossFightOneAI enemyAI;

    float elapsedTime;
    float intervalTime;
    public BossScreechState(BossFightOneAI Lich)
    {
        enemyAI = Lich;
        curSpeed = 0;
        stateID = FSMStateID.Dead;
        //enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {

    }

    public override void Reason()
    {
        if (enemyAI.bossStats.health <= 0)
        {
            enemyAI.PerformTransition(Transition.NoHealth);
            return;
        }
    }
}
