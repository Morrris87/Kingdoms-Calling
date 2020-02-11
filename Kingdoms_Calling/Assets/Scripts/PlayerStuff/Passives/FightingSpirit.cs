﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingSpirit : MonoBehaviour
{
    FlamingLeap leap;
    AxeWhirlwind axe;
    CharacterManager manager;
    BasicAttack attack;
    public int AdditionalSpeed;
    public int AdditionalDamage;
    float baseSpeed;
    float baseAttack;
    // Start is called before the first frame update
    void Start()
    {
        AdditionalSpeed = 200;
        AdditionalDamage = 3;
        manager = GetComponent<CharacterManager>();
        leap = GetComponent<FlamingLeap>();
        axe = GetComponent<AxeWhirlwind>();
        attack = GetComponent<BasicAttack>();
        baseSpeed = manager.speed;
        baseAttack = attack.AttackDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if(leap.isUsable == false && axe.isUsable == false)
        {
            manager.speed += AdditionalSpeed;
            attack.AttackDamage += AdditionalDamage;
        }
        else
        {
            manager.speed = baseSpeed;
            attack.AttackDamage = baseAttack;
        }
    }
}
