using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class ChargeState : FSMState
{
    public ChargeState(AI skeleton)
    {
        AI enemyAI = skeleton;
        curSpeed = 0;
        stateID = FSMStateID.Chasing;
        enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {
    }

    public override void Reason()
    {
    }
}
