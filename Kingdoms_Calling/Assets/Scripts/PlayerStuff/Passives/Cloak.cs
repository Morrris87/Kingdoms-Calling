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
    bool used;

    // Start is called before the first frame update
    void Start()
    {
        color = GetComponentInChildren<SkinnedMeshRenderer>().material.color;
        cloakTime = 2;
        timer = cloakTime;
        execution = GetComponent<Execution>();
        strike = GetComponent<ThunderStrike>();
        used = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (used == false)
        {
            if ((execution.isUsable == false && strike.isUsable == false) ||
                    (execution.isUsable == true && strike.isUsable == false) ||
                    (execution.isUsable == false && strike.isUsable == true))//ability one OR ability two are on cooldown
            {
                timer -= Time.deltaTime;
                color.a = 0.5F;
                this.GetComponentInChildren<SkinnedMeshRenderer>().material.color = color;
                this.gameObject.tag = "Immune";
                if (timer <= 0)// check timer 2 seconds
                { 
                    timer = cloakTime;
                    used = true;
                }
            }
        }
        // works once not twice must thing it tru again!
        else if (used == true)
        {
            color.a = 1f;
            this.GetComponentInChildren<SkinnedMeshRenderer>().material.color = color;
            this.gameObject.tag = "Player";
            timer = cloakTime;
        }
        if (execution.isUsable == true && strike.isUsable == true)
        {
            used = false;
        }
    }
    private void OnGUI()
    {
        //GUI.Label(new Rect(60, 30, 30, 30), timer.ToString());
        //GUI.Label(new Rect(90, 30, 30, 30), used.ToString());
    }
}
