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
    SpawnBoss bossCounter;

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
        bossCounter.currentNumber -= 1;
        GameObject.Destroy(enemyAI.thisSkeleton, 5);
        
    }

    public override void Reason()
    {
    }
}
