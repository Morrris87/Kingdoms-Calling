using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompleteBossOne;

public class BossAutoAttack : BossFightOneFSMState
{
    BossFightOneAI enemyAI;

    float elapsedTime;
    float intervalTime;


    GameObject playerOne;
    GameObject playerTwo;
    GameObject playerThree;
    GameObject playerFour;

    int attackRange = 10;

    float rangeFromPlayerOne;
    float rangeFromPlayerTwo;
    float rangeFromPlayerThree;
    float rangeFromPlayerFour;

    GameObject currentClosestPlayer;

    float[] Players;
    public BossAutoAttack(BossFightOneAI Lich)
    {
        enemyAI = Lich;
        curSpeed = 0;
        stateID = FSMStateID.AutoAttack;
    }

    public override void Act()
    {
        //damage the closest player
        
    }

    public override void Reason()
    {
        //player one distance
        if (IsInCurrentRange(enemyAI.gameObject.transform, playerOne.transform.position, attackRange))
        {
            rangeFromPlayerOne = Vector3.Distance(enemyAI.gameObject.transform.position, playerOne.transform.position);
        }
        //player two distance
        if (IsInCurrentRange(enemyAI.gameObject.transform, playerTwo.transform.position, attackRange))
        {
            rangeFromPlayerTwo = Vector3.Distance(enemyAI.gameObject.transform.position, playerTwo.transform.position);
        }
        //player three distance
        if (IsInCurrentRange(enemyAI.gameObject.transform, playerThree.transform.position, attackRange))
        {
            rangeFromPlayerThree = Vector3.Distance(enemyAI.gameObject.transform.position, playerThree.transform.position);
        }
        //player four distance 
        if (IsInCurrentRange(enemyAI.gameObject.transform, playerFour.transform.position, attackRange))
        {
            rangeFromPlayerFour = Vector3.Distance(enemyAI.gameObject.transform.position, playerFour.transform.position);
        }

        //find the closest player to the lich
        AssignPlayersToArray();
        // get the closest player here you dumb fuck you need sleep wake up fuck heads
        


    }
    public void AssignPlayersToArray()
    {
        Players[1] = rangeFromPlayerOne;
        Players[2] = rangeFromPlayerTwo;
        Players[3] = rangeFromPlayerThree;
        Players[4] = rangeFromPlayerFour;
    }
}
