using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restoration : MonoBehaviour
{
    Health hp;
    float timer;
    public int regenTime;
    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<Health>();
        regenTime = 1;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp.currentHealth != 0)
        {
            timer += Time.deltaTime;
            // if like not full all 1 hp every 2 seconds
            if (hp.currentHealth < hp.maxHealth && timer >= regenTime)
            {
                hp.currentHealth++;
            }
        }
    }
}
