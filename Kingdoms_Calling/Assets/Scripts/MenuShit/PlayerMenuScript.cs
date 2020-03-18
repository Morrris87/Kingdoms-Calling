using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMenuScript : MonoBehaviour
{

    public GameObject uiCanvas;
    // Start is called before the first frame update
    void Start()
    {
        uiCanvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        //if we dont have a ui canvas find it
        if (!uiCanvas)
        {
            uiCanvas = GameObject.Find("Canvas");
        }
        else if (uiCanvas.GetComponent<PlayerInputManager>())
        {
            if (uiCanvas.GetComponent<PlayerInputManager>().playerCount < 4)
                uiCanvas.GetComponent<PlayerInputManager>().EnableJoining();
            else if(uiCanvas.GetComponent<PlayerInputManager>().playerCount >= 4)
            {
                uiCanvas.GetComponent<PlayerInputManager>().DisableJoining();
            }
        }
    }
}
