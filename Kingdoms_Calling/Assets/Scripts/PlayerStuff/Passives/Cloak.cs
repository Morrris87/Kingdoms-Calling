using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloak : MonoBehaviour
{
    Execution exicution;
    ThunderStrike strike;
    float timer;
    public int cloakTime;
    Color color;

    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<MeshRenderer>().material.color;
        cloakTime = 2;
        timer = 0;
        exicution = GetComponent<Execution>();
        strike = GetComponent<ThunderStrike>();
    }

    // Update is called once per frame
    void Update()
    {
        if(exicution.isUsable == false || strike.isUsable == false)//ability one OR ability two are on cooldown
        {
            timer += Time.deltaTime;
            if(timer >= cloakTime)// check timer 2 seconds
            {
                color.a = 0.5F;
                this.GetComponent<MeshRenderer>().material.color = color;
                this.tag = "Immune";
            }
        }
        else
        {
            // make normal again
            color.a = 1f;
            this.GetComponent<MeshRenderer>().material.color = color;
            this.tag = "Player";
        }
    }
}
