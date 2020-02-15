using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restoration : MonoBehaviour
{
    Health hp;
    float timer;
    [HideInInspector] public int regenTime;
    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<Health>();
        regenTime = 1;
        timer = regenTime;
    }

    // Update is called once per frame
    void Update()
    {
        // If timer hasn't completed...
        if (timer >= 0)
        {
            // Subtract timer by deltaTime
            timer -= Time.deltaTime;
        }
        // Otherwise timer has completed
        else
        {
            // If Paladin's health is above 0 and below maxHealth...
            if (hp.currentHealth > 0 && hp.currentHealth < hp.maxHealth)
            {
                hp.currentHealth++;
                timer = regenTime;
            }
        }
    }
}
