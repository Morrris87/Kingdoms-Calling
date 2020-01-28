using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompleteBossOne;

public class BossScreechState : BossFightOneFSMState
{
    BossFightOneAI enemyAI;

    float randomTimer;
    public BossScreechState(BossFightOneAI Lich)
    {
        enemyAI = Lich;
        curSpeed = 0;
        stateID = FSMStateID.Sceach;
        randomTimer = enemyAI.randomizPlayersInputsTimer;
        //enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {
        enemyAI.bossScreechHitBox.gameObject.SetActive(true);

        //check if collided objects are tagged player
        //Dammage collided players
        if(enemyAI.randomizPlayersInputsTimer <= 0)
        {
            //randomize the players input for randomizPlayersInputsTimer amount of time
            enemyAI.randomizPlayersInputsTimer = randomTimer;
            // do randomize shit here
            RandomizePlayersInput();
        }
    }

    public override void Reason()
    {
        if (enemyAI.bossStats.health <= 0)
        {
            enemyAI.PerformTransition(Transition.NoHealth);
            return;
        }
    }
    public void RandomizePlayersInput()
    {

    }
}
