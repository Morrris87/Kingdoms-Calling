using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MapPopUp : MonoBehaviour
{
    public GameObject map;
    GameObject player;
    bool isInBox;
    bool buttonPressed;
    bool mapActive;

    // Start is called before the first frame update
    void Start()
    {
        //map.canvas.enabled = false;
        mapActive = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        isInBox = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isInBox = false;
    }
    public void MapStuff(InputAction.CallbackContext context)
    {
        if (isInBox == true && mapActive == false)
        {
            if (context.performed == true)//button Pressed
            {
                map.SetActive(true);
                Time.timeScale = 0;//now pause
                mapActive = true;
                
            }
        }
        else if (mapActive == true)
        {
            if (context.performed == true)//button Pressed
            {
                map.SetActive(false);
                Time.timeScale = 1;
                mapActive = false;
            }
        }
    }
    private void OnGUI()
    {
        
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        Vector2 position = new Vector2(x, y);
        if(isInBox == true)
        {
            //GUI.Label(new Rect(x,y,Screen.height/2,Screen.width/2), "Press A to open Map");
        }
        else
        {
            // no label?
        }
    }
}
