using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CompleteClone;

public class CloneSpawnSkeletonsState : CloneFightOneFSMState
{
    BossFightOneAI enemyAI;

    public GameObject BossSpawnOne;


    float elapsedTime;
    float intervalTime;



    int packSize;
    int whitePackNumber = 6;
    int greyPackNUmber = 3;

    public GameObject skeletonWhite;
    public GameObject skeletonGrey;

    GameObject spawnZone;

    //have these set somewhere else 
    public bool fightOne = false;
    public bool fightTwo = false;

    string colour;
    public CloneSpawnSkeletonsState(BossFightOneAI Lich)
    {
        enemyAI = Lich;
        curSpeed = 0;
        stateID = FSMStateID.SpawnSkeletons;
        //enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {
        enemyAI.spawnScript.spawnSkeletonsForBoss(colour);//call from spawn
    }

    public override void Reason()
    {
        if (enemyAI.bossStats.health <= 0)
        {
            //enemyAI.PerformTransition(Transition.NoHealth);
            return;
        }
        //if fight one spawn white
        if (fightOne == true && fightTwo == false)
        {
            colour = "white";
        }
        // if fight 2 spawn grey
        else if(fightOne == false && fightTwo == true)
        {
            colour = "grey";
        }
    }
}
