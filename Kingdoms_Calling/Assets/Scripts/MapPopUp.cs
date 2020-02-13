using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPopUp : MonoBehaviour
{
    public Image map;
    GameObject player;
    bool isInBox;
    bool buttonPressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInBox == true)
        {
            if(Input.GetButtonDown("Fire1"))//button Pressed
            {
                buttonPressed = true;
            }
        }
        else if (isInBox == true && buttonPressed == true)
        {
            if (Input.GetButtonDown("Fire1"))//button Pressed
            {
                buttonPressed = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isInBox = true;
        if(buttonPressed == true)
        {
            map.enabled = true;
            // now pause
            Time.timeScale = 0;
        }
        else if(buttonPressed == false)
        {
            map.enabled = false;
            Time.timeScale = 1;
        }
        // pop up a thing that says "HIT A TO READ MAP"
        // when A is hit pause game Display Image
        // to close HIT A AGAIN
        // unpause
    }
    private void OnGUI()
    {
        
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        Vector2 position = new Vector2(x, y);
        if(isInBox == true)
        {
            GUI.Label(new Rect(x,y,Screen.height/2,Screen.width/2), "Press A to open Map");
        }
        else
        {
            // no label?
        }
    }
}
