using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompleteClone;

public class CloneDeathState : CloneFightOneFSMState
{
    BossFightOneAI enemyAI;

    float elapsedTime;
    float intervalTime;
    public CloneDeathState(BossFightOneAI Lich)
    {
        enemyAI = Lich;
        curSpeed = 0;
        stateID = FSMStateID.Dead;
        //enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {
        //die
    }

    public override void Reason()
    {
       
    }
}
