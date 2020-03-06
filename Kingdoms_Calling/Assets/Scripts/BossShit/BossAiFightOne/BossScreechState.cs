﻿using System.Collections;
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
        enemyAI.animator.SetTrigger("Screeched");
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
        int numberOfInputs = 4;
        // get all inputs here

        for (int i = 0; i < numberOfInputs; i++)
        {
            int rand = Random.Range(0, 4);

            if (rand == 0)
            {
                //fucking randomize
            }
            else if (rand == 1)
            {

            }
            else if (rand == 2)
            {

            }
            else if (rand == 3)
            {

            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        //check if collided objects are tagged player
        if (collision.gameObject.tag == "Player")
        {
            //Dammage collided players
            enemyAI.playerHealth.Damage(enemyAI.bossStats.power);

            if (enemyAI.randomizPlayersInputsTimer <= 0)
            {
                //randomize the players input for randomizPlayersInputsTimer amount of time
                enemyAI.randomizPlayersInputsTimer = randomTimer;
                // do randomize shit here
                RandomizePlayersInput();
            }
        }
    }
}
