/*
 * Controller input Manager
 * Resource: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Installation.html
 * Created by: Bradley Williamson
 * On: 03/17/20
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    //public bool isPause;
    Vector2 movement;
    public GameObject parentObject;
    private PlayerInput pI;
    private GraphicRaycaster gr;
    private PointerEventData pointerEventData = new PointerEventData(null);
    public bool alreadySelectedCharacter = false;
    public InputUser user;
    public InputDevice[] userDevice;
    public CharacterCardScript.character chosenCharacter;
    List<RaycastResult> results = new List<RaycastResult>();
    TextMeshProUGUI playerText;


    // Start is called before the first frame update
    void Start()
    {
        gr = GetComponentInParent<GraphicRaycaster>();
        parentObject = GameObject.Find("Canvas");
        pI = GetComponent<PlayerInput>();
        user = pI.user;
        userDevice = pI.devices.ToArray();
        if (pI.devices.Count == 0)
        {
            if (Time.deltaTime != 0)
            {
                //GameObject.Destroy(this.gameObject);
            }
        }
        playerText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //if we havent yet, get which user we are use this to know which character to give the player input
        if (user == null)
        {
            user = pI.user;
        }
        playerText.text = "P" + user.id;

        //if we dont have a ui canvas find it
        if (parentObject == null)
        {
            //if we are in menu
            parentObject = GameObject.Find("Canvas");
            //test

            //if we are ingame
            //if (parentObject == null)
            //    parentObject = GameObject.Find("Canvas_InGame");
        }
        //if we have a canvas check if it is our parent as this is a ui script
        else if (parentObject)
        {
            if (transform.parent == parentObject.transform)
            {
                //do nothing the canvas is our parent
            }
            else
            {
                //set the cursors parent to the Canvas
                transform.SetParent(parentObject.transform);
            }
        }
        if (!pI.uiInputModule)
        {
            pI.uiInputModule = GameObject.Find("EventSystem").GetComponent<InputSystemUIInputModule>();
        }
        if (gr == null)
            gr = GetComponentInParent<GraphicRaycaster>();

        if (pI.devices.Count == 0)
        {
            if (Time.deltaTime != 0)
            {
                //this.gameObject.SetActive(false);
            }
            else if (Time.deltaTime == 0)
            {
                //this.gameObject.SetActive(true);
            }
        }

        Move();
    }

    private void FixedUpdate()
    {
        if (gr != null)
        {
            cursorRaycast();
        }
    }

    void OnMovement(InputValue value)
    {
        i_Movement = value.Get<Vector2>();
        if (Time.deltaTime == 0)
        {
            Move();
        }
        //Debug.Log(i_Movement);
    }

    void OnNo()
    {
        if (Time.deltaTime == 0)
        {
            cursorRaycast();
        }
        //Loop through our results
        foreach (RaycastResult r in results)
        {
            //Check if they have a button if it does we use the first button grabbed
            checkButton(r);

            //Check if we clicked on a character card
            checkCharacter(r);
        }
    }

    void OnYes()
    {
        if (Time.deltaTime == 0)
        {
            cursorRaycast();
        }

        //Loop through our results
        foreach (RaycastResult r in results)
        {
            //if (r.gameObject.tag == "Character")
                //Debug.Log(r);
            //Check if they have a button if it does we use the first button grabbed
            checkButton(r);

            //Check if we clicked on a character card
            checkCharacter(r);
        }
    }

    private void Move()
    {
        if (i_Movement == Vector2.zero || Time.deltaTime == 0)
        {
            movement = new Vector2(i_Movement.x, i_Movement.y) * (moveSpeed/150); //Time.deltaTime;            
        }
        else
        {
            movement = new Vector2(i_Movement.x, i_Movement.y) * moveSpeed * Time.deltaTime;
        }
        transform.Translate(movement);
    }

    void cursorRaycast()
    {
        //reset our results
        results = new List<RaycastResult>();

        //Get our cursors position
        pointerEventData.position = (transform.position);

        //Raycast the UI
        if (gr)
            gr.Raycast(pointerEventData, results);
    }

    void checkButton(RaycastResult r)
    {
        if (r.gameObject.GetComponent<Button>())
        {
            //Debug.Log("Selected " + r.gameObject);
            Button bUI = r.gameObject.GetComponent<Button>();
            //if the button we are clicking has a onclick function use it
            if (bUI.onClick != null)
            {
                bUI.onClick.Invoke();
            }
        }
    }

    void checkOverButton()
    {
        //Loop through our results
        foreach (RaycastResult r in results)
        {

        }
    }

    void checkCharacter(RaycastResult r)
    {
        if (r.gameObject.tag == "Character")
        {
            //call the character picked function on that card
            r.gameObject.GetComponent<CharacterCardScript>().CharacterPicked(this.gameObject);
            //if we have already chosen this character remove it from our variable, if not add it
            if (chosenCharacter == r.gameObject.GetComponent<CharacterCardScript>().thisCharacter)
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
