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
    [Header("Movement")]
    public float speed = 4.5f;
    public float rotSpeed = 10.0f;

    [Header("Camera")]
    public Camera mainCamera;

    // Private Variables
    PlayerInputActions inputAction; // InputActions
    Rigidbody playerRBody;
    //Character Input variables
    Vector2 rotationDirection, movementInput;
    Vector3 inputDirection, inputRotation;
    float xMove, yMove, xRot, yRot;
    //Camera information
    Vector3 camForward, camRight;


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
        //Grab movement input x and y 2D
        xMove = movementInput.x;
        yMove = movementInput.y;

        //Fill input direction with the Lerp of current pos and destination direction as well as rotation direction
        Vector3 targetInputDir = new Vector3(xMove, 0, yMove);
        inputDirection = Vector3.Lerp(inputDirection, targetInputDir, Time.deltaTime * 10f);

        //Fill the desired direction, rotation vector with the basic directions 
        Vector3 desiredDirection = new Vector3(inputDirection.x, 0, inputDirection.z);

        //Verify there was input
        if (desiredDirection != Vector3.zero)
            UpdatePlayer(desiredDirection);//Move the player
        
        //Call update rotation to change rotation of the player  
        UpdatePlayerRotation();
        
    }

    /// <summary>
    /// Player update function handling the movement/speed of the player
    /// </summary>
    /// <param name="dir">The desired direction of the left analog stick</param>
    void UpdatePlayer(Vector3 dir)
    {
        //Debug.Log("Moving" + dir);
        //Generate the new position based on our speed and time passed
        dir = dir * speed * Time.deltaTime;
        //Update that position
        playerRBody.MovePosition(transform.position + dir);

    }

    /// <summary>
    /// Player update rotation function handling the rotation of the player based on right analog stick input
    /// </summary>
    void UpdatePlayerRotation()
    {
        //Camera Direction for calculating rotation
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        Vector2 input = rotationDirection;

        //Convert "input" to a Vector3 where the Y axis will be used as the Z axis
        Vector3 lookDirection = new Vector3(input.x, 0, input.y);
        //Convert the direction from local space to world space then projecting to get the Vector to rotate to
        Vector3 lookRot = mainCamera.transform.TransformDirection(lookDirection);
        lookRot = Vector3.ProjectOnPlane(lookRot, Vector3.up);

        //Verify we actually have input
        if (lookRot != Vector3.zero)
        {
            //Update the players rigidbody
            Quaternion newRotation = Quaternion.LookRotation(lookRot);
            playerRBody.MoveRotation(newRotation);
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
    //Input Functions below (Examples of how the call are made context being the information given back to us by the input casting that results in our data)
    public void Fire(InputAction.CallbackContext context)
    {
        //Debug.Log("Fire");
    }

    public void CycleTargetB(InputAction.CallbackContext context)
    {
        Debug.Log("CycleTargetB");
    }

    public void CycleTargetF(InputAction.CallbackContext context)
    {
        Debug.Log("CycleTargetF");
    }

    public void Move(InputAction.CallbackContext context)
    {
        //Debug.Log("Move");
    }
}