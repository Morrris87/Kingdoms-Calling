/*
 * Player Input Script(Using New Input System)
 * Resource: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Installation.html
 * Created by: Bradley Williamson
 * On: 1/7/20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{
    // Public Variables
    public float speed = 4.5f;
    public float rotSpeed = 10.0f;

    // Private Variables
    PlayerInputActions inputAction; // InputActions
    Rigidbody playerRBody;
    //Character Input variables
    Vector2 rotationDirection;
    Vector2 movementInput;
    Vector3 inputDirection;
    float x;
    float y;


    private void Awake()
    {
        inputAction = new PlayerInputActions();// Generate new PlayerInputActions
        //Using the input performed method to retrieve the input value and assign to the new created variables in the fixed update
        inputAction.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputAction.PlayerControls.RotateCharacter.performed += ctx => rotationDirection = ctx.ReadValue<Vector2>();

        if (this.GetComponent<Rigidbody>())// Check if the current object has a rigid body attatched
            playerRBody = this.GetComponent<Rigidbody>();
        else
            Debug.Log(this.gameObject + " needs a rigid body");
    }
    
    void FixedUpdate()
    {
        //Grab input x and y 2D
        x = movementInput.x;
        y = movementInput.y;

        //Fill input direction with the Lerp of current pos and destination direction
        Vector3 targetInput = new Vector3(x, 0, y);
        inputDirection = Vector3.Lerp(inputDirection, targetInput, Time.deltaTime * 10f);

        //Fill the desired direction vector with the basic directions 
        Vector3 desiredDirection = new Vector3(inputDirection.x, 0, inputDirection.z);
        //Verify there was input
        if(desiredDirection != Vector3.zero)
            UpdatePlayer(desiredDirection);//Move the player
    }

    /// <summary>
    /// Player update function handling the speed and rotation of the player
    /// </summary>
    /// <param name="dir">The desired direction of the left analog stick</param>
    void UpdatePlayer(Vector3 dir)
    {
        //Debug.Log("Moving" + dir);
        //Generate the new position based on our speed and time passed
        dir = dir * speed * Time.deltaTime;
        //Update that position
        playerRBody.MovePosition(transform.position + dir);

        //Check if the user is giving input(Stops player from resetting to default angle)
        if (x != 0 && y != 0)
        {
            float angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
            playerRBody.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
        }
    }

    void OnEnable()
    {
        inputAction.Enable();
    }
    void OnDisable()
    {
        inputAction.Disable();
    }

    //-------------------------------------------------------------------------------------------------------------------------------------------
    //Input Functions below
    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire");
    }

    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log("Move");
    }
}