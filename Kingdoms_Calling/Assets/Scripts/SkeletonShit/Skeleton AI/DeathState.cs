//  Name: DeathStae.cs
//  Author: ZAC KINDY
//  Function:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class DeathState : FSMState
{
    AI enemyAI;
    BossFightOneAI bossCounter;

    float elapsedTime;
    float intervalTime;
    public DeathState(AI skeleton)
    {
        enemyAI = skeleton;
        curSpeed = 0;
        stateID = FSMStateID.Dead;
        enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {
        //die
        bossCounter.bossSkellyNumber -= 1;
        enemyAI.animator.SetTrigger("Dead");
       
        GameObject.Destroy(enemyAI.thisSkeleton, 5f);
    }

    public override void Reason()
    {
    }
}
