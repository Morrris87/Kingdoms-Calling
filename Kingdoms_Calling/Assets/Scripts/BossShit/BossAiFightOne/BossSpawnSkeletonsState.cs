using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CompleteBossOne;

public class BossSpawnSkeletonsState : BossFightOneFSMState
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
    public BossSpawnSkeletonsState(BossFightOneAI Lich)
    {
        enemyAI = Lich;
        curSpeed = 0;
        stateID = FSMStateID.Dead;
        //enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {

    }

    public override void Reason()
    {
        //if fight one spawn white
        if (fightOne == true && fightTwo == false)
        {
            colour = "white";
            enemyAI.spawnScript.spawnSkeletonsForBoss(colour);//call from spawn
        }
        // if fight 2 spawn grey
        else if(fightOne == false && fightTwo == true)
        {
            colour = "grey";
            enemyAI.spawnScript.spawnSkeletonsForBoss(colour);
        }
    }
}
