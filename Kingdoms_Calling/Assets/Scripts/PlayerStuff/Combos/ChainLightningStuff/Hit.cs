using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class Hit : MonoBehaviour 
{ 
    public float killDelay = 100f;
    public GameObject lightningPrefab;
    public GameObject start;

    LightningBoltScript lastLightning;

    private 
    
    void Start() 
    {
        if (lightningPrefab == null)
        {
            lightningPrefab = GameObject.Find("SimpleLightningBoltAnimatedPrefab");
        }

        lightningPrefab.GetComponent<LightningBoltScript>().StartObject = start;
        lightningPrefab.GetComponent<LightningBoltScript>().EndObject = this.gameObject;

        Instantiate(lightningPrefab, transform);

        //Destroy this component only after killDelay-second passed 
        Destroy( this, killDelay );
    }

    private void Update()
    {

    }
}