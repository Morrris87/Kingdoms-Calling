﻿using System.Collections;
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
    public bool fightOne = true;
    public bool fightTwo = false;

    string colour;
    public BossSpawnSkeletonsState(BossFightOneAI Lich)
    {
        enemyAI = Lich;
        curSpeed = 0;
        stateID = FSMStateID.SpawnSkeletons;
        //enemyAI.navAgent.speed = curSpeed;
    }

    public override void Act()
    {
        //ISSUE HERE
        //enemyAI.spawnScript.spawnSkeletonsForBoss(colour);//call from spawn
    }

    public override void Reason()
    {
        if (enemyAI.bossStats.health <= 0)
        {
            enemyAI.PerformTransition(Transition.NoHealth);
            return;
        }
        if(enemyAI.allSkeletonsDead == true)// all skeletond dead
        {
            enemyAI.PerformTransition(Transition.AllSkeletonsDead);
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
