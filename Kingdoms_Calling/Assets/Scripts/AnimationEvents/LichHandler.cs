using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichHandler : MonoBehaviour
{
    public BossFightOneAI bossFight;

    // Start is called before the first frame update
    void Start()
    {
        bossFight = GetComponentInParent<BossFightOneAI>();
    }

    public void MeleeAttackEvent()
    {
        bossFight.playerHealth.Damage(bossFight.bossStats.power);
    }

    public void RangedAttackEvent()
    {
        bossFight.playerHealth.Damage(bossFight.bossStats.power);
    }

    public void MultiplyEvent()
    {
        bossFight.spawnScript.SpawnClones(bossFight.bossPrefab, bossFight.CloneOneSpawn, bossFight.CloneTwoSpawn, bossFight.CloneThreeSpawn, bossFight.CloneFourSpawn);
    }

    public void SpawnSkeletonsEvent(string colour)
    {
        bossFight.spawnScript.spawnSkeletonsForBoss(colour);//call from spawn
    }

    public void ScreechStartEvent()
    {
        bossFight.bossScreechHitBox.gameObject.SetActive(true);
    }

    public void ScreechEndEvent()
    {
        bossFight.bossScreechHitBox.gameObject.SetActive(false);
        bossFight.screechTimer = 2;
    }
}