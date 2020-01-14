using System.Collections;
using System.Collections.Generic;
using System;
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
        enemyAI.transform.LookAt(enemyAI.objPlayer.transform);
        enemyAI.transform.position += enemyAI.transform.forward * speed;

        //enemyAI.transform.Rotate(new Vector3 (0,-90,0),Space.Self);
        //enemyAI.transform.Translate(new Vector3(speed, 0, 0));
        //Vector3.MoveTowards(enemyAI.transform.position, enemyAI.Player.transform.position, enemyAI.skeletonStats.speed); // THIS FUCKING SHIT IS BROKE!

    }

    public override void Reason()
    {
        Transform skeleton = enemyAI.gameObject.transform;
        Transform player = enemyAI.objPlayer.transform;
        if (enemyAI.skeletonStats.health <= 0)
        {
            enemyAI.PerformTransition(Transition.NoHealth);
            return;
        }
        if (Vector3.Distance(enemyAI.transform.position, enemyAI.objPlayer.transform.position) <= enemyAI.attackRange)
        {
            enemyAI.PerformTransition(Transition.ReachPlayer);
            Debug.Log("attacking");
            return;
        }
    }
    double distance(float x1, float y1, float x2, float y2)
    {
        return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2) * 1.0);
    }
}
