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
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{
    //Slot machine

    // Public Variables
    public enum CharacterClass { NONE, Paladin, Warrior, Assassin, Archer };

    [Header("Character Class")]
    public CharacterClass characterClass;

    [Header("Movement")]
    public float speed = 7.5f;   // Original = 4.5f
    public float rotSpeed = 10.0f;

    [Header("Camera")]
    private Camera mainCamera;

    [Header("CycleInputTimer")]
    public float cycleSpeed = 0.5f;
    public float cycleRange = 10f;

    [Header("Right Analog Stick Targeting Range")]
    public float analogTargetRange = 5.0f;

    [Header("Characters Target")]
    public GameObject abilityIndicator;

    [HideInInspector]
    public bool displayLocation = false;

    // Private Variables
    PlayerInputActions inputAction; // InputActions    
    Rigidbody playerRBody;
    //Character Input variables
    Vector2 movementInput;
    Vector3 inputDirection, inputRotation, desiredDirection, lookRot, lookDirection, rotationDirection, targetInputDir;
    float xMove, yMove, xRot, yRot, cycleTimer;
    bool rightStick = false;
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
    ElectricDash electricDash;

    //Archer
    ArrowVolley arrowVolley;
    PiercingArrow piercingArrow;

    //Player and enemy layer index
    int playerLayerIndex, enemyLayerIndex;

    private void Awake()
    {
        //inputAction = new PlayerInputActions();// Generate new PlayerInputActions
        //inputAction = this.GetComponent<PlayerInputActions>();
        //Using the input performed method to retrieve the input value and assign to the new created variables in the fixed update
        //inputAction.PlayerControls.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        //inputAction.PlayerControls.Rotate.performed += ctx => rotationDirection = ctx.ReadValue<Vector2>();


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

        desiredDirection = Vector3.zero;
        targetInputDir = Vector3.zero;

        mainCamera = Camera.main;

        if (characterClass == CharacterClass.Paladin)
        {
            paladinEarthHealingSpring = this.GetComponent<paladinHealingSpring>();
            paladinTaunt = this.GetComponent<Taunt>();
        }
        else if (characterClass == CharacterClass.Assassin)
        {
            thunderStrike = this.GetComponent<ThunderStrike>();
            execution = this.GetComponent<Execution>();
            if(GetComponent<ElectricDash>())
                electricDash = GetComponent<ElectricDash>(); // prevent errors for people not using the prefab
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

        //this.GetComponentInChildren<Animator>().Play("Idle");
    }

    void Update()
    {
        ////Grab movement input x and y 2D
        //xMove = movementInput.x;
        //yMove = movementInput.y;

        ////Fill input direction with the Lerp of current pos and destination direction as well as rotation direction
        //Vector3 targetInputDir = new Vector3(xMove, 0, yMove);
        //inputDirection = Vector3.Lerp(inputDirection, targetInputDir, Time.deltaTime * 10f);

        ////Fill the desired direction, rotation vector with the basic directions 
        //Vector3 desiredDirection = new Vector3(inputDirection.x, 0, inputDirection.z);


        //Camera Direction for calculating rotation
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        //check if we are rotating right stick
        if (rotationDirection != Vector3.zero && rightStick == true)
        {
            this.GetComponent<Rigidbody>().freezeRotation = false;
            Quaternion newRotation = Quaternion.LookRotation(rotationDirection);
            //Debug.Log(newRotation);
            this.GetComponent<Rigidbody>().MoveRotation(newRotation);
            //if we are not moving freeze the position
            if (!GetComponentInChildren<Animator>().GetBool("isMoving"))
            {
                this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
            else
            {
                this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            }
            //lookRot = Vector3.zero;
        }

        //Verify there was input left stick
        if (desiredDirection != Vector3.zero)
        {
            //this.GetComponent<Rigidbody>().isKinematic = false;
            //desiredDirection.y
            UpdatePlayer(desiredDirection);//Move the player
            if (rightStick == false)
            {
                CalcRotation(movementInput);
                //if we have no input on right stick make the character face movement direction
                if (rotationDirection != Vector3.zero && movementInput != Vector2.zero)
                {
                    this.GetComponent<Rigidbody>().freezeRotation = false;
                    Quaternion newRotation = Quaternion.LookRotation(rotationDirection);
                    //Debug.Log(newRotation);
                    this.GetComponent<Rigidbody>().MoveRotation(newRotation);
                    this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

                    //Debug.Log("left stick rotation");
                    rotationDirection = new Vector3(newRotation.x, newRotation.y, newRotation.z);
                }
            }
        }

        DrawLocation(desiredDirection);

        //Update the cycle timer
        if (cycleTimer >= 0)
            cycleTimer -= Time.deltaTime;

    }

    /// <summary>
    /// Used for handling physics updates
    /// </summary>
    private void FixedUpdate()
    {
        UpdatePlayer(desiredDirection);

        
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
        dir.y = GetComponent<Rigidbody>().velocity.y;
        //Debug.Log(dir);
        GetComponent<Rigidbody>().velocity = dir;
    }

    /// <summary>
    /// Player update rotation function handling the rotation of the player based on right analog stick input
    /// </summary>
    void UpdatePlayerRotation(Vector3 lookRotation)
    {
        //Verify we actually have input
        //if (lookRotation != Vector3.zero)
        //{
        //    //reset the constraints
        //    this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //    this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        //}
        ////else we dont have input prevent spinning
        //else
        //{
        //    this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        //}



        //-----------------------------------------------------------
        //Right analog stick targeting code vvv

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

    void CalcRotation(Vector2 input)
    {
        if (input.x != 0 || input.y != 0)
        {
            // Convert "input" to a Vector3 where the Y axis will be used as the Z axis
            lookDirection = new Vector3(input.x, 0, input.y);
            lookRot = Camera.main.transform.TransformDirection(lookDirection);
            lookRot = Vector3.ProjectOnPlane(lookRot, Vector3.up);
            //Debug.Log(lookRot);
            if (lookRot != Vector3.zero)
                rotationDirection = lookRot;
        }
    }


    //-------------------------------------------------------------------------------------------------------------------------------------------
    //Input Functions below (Examples of how the call are made context being the information given back to us by the input casting that results in our data)
    public void Attack(InputAction.CallbackContext context)
    {
        if (characterClass == CharacterClass.Archer)
        {
            basicAttack.AttackRanged();
        }
        else
        {
            basicAttack.AttackMelee();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        //Debug.Log("Move");
        movementInput = context.ReadValue<Vector2>();

        //Grab movement input x and y 2D
        xMove = movementInput.x;
        yMove = movementInput.y;

        // Sets the isWalking bool to true if the character is moving, otherwise set to false to control animations
        if (xMove > 0 || yMove > 0)
        {
            GetComponentInChildren<Animator>().SetBool("isMoving", true);
            //this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            GetComponentInChildren<Animator>().SetBool("isMoving", false);
            //this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        }
        if (xMove > 0 || xMove < 0 || yMove > 0 || yMove < 0)
        {
            GetComponentInChildren<Animator>().SetBool("isMoving", true);
            //this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            GetComponentInChildren<Animator>().SetBool("isMoving", false);
            this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        //Fill input direction with the Lerp of current pos and destination direction as well as rotation direction
        targetInputDir = new Vector3(xMove, 0, yMove);
        inputDirection = Vector3.Lerp(inputDirection, targetInputDir, Time.deltaTime * 10f);
        //Debug.Log(inputDirection);
        //Fill the desired direction, rotation vector with the basic directions 
        desiredDirection = new Vector3(inputDirection.x, 0, inputDirection.z);

        //UpdatePlayer(desiredDirection);//Move the player
    }

    public void Rotate(InputAction.CallbackContext context)
    {
        //Camera Direction for calculating rotation
        camForward = Camera.main.transform.forward;
        camRight = Camera.main.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        // Old Input system
        Vector2 input = context.ReadValue<Vector2>();
        if (input.x != 0 || input.y != 0)
        {
            if (!rightStick)
                rightStick = true;
            //Debug.Log(rightStick);
            CalcRotation(input);
        }
        else
        {
            if (rightStick)
                rightStick = false;
            //Debug.Log(rightStick);
        }
    }

    public void Ability1(InputAction.CallbackContext context) // For the character's first ability (left bumper)
    {
        if (characterClass == CharacterClass.Paladin)
        {
            if (context.ReadValue<float>() == 1)
                paladinEarthHealingSpring.UseAbility();
        }
        else if (characterClass == CharacterClass.Assassin)
        {
            if (context.ReadValue<float>() == 1)
                thunderStrike.UseAbility(targetedEnemy);
        }
        else if (characterClass == CharacterClass.Archer)
        {
            if (context.ReadValue<float>() == 1)
                arrowVolley.UseAbility(targetedEnemy);
        }
        else if (characterClass == CharacterClass.Warrior)
        {
            if (context.ReadValue<float>() == 1)
            {
                displayLocation = true;
                Debug.Log("Started");
            }

            if (context.ReadValue<float>() == 0)
            {
                warriorFalmingLeap.UseAbility(rotationDirection);
                //displayLocation = false;
                Debug.Log("Performed");
            }
        }
    }

    public void Ability2(InputAction.CallbackContext context)   // For the character's second ability (right bumper)
    {
        if (characterClass == CharacterClass.Paladin)
        {
            if (context.ReadValue<float>() == 1)
                paladinTaunt.tauntEnemies();
        }
        else if (characterClass == CharacterClass.Assassin)
        {
            if (context.ReadValue<float>() == 1)
                execution.UseAbility(targetedEnemy);
        }
        else if (characterClass == CharacterClass.Archer)
        {
            if (context.ReadValue<float>() == 1)
                piercingArrow.UseAbility();
        }
        else if (characterClass == CharacterClass.Warrior)
        {
            if (context.ReadValue<float>() == 1)
                warriorAxeWhirlwind.UseAbility();
        }
    }

    public void PauseGame(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            FindObjectOfType<HUDController>().PauseGame();
        }
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

    private void DrawLocation(Vector2 inp)
    {
        if(abilityIndicator)
        {
            //if our indicator isnt active yet activate it
            if(!abilityIndicator.activeInHierarchy && displayLocation)
            {
                abilityIndicator.SetActive(true);
            }

            //if active update the location
            if (abilityIndicator.activeInHierarchy && displayLocation)
            {
                float range = 0;
                if(characterClass == CharacterClass.Archer)
                {
                    // set the range to the archers volley range
                    // range = arrowVolley.range;?
                }
                else if(characterClass == CharacterClass.Assassin)
                {
                    // set the range to the assassins electric dash range
                    range = electricDash.dashLength;
                }
                else if (characterClass == CharacterClass.Warrior)
                {
                    // set the range to the warriors flaming leap range
                    range = warriorFalmingLeap.leapDistance;
                }
                if (range == 0)
                    range = 1;

                abilityIndicator.transform.localPosition = new Vector3(0.0f, 0.0f, lookDirection.magnitude * range);
                //Debug.Log(indicatorLocation);
            }

            if (abilityIndicator.activeInHierarchy && !displayLocation)
            {
                abilityIndicator.SetActive(false);
            }
        }
        else if(abilityIndicator == null)
        {
            Debug.Log("Please set a ability indicator GameObject");
        }
    }

    private void OnGUI()
    {

    }
}