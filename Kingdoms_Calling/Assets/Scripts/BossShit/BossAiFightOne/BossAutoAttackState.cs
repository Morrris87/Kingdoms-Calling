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

    float rangeFromPlayerOne;
    float rangeFromPlayerTwo;
    float rangeFromPlayerThree;
    float rangeFromPlayerFour;

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



            //if (currentClosestPlayer == enemyAI.playerOne)
            //{
            //    //Attack Player? not the boss!!!!!
            //    // how to make it hit player one?
            //    //enemyAI.playerOne;
            //    // more shit have to figure that out
            //    enemyAI.playerHealth.Damage(enemyAI.bossStats.power);
            //}
            //else if (currentClosestPlayer == enemyAI.playerTwo)
            //{
            //    enemyAI.playerHealth.Damage(enemyAI.bossStats.power);
            //}
            //else if (currentClosestPlayer == enemyAI.playerThree)
            //{
            //    enemyAI.playerHealth.Damage(enemyAI.bossStats.power);
            //}
            //else if (currentClosestPlayer == enemyAI.playerFour)
            //{
            //    enemyAI.playerHealth.Damage(enemyAI.bossStats.power);
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
        //player one distance
        if (IsInCurrentRange(enemyAI.gameObject.transform, enemyAI.playerOne.transform.position, attackRange))
        {
            rangeFromPlayerOne = Vector3.Distance(enemyAI.gameObject.transform.position, enemyAI.playerOne.transform.position);
        }
        //player two distance
        if (IsInCurrentRange(enemyAI.gameObject.transform, enemyAI.playerTwo.transform.position, attackRange))
        {
            rangeFromPlayerTwo = Vector3.Distance(enemyAI.gameObject.transform.position, enemyAI.playerTwo.transform.position);
        }
        //player three distance
        if (IsInCurrentRange(enemyAI.gameObject.transform, enemyAI.playerThree.transform.position, attackRange))
        {
            rangeFromPlayerThree = Vector3.Distance(enemyAI.gameObject.transform.position, enemyAI.playerThree.transform.position);
        }
        //player four distance 
        if (IsInCurrentRange(enemyAI.gameObject.transform, enemyAI.playerFour.transform.position, attackRange))
        {
            rangeFromPlayerFour = Vector3.Distance(enemyAI.gameObject.transform.position, enemyAI.playerFour.transform.position);
        }

        //find the closest player to the lich
        AssignPlayersToArray();


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
        // get the closest player here you dumb fuck you need sleep wake up fuck heads
        // i need to vegitate at home and eat food



    }
    public void AssignPlayersToArray()
    {
        Players[0] = rangeFromPlayerOne;
        Players[1] = rangeFromPlayerTwo;
        Players[2] = rangeFromPlayerThree;
        Players[3] = rangeFromPlayerFour;
    }
}
