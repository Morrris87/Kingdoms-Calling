using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour 
{ 
    public float killDelay = 3f;
    void Start() 
    { 
        //Destroy this component only after killDelay-second passed 
        Destroy( this, killDelay ); 
    }
}