using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class Hit : MonoBehaviour 
{ 
    public float killDelay = 3f;
    public GameObject lightningPrefab;
    public GameObject start;

    //LightningBoltScript lastLightning;

    private 
    
    void Start() 
    {
        lightningPrefab.GetComponent<LightningBoltScript>().StartObject = start;
        lightningPrefab.GetComponent<LightningBoltScript>().EndObject = this.gameObject;

        //Destroy this component only after killDelay-second passed 
        Destroy( this, killDelay );
    }

    private void Update()
    {

    }
}