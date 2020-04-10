using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
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
        
        Debug.Log("Spawning");
        //ISSUE HERE
        if (enemyAI.colour == BossFightOneAI.skeletonColour.white)
        {
            enemyAI.spawnSkeletonsForBoss("white");//call from spawn
        }
        else if(enemyAI.colour == BossFightOneAI.skeletonColour.grey)
        {
            enemyAI.spawnSkeletonsForBoss("grey");//call from spawn
        }
        else if (enemyAI.colour == BossFightOneAI.skeletonColour.purple)
        {
            enemyAI.spawnSkeletonsForBoss("purple");//call from spawn
        }
        Debug.Log("Spawning Over");
        //just fucking do the spawning here
        enemyAI.allSkeletonsSpawned = true;
        enemyAI.SpawnSkeletonsTimer = enemyAI.skeltonSpawnCooldown;
    }

    public override void Reason()
    {
        if (enemyAI.bossStats.health <= 0)
        {
            enemyAI.PerformTransition(Transition.NoHealth);
            return;
        }
        // THIS DOWN TO MAKE HIM PATROL NOT AUTO ATTACK
        if(enemyAI.allSkeletonsSpawned == true)// all skeletond dead
        {
            enemyAI.PerformTransition(Transition.ToPatrol);
            return;
        }
    }
}
