using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityIndicator : MonoBehaviour
{
    public bool insideTerrain = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("Enter");
        if (insideTerrain == false)
        {
            if (collision.gameObject.tag == "Terrain")
            {
                insideTerrain = true;
                Debug.Log("Inside terrain");
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        //Debug.Log("Exit");
        if (insideTerrain == true)
        {
            if (collision.gameObject.tag == "Terrain")
            {
                insideTerrain = false;
                //Debug.Log("Out of terrain");
            }
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if(other.gameObject.tag == "Terrain")
    //    Debug.Log("Test Stay");
    //}


}
