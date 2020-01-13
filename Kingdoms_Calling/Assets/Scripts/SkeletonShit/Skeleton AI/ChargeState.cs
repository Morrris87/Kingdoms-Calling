using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;

public class ChargeState : FSMState
{
    AI enemyAI;

    float elapsedTime;
    float intervalTime;  
    public ChargeState(AI skeleton)
    {
        enemyAI = skeleton;
        curSpeed = 0;
        stateID = FSMStateID.Chasing;
        enemyAI.navAgent.speed = curSpeed;
        
    }

    public override void Act()
    {
        float speed = enemyAI.skeletonStats.speed * Time.deltaTime;
        // Move to player 
        //enemyAI.transform.position += 
        enemyAI.transform.LookAt(enemyAI.Player.transform);
        enemyAI.transform.Rotate(new Vector3 (0,-90,0),Space.Self);
        enemyAI.transform.Translate(new Vector3(speed, 0, 0));
            //Vector3.MoveTowards(enemyAI.transform.position, -enemyAI.Player.transform.position, speed); // THIS FUCKING SHIT IS BROKE!

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
            enemyAI.PerformTransition(Transition.ReachPlayer);
            Debug.Log("attacking");
            return;
        }
    }
}
