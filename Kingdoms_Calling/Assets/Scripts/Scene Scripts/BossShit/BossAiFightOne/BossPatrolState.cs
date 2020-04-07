using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompleteBossOne;

public class BossPatrolState : BossFightOneFSMState
{
    BossFightOneAI enemyAI;

    bool donePatrol;

    public float speed;
    bool picked;
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
    int randOne = -1;
    float timerLength = 7f;
    float timer;
    public BossPatrolState(BossFightOneAI Lich)
    {
        enemyAI = Lich;
        timer = timerLength;
        curSpeed = 0;
        stateID = FSMStateID.Patrol;
        //Get 4 RandomPositions in the patrolList
        
        //enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {
        donePatrol = false;
        Debug.Log("Patrolling");
        // Play move animation   
        //move
        speed = enemyAI.bossStats.speed * Time.deltaTime;
        if (donePatrol == false)
        {
            if (randOne == -1)
            {
                randOne = Random.Range(0, enemyAI.PatrolList.Count);
                Debug.Log("Picked " + randOne.ToString());
            }
            //move to randOne
            //if (timer > 0f)
           // {
                
                timer -= Time.deltaTime;
                if(Vector3.Distance(enemyAI.PatrolList[randOne].transform.position, 
                    enemyAI.transform.position) > 1)
                {
                    enemyAI.transform.LookAt(enemyAI.PatrolList[randOne].transform);
                    enemyAI.transform.position += enemyAI.transform.forward * speed;
                }
            else
            {
                timer = timerLength;
                picked = false;
                randOne = -1;
                donePatrol = true;
            }
                //if (enemyAI.transform.position.x != enemyAI.PatrolList[randOne].transform.position.x 
                //    && enemyAI.transform.position.z != enemyAI.PatrolList[randOne].transform.position.z)
                //{
                    
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