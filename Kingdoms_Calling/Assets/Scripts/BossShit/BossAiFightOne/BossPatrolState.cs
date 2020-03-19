using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompleteBossOne;

public class BossPatrolState : BossFightOneFSMState
{
    BossFightOneAI enemyAI;

    bool donePatrol = false;

    public float speed;

    float elapsedTime;
    float intervalTime;
    int randOne;
    int randTwo;
    int randThree;
    int randFour;
    int timesMoved = 0;
    GameObject one;
    GameObject Two;
    GameObject Three;
    GameObject Four;
    public BossPatrolState(BossFightOneAI Lich)
    {
        enemyAI = Lich;
        curSpeed = 0;
        stateID = FSMStateID.Patrol;
        //Get 4 RandomPositions in the patrolList
        randOne = Random.Range(0,enemyAI.PatrolList.Count);
        randTwo = Random.Range(0, enemyAI.PatrolList.Count);
        randThree = Random.Range(0, enemyAI.PatrolList.Count);
        randFour = Random.Range(0, enemyAI.PatrolList.Count);
        //enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {
        Debug.Log("Patrolling");
        // Play move animation   
        //move
        speed = enemyAI.bossStats.speed * Time.deltaTime;
        if (donePatrol == false)
        {
            if(timesMoved == 0)
            {
                //move to randOne
                enemyAI.transform.LookAt(enemyAI.PatrolList[randOne].transform);
                enemyAI.transform.position += enemyAI.transform.forward * speed;
                timesMoved++;
            }
            else if (timesMoved == 1)
            {
                //move to randTwo
                enemyAI.transform.LookAt(enemyAI.PatrolList[randTwo].transform);
                enemyAI.transform.position += enemyAI.transform.forward * speed;
                timesMoved++;
            }
            else if (timesMoved == 2)
            {
                //move to randThree
                enemyAI.transform.LookAt(enemyAI.PatrolList[randThree].transform);
                enemyAI.transform.position += enemyAI.transform.forward * speed;
                timesMoved++;
            }
            else if (timesMoved == 3)
            {
                //move to randFour
                enemyAI.transform.LookAt(enemyAI.PatrolList[randFour].transform);
                enemyAI.transform.position += enemyAI.transform.forward * speed;
                timesMoved++;
            }
            else if (timesMoved == 4)
            {
                timesMoved = 0;
                donePatrol = true;
            }
        }
        
        
    }

    public override void Reason()
    {
        if (enemyAI.bossStats.health <= 0)
        {
            enemyAI.PerformTransition(Transition.NoHealth);
            return;
        }
        //make lich auto once he is finished patrol
        if (donePatrol == true)
        {
            enemyAI.PerformTransition(Transition.ToAutoAttack);
            return;
        }
        

    }
}
