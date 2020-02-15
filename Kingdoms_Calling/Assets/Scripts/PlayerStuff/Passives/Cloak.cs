using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloak : MonoBehaviour
{
    Execution execution;
    ThunderStrike strike;
    float timer;
    [HideInInspector] public int cloakTime;
    Color color;

    // Start is called before the first frame update
    void Start()
    {
        color = GetComponentInChildren<SkinnedMeshRenderer>().material.color;
        cloakTime = 2;
        timer = 0;
        execution = GetComponent<Execution>();
        strike = GetComponent<ThunderStrike>();
    }

    // Update is called once per frame
    void Update()
    {
        if (execution.isUsable == false || strike.isUsable == false)//ability one OR ability two are on cooldown
        {
            timer += Time.deltaTime;
            if (timer >= cloakTime)// check timer 2 seconds
            {
                color.a = 0.5F;
                this.GetComponentInChildren<SkinnedMeshRenderer>().material.color = color;
                this.tag = "Immune";
            }
        }
        else
        {
            // make normal again
            color.a = 1f;
            this.GetComponentInChildren<SkinnedMeshRenderer>().material.color = color;
            this.tag = "Player";
        }
    }
}
