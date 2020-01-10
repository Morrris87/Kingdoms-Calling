using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class AttackState : FSMState
{
    public AttackState(AI skeleton)
    {
        AI enemyAI = skeleton;
        curSpeed = 0;
        stateID = FSMStateID.Attacking;
        enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {
    }

    public override void Reason()
    {
    }
}
