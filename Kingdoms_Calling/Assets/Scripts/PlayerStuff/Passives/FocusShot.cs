using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusShot : MonoBehaviour
{
    [HideInInspector]
    public float Timer;
    Health thisForDamage;
    BasicAttack ArcherDamage;
    int doubleDamage;
    [HideInInspector]
    public bool passiveReady = false;

    public GameObject PassiveIndicator;

    // Start is called before the first frame update
    void Start()
    {
        Timer = 0;
        PassiveIndicator.SetActive(false);
        //doubleDamage = Convert.ToInt32(ArcherDamage.AttackDamage);
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer >= 12 && passiveReady == false)
        {
            passiveReady = true;
            PassiveIndicator.SetActive(true);
        }
        //Maybei dont need this and have to do all this(below) in the auto attack thing
        //if (passiveReady == true)// && you attacked gotta figure out how to that 
        //{

        //    thisForDamage.Damage(doubleDamage);
        //    passiveReady = false;
        //    Timer = 0;
        //}
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(90, 30, 30, 30), passiveReady.ToString());
    }
}
