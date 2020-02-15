using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingSpirit : MonoBehaviour
{
    FlamingLeap leap;
    AxeWhirlwind axe;
    CharacterManager manager;
    BasicAttack attack;
    [HideInInspector] public int AdditionalSpeed;
    [HideInInspector] public int AdditionalDamage;
    float baseSpeed;
    float baseAttack;
    bool added;
    // Start is called before the first frame update
    void Start()
    {
        AdditionalSpeed = 400;
        AdditionalDamage = 3;
        manager = GetComponent<CharacterManager>();
        leap = GetComponent<FlamingLeap>();
        axe = GetComponent<AxeWhirlwind>();
        attack = GetComponent<BasicAttack>();
        baseSpeed = manager.speed;
        baseAttack = attack.AttackDamage;
        added = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(leap.isUsable == false && axe.isUsable == false)
        {
            if (added == false)
            {
                manager.speed += AdditionalSpeed;
                attack.AttackDamage += AdditionalDamage;
                added = true;
            }
        }
        else
        {
            manager.speed = baseSpeed;
            attack.AttackDamage = baseAttack;
            added = false;
        }
    }
}
