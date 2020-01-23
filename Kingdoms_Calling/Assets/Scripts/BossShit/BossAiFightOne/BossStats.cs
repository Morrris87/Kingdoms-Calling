using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    public enum Proc
    {
        None,
        Fire,
        Wind,
        Earth,
        Lightning
    }
    public Proc proc;

    public int health;
    public int power;
    public int speed;
    public int stamina;
    public int physicaDefence;
    public int magicDefence;  
    

    void Start()
    {
        health = 10;
        power = 7;
        speed = 6;
        stamina = 10;
        physicaDefence = 8;
        magicDefence = 6;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
