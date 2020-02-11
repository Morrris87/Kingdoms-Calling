using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPopUp : MonoBehaviour
{
    public Image map;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnTriggerEnter(player.GetComponent<MeshCollider>());
    }
    private void OnTriggerEnter(Collider other)
    {
        
        // pop up a thing that says "HIT A TO READ MAP"
        // when A is hit pause game Display Image
        // to close HIT A AGAIN
        // unpause
    }
}
