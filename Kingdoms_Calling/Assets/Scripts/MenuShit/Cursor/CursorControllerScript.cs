/*
 * Controller input Manager
 * Resource: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Installation.html
 * Created by: Bradley Williamson
 * On: 03/17/20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;

public class CursorControllerScript : MonoBehaviour
{
    Vector2 i_Movement;
    public float moveSpeed;
    public GameObject uiCanvas;
    private PlayerInput pI;
    private GraphicRaycaster gr;
    private PointerEventData pointerEventData = new PointerEventData(null);
    public bool alreadySelectedCharacter = false;
    public InputUser user;
    public InputDevice[] userDevice;
    public CharacterCardScript.character chosenCharacter;
    

    // Start is called before the first frame update
    void Start()
    {
        gr = GetComponentInParent<GraphicRaycaster>();
        uiCanvas = GameObject.Find("Canvas");
        pI = GetComponent<PlayerInput>();
        user = pI.user;
        userDevice = pI.devices.ToArray();
        if(pI.devices.Count == 0)
        {
            //GameObject.Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if we havent yet, get which user we are use this to know which character to give the player input
        if(user == null)
        {
            user = pI.user;
        }
        //if we dont have a ui canvas find it
        if (!uiCanvas)
        {
            uiCanvas = GameObject.Find("Canvas_InGame");
        }
        //if we have a canvas check if it is our parent as this is a ui script
        else if (uiCanvas)
        {
            if (transform.parent == uiCanvas.transform)
            {
                //do nothing the canvas is our parent
            }
            else
            {
                //set the cursors parent to the Canvas
                transform.SetParent(uiCanvas.transform);
            }
        }
        if (!pI.uiInputModule)
        {
            pI.uiInputModule = GameObject.Find("EventSystem").GetComponent<InputSystemUIInputModule>();
        }
        if (gr == null)
            gr = GetComponentInParent<GraphicRaycaster>();

        Move();
    }

    void OnMovement(InputValue value)
    {
        if(value.Get<Vector2>() != Vector2.zero)
        {
            i_Movement = value.Get<Vector2>();
            Vector2 movement = new Vector2(i_Movement.x, i_Movement.y) * moveSpeed; //Time.deltaTime;
            transform.Translate(movement);
        }
        
    }

    void OnNo()
    {
        checkOntop();
    }

    void OnYes()
    {
        Debug.Log("onyes");
        checkOntop();
    }

    private void Move()
    {
        //Vector2 movement = new Vector2(i_Movement.x, i_Movement.y) * moveSpeed; //Time.deltaTime;
        //transform.Translate(movement);
        //Debug.Log(userDevice[0] + " is moving");
    }

    void checkOntop()
    {
        //Get our cursors position
        pointerEventData.position = (transform.position);
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast the UI
        if (gr)
            gr.Raycast(pointerEventData, results);
        Debug.Log(pointerEventData.position);
        //We are over something
        if (results.Count > 0)
        {
            //Loop through our results
            foreach (RaycastResult r in results)
            {
                Debug.Log(r);
                //Check if they have a button if it does we use the first button grabbed
                if (r.gameObject.GetComponent<Button>())
                {
                    Button bUI = r.gameObject.GetComponent<Button>();
                    //if the button we are clicking has a onclick function use it
                    if (bUI.onClick != null)
                    {
                        bUI.onClick.Invoke();
                    }
                }

                //Check if we clicked on a character card
                if(r.gameObject.tag == "Character")
                {
                    //call the character picked function on that card
                    r.gameObject.GetComponent<CharacterCardScript>().CharacterPicked(this.gameObject);
                    //if we have already chosen this character remove it from our variable, if not add it
                    if(chosenCharacter == r.gameObject.GetComponent<CharacterCardScript>().thisCharacter)
                    {
                        chosenCharacter = CharacterCardScript.character.NONE;
                    }
                    else
                    {
                        chosenCharacter = r.gameObject.GetComponent<CharacterCardScript>().thisCharacter;
                    }
                }
            }
        }
    }
}
