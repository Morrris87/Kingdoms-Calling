using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using CompleteBossOne;

public class BossAutoAttackState : BossFightOneFSMState
{
    BossFightOneAI enemyAI;

    float elapsedTime;
    float intervalTime;

    int attackRange = 10;

    int timerMultiply;
    int timerSpawnSkelys;

    float rangeFromPlayerOne;
    float rangeFromPlayerTwo;
    float rangeFromPlayerThree;
    float rangeFromPlayerFour;

    int multiplyCooldown = 15;
    int spawnCooldown = 12;

    GameObject currentClosestPlayer;

    float[] Players;

    public BossAutoAttackState(BossFightOneAI Lich)
    {
        enemyAI = Lich;
        curSpeed = 0;
        stateID = FSMStateID.AutoAttack;
        Players = new float[4];
    }

    public override void Act()
    {
        //damage the closest player
        // make timer

        //attack only on the time for the timer

        if (enemyAI.bossTimer <= 0)
        {
            enemyAI.bossTimer = enemyAI.bossAutoAttackCooldown;


            if (currentClosestPlayer == enemyAI.playerOne)
            {
                enemyAI.playerHealth.Damage(enemyAI.bossStats.power);
                //animation
            }
            else if (currentClosestPlayer == enemyAI.playerTwo)
            {
                enemyAI.playerHealth.Damage(enemyAI.bossStats.power);
            }
            else if (currentClosestPlayer == enemyAI.playerThree)
            {
                enemyAI.playerHealth.Damage(enemyAI.bossStats.power);
            }
            else if (currentClosestPlayer == enemyAI.playerFour)
            {
                enemyAI.playerHealth.Damage(enemyAI.bossStats.power);
            }
        }


    }

    public override void Reason()
    {

        //update timers conners way so i dont fuking break it :D





        if (enemyAI.bossStats.health <= 0)
        {
            enemyAI.PerformTransition(Transition.NoHealth);
            return;
        }
        else
        {
            for (int i = 0; i < enemyAI.playerArray.Length; i++)
            {
                if (i == 0)
                {
                    //player one distance
                    if (IsInCurrentRange(enemyAI.gameObject.transform, enemyAI.playerOne.transform.position, attackRange))
                    {
                        rangeFromPlayerOne = Vector3.Distance(enemyAI.gameObject.transform.position, enemyAI.playerOne.transform.position);
                        Players[i] = rangeFromPlayerOne;
                    }
                }
                else if (i == 1)
                {
                    //player two distance
                    if (IsInCurrentRange(enemyAI.gameObject.transform, enemyAI.playerTwo.transform.position, attackRange))
                    {
                        rangeFromPlayerTwo = Vector3.Distance(enemyAI.gameObject.transform.position, enemyAI.playerTwo.transform.position);
                        Players[i] = rangeFromPlayerTwo;
                    }
                }
                else if (i == 3)
                {
                    //player three distance
                    if (IsInCurrentRange(enemyAI.gameObject.transform, enemyAI.playerThree.transform.position, attackRange))
                    {
                        rangeFromPlayerThree = Vector3.Distance(enemyAI.gameObject.transform.position, enemyAI.playerThree.transform.position);
                        Players[i] = rangeFromPlayerThree;
                    }
                }
                else if (i == 3)
                {
                    //player four distance 
                    if (IsInCurrentRange(enemyAI.gameObject.transform, enemyAI.playerFour.transform.position, attackRange))
                    {
                        rangeFromPlayerFour = Vector3.Distance(enemyAI.gameObject.transform.position, enemyAI.playerFour.transform.position);
                        Players[i] = rangeFromPlayerFour;
                    }
                }
            }
        }

        //find the closest player to the lich
        
        if(Players.Min() == rangeFromPlayerOne)
        {
            currentClosestPlayer = enemyAI.playerOne;
        }
        if (Players.Min() == rangeFromPlayerTwo)
        {
            currentClosestPlayer = enemyAI.playerTwo;
        }
        if (Players.Min() == rangeFromPlayerThree)
        {
            currentClosestPlayer = enemyAI.playerThree;
        }
        if (Players.Min() == rangeFromPlayerFour)
        {
            currentClosestPlayer = enemyAI.playerFour;
        }


        if(timerMultiply == 0)
        {
            enemyAI.PerformTransition(Transition.CastMultiply);
            timerMultiply = multiplyCooldown;
            return;
        }
        if(timerSpawnSkelys == 0)
        {
            enemyAI.PerformTransition(Transition.CastSpawnSkeletons);
            timerSpawnSkelys = spawnCooldown;
            return;
        }


    }
    //public void AssignPlayersToArray()
    //{
    //    for (int i = 0; i < enemyAI.playerArray.Length; i++)
    //    {
    //        if (i == 0)
    //        {
    //            Players[1] = rangeFromPlayerOne;
    //        }
    //        else if (i == 1)
    //        {
    //            Players[2] = rangeFromPlayerTwo;
    //        }
    //        else if (i == 2)
    //        {
    //            Players[3] = rangeFromPlayerThree;
    //        }
    //        else if (i == 3)
    //        {
    //            Players[4] = rangeFromPlayerFour;
    //        }
    //    }
    //}
}
