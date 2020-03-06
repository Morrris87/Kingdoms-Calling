using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompleteBossOne;

public class BossMultiplyState : BossFightOneFSMState
{
    // call spawnScript
    BossFightOneAI enemyAI;

    public BossMultiplyState(BossFightOneAI Lich)
    {
        enemyAI = Lich;
        curSpeed = 0;
        stateID = FSMStateID.Multiply;
        //enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {
        //Call the new function i made in the Spawn Script
        enemyAI.animator.SetTrigger("MultiplyEvent");
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
