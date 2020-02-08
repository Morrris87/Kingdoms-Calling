using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CompleteBossOne;

public class SitOnThroneState : BossFightOneFSMState
{
    BossFightOneAI enemyAI;

    float elapsedTime;
    float intervalTime;
    bool isInvincible = false;

    int HealtyPrecent;

    
    int tempHealth = 1000000;

    public SitOnThroneState(BossFightOneAI Lich)
    {
        enemyAI = Lich;
        curSpeed = 0;
        stateID = FSMStateID.Sit;
        enemyAI.currentHealth = enemyAI.bossStats.health;
    }

    public override void Act()
    {
        
        if(isInvincible == true)
        {
            enemyAI.bossStats.health = tempHealth;
        }
        if(HealtyPrecent == 75)
        {
            enemyAI.spawnScript.SpawnSkeletonWazeForThroneFightSeventyFive();
        }
        else if(HealtyPrecent == 50)
        {
            enemyAI.spawnScript.SpawnSkeletonWazeForThroneFightFifty();
        }
        else if(HealtyPrecent == 25)
        {
            enemyAI.spawnScript.SpawnSkeletonWazeForThroneFightTwentyFive();
        }
    }

    public override void Reason()
    {
        enemyAI.TimerThroneHasBeenActivated++;
        if (enemyAI.bossStats.health <= 0)
        {
            enemyAI.PerformTransition(Transition.NoHealth);
            return;
        }
        if(isInvincible == false)
        {
            isInvincible = true;
        }

        if(enemyAI.TimerThroneHasBeenActivated == 1)
        {
            HealtyPrecent = 75;
        }
        else if(enemyAI.TimerThroneHasBeenActivated == 2)
        {
            HealtyPrecent = 50;
        }
        else if (enemyAI.TimerThroneHasBeenActivated == 3)
        {
            HealtyPrecent = 25;
        }
    }
}
