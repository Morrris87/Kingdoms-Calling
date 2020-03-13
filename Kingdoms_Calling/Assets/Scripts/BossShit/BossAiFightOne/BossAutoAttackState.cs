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

    Transform currentClosestPlayer;

    Transform[] players = new Transform[4];
    public BossAutoAttackState(BossFightOneAI Lich)
    {
        enemyAI = Lich;
        curSpeed = 0;
        stateID = FSMStateID.AutoAttack;

        // Assign players to position array
        players[0] = enemyAI.archer.transform;
        players[1] = enemyAI.assassin.transform;
        players[2] = enemyAI.paladin.transform;
        players[3] = enemyAI.warrior.transform;
    }

    public override void Act()
    {
        // damage the closest player
        enemyAI.transform.LookAt(currentClosestPlayer);

        //attack only on the time for the timer

        if (enemyAI.bossTimer <= 0)
        {
            enemyAI.animator.SetTrigger("Attacked");
            enemyAI.bossTimer = enemyAI.bossAutoAttackCooldown;
        }
        else
        {
            enemyAI.bossTimer -= Time.deltaTime;
        }
    }

    public override void Reason()
    {
        // Transition to Dead State
        if (enemyAI.bossStats.health <= 0)
        {
            enemyAI.PerformTransition(Transition.NoHealth);
            return;
        }

        // Gets the closest player
        currentClosestPlayer = GetClosestPlayer(players);
    }

    public Transform GetClosestPlayer(Transform[] players)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = enemyAI.transform.position;

        foreach(Transform t in players)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }

        return tMin;
    }
}
