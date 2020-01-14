using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkeletonColour
{
    White,
    Grey,
    Purple
}
public enum Proc
{
    None,
    Fire,
    Wind,
    Earth,
    Lightning
}
public class SkeletonStats : MonoBehaviour
{
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
            health = 3;
            power = 2;
            speed = 4;
            stamina = 5;
            physicaDefence = 5;
            magicDefence = 4;
        }
        else if (this.tag == "Grey")
        {
            //BLOCK NUMBERS FOR GREY BOI
            health = 4;
            power = 3;
            speed = 4;
            stamina = 5;
            physicaDefence = 5;
            magicDefence = 5;
        }
        else if (this.tag == "Purple")
        {
            //BLOCK NUMBERS FOR PURPLE BOI
            health = 7;
            power = 5;
            speed = 5;
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
