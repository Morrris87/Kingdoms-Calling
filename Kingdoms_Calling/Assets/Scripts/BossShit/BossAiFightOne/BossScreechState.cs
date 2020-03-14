using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompleteBossOne;

public class BossScreechState : BossFightOneFSMState
{
    BossFightOneAI enemyAI;

    float randomTimer;

    float randomLength;
    public BossScreechState(BossFightOneAI Lich)
    {
        enemyAI = Lich;
        curSpeed = 0;
        stateID = FSMStateID.Sceach;
        randomTimer = enemyAI.randomizePlayersInputsTimer;
        //enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {
        //timer for the attack length (maybe just have it lenght of the animation)
        if (enemyAI.screechTimer >= 2)
        {
            enemyAI.bossScreechHitBox.gameObject.SetActive(true);
        }
        else
        {
            enemyAI.bossScreechHitBox.gameObject.SetActive(false);
            enemyAI.screechTimer = 2;
        }
    }

    public override void Reason()
    {
        if (enemyAI.bossStats.health <= 0)
        {
            enemyAI.PerformTransition(Transition.NoHealth);
            return;
        }
        if(randomLength <= 0)
        {
            enemyAI.PerformTransition(Transition.ScreechOnCooldown);
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

            if (enemyAI.randomizePlayersInputsTimer <= 0)
            {
                //randomize the players input for randomizPlayersInputsTimer amount of time
                enemyAI.randomizePlayersInputsTimer = randomTimer;
                // do randomize shit here
                RandomizePlayersInput();
            }
        }
    }
}
