/*
 * Player Input Script(Using New Input System)
 * Resource: https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Installation.html
 * Created by: Bradley Williamson
 * On: 1/7/20
 */

using Complete;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{
    // Public Variables
    public enum CharacterClass { NONE, Paladin, Warrior, Assassin, Archer };
    

    [Header("Character Class")]
    public CharacterClass characterClass;

    

    [Header("Movement")]
    public float speed = 4.5f;   // Original = 4.5f
    public float rotSpeed = 10.0f;

    [Header("Camera")]
    public Camera mainCamera;

    [Header("CycleInputTimer")]
    public float cycleSpeed = 0.5f;
    public float cycleRange = 10f;

    [Header("Right Analog Stick Targeting Range")]
    public float analogTargetRange = 5.0f;

    // Private Variables
    PlayerInputActions inputAction; // InputActions    
    Rigidbody playerRBody;
    //Character Input variables
    Vector2 rotationDirection, movementInput;
    Vector3 inputDirection, inputRotation;
    float xMove, yMove, xRot, yRot, cycleTimer;
    //Camera information
    Vector3 camForward, camRight;
    GameObject targetedEnemy;
    Quaternion newRotation;
    // Character Abilities/scripts
    BasicAttack basicAttack;
    //Paladin
    paladinHealingSpring paladinEarthHealingSpring;
    Taunt paladinTaunt;
    //Warrior
    AxeWhirlwind warriorAxeWhirlwind;
    FlamingLeap warriorFalmingLeap;

    //Assassin    
    ThunderStrike thunderStrike;
    Execution execution;

    //Archer
    ArrowVolley arrowVolley;
    PiercingArrow piercingArrow;

    //Player and enemy layer index
    int playerLayerIndex, enemyLayerIndex;

    private void Awake()
    {
        inputAction = new PlayerInputActions();// Generate new PlayerInputActions
        //Using the input performed method to retrieve the input value and assign to the new created variables in the fixed update
        inputAction.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputAction.PlayerControls.RotateCharacter.performed += ctx => rotationDirection = ctx.ReadValue<Vector2>();
        

        //Get the player and enemy layermask id's
        playerLayerIndex = LayerMask.NameToLayer("Player");
        enemyLayerIndex = LayerMask.NameToLayer("Enemy");
        cycleTimer = 0;

        if (this.GetComponent<Rigidbody>())// Check if the current object has a rigid body attatched
            playerRBody = this.GetComponent<Rigidbody>();
        else
            Debug.Log(this.gameObject + " needs a rigid body");

        //Handle loading and grabbing character abilities/scripts here
        basicAttack = this.gameObject.GetComponent<BasicAttack>();

        if (characterClass == CharacterClass.Paladin)
        {
            paladinEarthHealingSpring = this.GetComponent<paladinHealingSpring>();
            paladinTaunt = this.GetComponent<Taunt>();
        }
        else if (characterClass == CharacterClass.Assassin)
        {
            thunderStrike = this.GetComponent<ThunderStrike>();
            execution = this.GetComponent<Execution>();
        }
        else if (characterClass == CharacterClass.Archer)
        {
            arrowVolley = this.GetComponent<ArrowVolley>();
            piercingArrow = this.GetComponent<PiercingArrow>();
        }
        else if (characterClass == CharacterClass.Warrior)
        {
            warriorAxeWhirlwind = this.GetComponent<AxeWhirlwind>();
            warriorFalmingLeap = this.GetComponent<FlamingLeap>();
        }
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

        //Update the cycle timer
        if (cycleTimer >= 0)
            cycleTimer -= Time.deltaTime;

        

        //Draw where the player is currently facing
        //UsefullFunctions.DebugRay(transform.position, transform.forward * 5.0f, Color.red);

        //Debug draw testing left
        //UsefullFunctions.DebugRay(transform.position, new Vector3( -1f, 0, 1f), Color.blue);
        //Debug draw testing left
        //UsefullFunctions.DebugRay(transform.position, new Vector3(-0.5f, 0, 1f)* 1.2f, Color.blue);

        //Debug draw testing right
        //UsefullFunctions.DebugRay(transform.position, new Vector3( 1f, 0, 1f), Color.green);
        //Debug draw testing right
        //UsefullFunctions.DebugRay(transform.position, new Vector3( 0.5f, 0, 1f) * 1.2f, Color.green);
    }

    /// <summary>
    /// Player update function handling the movement/speed of the player
    /// </summary>
    /// <param name="dir">The desired direction of the left analog stick</param>
    void UpdatePlayer(Vector3 dir)
    {
        //Debug.Log("Moving" + dir);
        //Generate the new position based on our speed and time passed
        dir = dir * speed * 5 * Time.deltaTime;
        //Update that position
        //playerRBody.MovePosition(transform.position + dir);
        //Debug.Log(dir);
        playerRBody.AddForce(dir * 500);



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
            //reset the constraints
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            //Update the players rigidbody
            newRotation = Quaternion.LookRotation(lookRot);
            Debug.Log("updating move rotation");
            playerRBody.MoveRotation(newRotation);
        }
        //else we dont have input prevent spinning
        else
        {
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        //Handle right analog targeting
        if (cycleTimer <= 0)
        {
            RaycastHit hit;
            // Does the ray intersect any objects in the enemy layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, 1 << enemyLayerIndex))
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Right analog targeting did hit: " + hit.collider.gameObject.name);
                targetedEnemy = hit.collider.gameObject;
                targetedEnemy.GetComponent<AI>().isTargeted = true;

            }
            // Else we didn't hit anyone reset the targeted enemy
            else
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                //Debug.Log("Did not Hit");
                //if we had a targeted enemy reset the last targeted enemies field and erase our last targeted enemy field
                if (targetedEnemy != null)
                {
                    targetedEnemy.GetComponent<AI>().isTargeted = false;
                    targetedEnemy = null;
                }

            }
            cycleTimer = cycleSpeed;//Reset timer whether we looked at a enemy or not
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
    public void Attack(InputAction.CallbackContext context)
    {
        basicAttack.CallBasicAttack();
    }

    public void Move(InputAction.CallbackContext context)
    {
        //Debug.Log("Move");
    }

    public void Ability1(InputAction.CallbackContext context) // For the character's first ability (left bumper)
    {
        if (characterClass == CharacterClass.Paladin)
        {
            if (context.performed)
                paladinEarthHealingSpring.UseAbility();
        }
        else if (characterClass == CharacterClass.Assassin)
        {
            if (context.performed)
                thunderStrike.UseAbility(targetedEnemy);
        }
        else if (characterClass == CharacterClass.Archer)
        {
            if (context.performed)
                arrowVolley.UseAbility(targetedEnemy);
        }
        else if (characterClass == CharacterClass.Warrior)
        {
            warriorFalmingLeap.UseAbility(rotationDirection);

        }
    }

    public void Ability2(InputAction.CallbackContext context)   // For the character's second ability (right bumper)
    {
        if (characterClass == CharacterClass.Paladin)
        {
            if (context.performed)
                paladinTaunt.tauntEnemies();
        }
        else if (characterClass == CharacterClass.Assassin)
        {
            if (context.performed)
                execution.UseAbility(targetedEnemy);
        }
        else if (characterClass == CharacterClass.Archer)
        {
            if (context.performed)
                piercingArrow.UseAbility();
        }
        else if (characterClass == CharacterClass.Warrior)
        {
            if (context.performed)
                warriorAxeWhirlwind.UseAbility();
        }
    }

    public void HealthLoss()
    {
        GetComponent<Health>().DebugHealthLoss();
    }

    public void HealthGain()
    {
        GetComponent<Health>().DebugHealthGain();
    }

    /// <summary>
    /// Cycle enemy targets backwards
    /// </summary>
    /// <param name="context">Input</param>
    public void CycleTargetB(InputAction.CallbackContext context)
    {
        if (cycleTimer <= 0)
        {
            //cycle
            //Grab all colliders inside of the sphere which in our case acts as a circle with the enemy layer mask 
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, cycleRange, 1 << enemyLayerIndex);

            //loop through the skeletons near the player
            int i = 0;
            while (i < hitColliders.Length)
            {
                //if the enemy is targeted target the next enemy in the list
                if (hitColliders[i].gameObject.GetComponent<AI>().isTargeted == true)
                {
                    hitColliders[i].gameObject.GetComponent<AI>().isTargeted = false;
                    //If we have a increase in the list then increment else set the first one.
                    if (hitColliders.Length < (i - 1))
                    {
                        hitColliders[i - 1].gameObject.GetComponent<AI>().isTargeted = true;
                        targetedEnemy = hitColliders[i - 1].gameObject;
                        Debug.Log("Current Selected Skeleton: " + hitColliders[i - 1].gameObject.name);
                    }
                    else
                    {
                        hitColliders[0].gameObject.GetComponent<AI>().isTargeted = true;
                        targetedEnemy = hitColliders[0].gameObject;
                        Debug.Log("Current Selected Skeleton: " + hitColliders[0].gameObject.name);
                    }
                }
                else//If we cannot find the last targeted enemy 
                {
                    //Grab and reset all enemies 
                    Collider[] fixTargeting = Physics.OverlapSphere(this.transform.position, cycleRange * 10, 1 << enemyLayerIndex);

                    int j = 0;
                    while (j < fixTargeting.Length)
                    {
                        fixTargeting[j].gameObject.GetComponent<AI>().isTargeted = false;
                        j++;
                    }

                    //Set the first enemy in the close radius of the player as the target
                    hitColliders[0].gameObject.GetComponent<AI>().isTargeted = true;
                    Debug.Log("Current Selected Skeleton: " + hitColliders[0].gameObject.name);
                }
                i++;
            }

            //once done cycling reset timer
            cycleTimer = cycleSpeed;
        }
        Debug.Log("CycleTargetB");
    }

    /// <summary>
    /// Cycle enemy targets forwards
    /// </summary>
    /// <param name="context">Input</param>
    public void CycleTargetF(InputAction.CallbackContext context)
    {
        if (cycleTimer <= 0)
        {
            //cycle
            //Grab all colliders inside of the sphere which in our case acts as a circle with the enemy layer mask 
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, cycleRange, 1 << enemyLayerIndex);

            //loop through the skeletons near the player
            int i = 0;
            while (i < hitColliders.Length)
            {
                //if the enemy is targeted target the next enemy in the list
                if (hitColliders[i].gameObject.GetComponent<AI>().isTargeted == true)
                {
                    hitColliders[i].gameObject.GetComponent<AI>().isTargeted = false;
                    //If we have a increase in the list then increment else set the first one.
                    if (hitColliders.Length < (i + 1))
                    {
                        hitColliders[i + 1].gameObject.GetComponent<AI>().isTargeted = true;
                        targetedEnemy = hitColliders[i + 1].gameObject;
                        Debug.Log("Current Selected Skeleton: " + hitColliders[i + 1].gameObject.name + " : " + i);
                    }
                    else
                    {
                        hitColliders[0].gameObject.GetComponent<AI>().isTargeted = true;
                        targetedEnemy = hitColliders[0].gameObject;
                        Debug.Log("Current Selected Skeleton: " + hitColliders[0].gameObject.name);
                    }
                }
                else//If we cannot find the last targeted enemy 
                {
                    //Grab and reset all enemies 
                    Collider[] fixTargeting = Physics.OverlapSphere(this.transform.position, cycleRange * 10, 1 << enemyLayerIndex);

                    int j = 0;
                    while (j < fixTargeting.Length)
                    {
                        fixTargeting[j].gameObject.GetComponent<AI>().isTargeted = false;
                        j++;
                    }

                    //Set the first enemy in the close radius of the player as the target
                    hitColliders[0].gameObject.GetComponent<AI>().isTargeted = true;
                    Debug.Log("Current Selected Skeleton: " + hitColliders[0].gameObject.name);
                }
                i++;
            }

            //once done cycling reset timer
            cycleTimer = cycleSpeed;
        }
        Debug.Log("CycleTargetF");
    }

    Collider[] GetInSphereOverlap(Vector3 pos, float r, int layer)
    {
        Collider[] hitColliders = Physics.OverlapSphere(pos, r, layer);

        return hitColliders;
    }

    //public void Move(InputAction.CallbackContext context)
    //{
    //Debug.Log("Move");
    //}
}