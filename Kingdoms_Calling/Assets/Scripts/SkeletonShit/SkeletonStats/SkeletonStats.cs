//  Name: SkeletonStats.cs
//  Author: ZAC KINDY
//  Function:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStats : MonoBehaviour
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
    string colour;


    // Start is called before the first frame update
    void Start()
    {
        if (this.tag == "White")
        {
            //BLOCK NUMBERS FOR WHITE BOI
            health = 60;
            power = 2;
            speed = 8;
            stamina = 5;
            physicaDefence = 5;
            magicDefence = 5;
        }
        else if (this.tag == "Grey")
        {
            //BLOCK NUMBERS FOR GREY BOI
            health = 65;
            power = 5;
            speed = 4;
            stamina = 5;
            physicaDefence = 5;
            magicDefence = 5;
        }
        else if (this.tag == "Purple")
        {
            //BLOCK NUMBERS FOR PURPLE BOI
            health = 85;
            power = 8;
            speed = 6;
            stamina = 7;
            physicaDefence = 7;
            magicDefence = 7;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
