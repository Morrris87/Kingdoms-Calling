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
    //int randOne;
    //int randTwo;
    //int randThree;
    //int randFour;
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
        
        //enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {
        int randOne = Random.Range(0, enemyAI.PatrolList.Count);
        //int randTwo = Random.Range(0, enemyAI.PatrolList.Count);
        //int randThree = Random.Range(0, enemyAI.PatrolList.Count);
        //int randFour = Random.Range(0, enemyAI.PatrolList.Count);
        Debug.Log("Patrolling");
        // Play move animation   
        //move
        speed = enemyAI.bossStats.speed * Time.deltaTime;
        if (donePatrol == false)
        {
            //for (int timesMoved = 0; timesMoved < 4; timesMoved++)// this breaks and wont move at 5 but moves and wont leave at 4
            // {
            //if (timesMoved == 0)
            //{
            //move to randOne
            while (enemyAI.transform.position != enemyAI.PatrolList[randOne].transform.position)
            {
                
                enemyAI.transform.LookAt(enemyAI.PatrolList[randOne].transform);
                enemyAI.transform.position += enemyAI.transform.forward * speed;
            }
            donePatrol = true;

                //}
                //else if (timesMoved == 1)
                //{
                //    //move to randTwo
                //    enemyAI.transform.LookAt(enemyAI.PatrolList[randTwo].transform);
                //    enemyAI.transform.position += enemyAI.transform.forward * speed;
                //}
                //else if (timesMoved == 2)
                //{
                //    //move to randThree
                //    enemyAI.transform.LookAt(enemyAI.PatrolList[randThree].transform);
                //    enemyAI.transform.position += enemyAI.transform.forward * speed;
                //}
                //else if (timesMoved == 3)
                //{
                //    //move to randFour
                //    enemyAI.transform.LookAt(enemyAI.PatrolList[randFour].transform);
                //    enemyAI.transform.position += enemyAI.transform.forward * speed;
                //} 
            //}
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
