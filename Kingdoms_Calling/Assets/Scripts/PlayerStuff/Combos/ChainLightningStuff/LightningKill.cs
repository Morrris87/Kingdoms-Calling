using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningKill : MonoBehaviour
{
    public float LightningDuration = 3f;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy this component only after killDelay-second passed 
        Destroy(this.gameObject, LightningDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
