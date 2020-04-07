﻿/*
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
    //Inventory
    [SerializeField] private UI_Inventory uiInventory;

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

    [Header("Item Prefabs")]
    public GameObject ElementalChainLink;
    public GameObject OrganOfDesperation;
    public GameObject CharmOfPressure;
    public GameObject NeedleOfChance;
    public GameObject PiercingSheathe;
    public GameObject TomeOfStatHealth;
    public GameObject TomeOfStatPower;
    public GameObject TomeOfStatSpeed;
    public GameObject TomeOfStatStamina;
    public GameObject TomeOfStatPhysicalDefence;
    public GameObject TomeOfStatMagicDefence;

    [HideInInspector]
    public bool displayLocation = false;

    // Private Variables
    PlayerInputActions inputAction; // InputActions    
    Rigidbody playerRBody;

    //Character Input variables
    Vector2 movementInput;
    Vector2 rightInput;
    Vector3 inputDirection, inputRotation, desiredDirection, lookRot, lookDirection, rotationDirection, targetInputDir;
    float xMove, yMove, xRot, yRot, cycleTimer, abilityInput;
    bool rightStick = false;

    //Player Animator Variables
    Animator playerAnim;

    //Camera information
    Vector3 camForward, camRight;
    GameObject targetedEnemy;
    Quaternion newRotation;

    // Character Abilities/scripts
    BasicAttack basicAttack;
    //Paladin
    HealingSpring paladinEarthHealingSpring;
    Taunt paladinTaunt;
    IronWall paladinIronWall;

    //Warrior
    AxeWhirlwind warriorAxeWhirlwind;
    FlamingLeap warriorFalmingLeap;
    RagingResponse warriorRagingResponse;

    //Assassin    
    ThunderStrike thunderStrike;
    Execution execution;
    ElectricDash electricDash;

    //Archer
    ArrowVolley arrowVolley;
    PiercingArrow piercingArrow;
    LeapOfFaith leapOfFaith;

    //Player and enemy layer index
    int playerLayerIndex, enemyLayerIndex;

    [HideInInspector]
    public Inventory inventory;

    private ItemDropSpawn dropItemScript = new ItemDropSpawn();

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
            paladinEarthHealingSpring = this.GetComponent<HealingSpring>();
            paladinTaunt = this.GetComponent<Taunt>();
            paladinIronWall = this.GetComponent<IronWall>();
        }
        else if (characterClass == CharacterClass.Assassin)
        {
            thunderStrike = this.GetComponent<ThunderStrike>();
            execution = this.GetComponent<Execution>();
            if (GetComponent<ElectricDash>())
                electricDash = GetComponent<ElectricDash>(); // prevent errors for people not using the prefab
        }
        else if (characterClass == CharacterClass.Archer)
        {
            arrowVolley = this.GetComponent<ArrowVolley>();
            piercingArrow = this.GetComponent<PiercingArrow>();
            leapOfFaith = this.GetComponent<LeapOfFaith>();
        }
        else if (characterClass == CharacterClass.Warrior)
        {
            warriorAxeWhirlwind = this.GetComponent<AxeWhirlwind>();
            warriorFalmingLeap = this.GetComponent<FlamingLeap>();
            warriorRagingResponse = this.GetComponent<RagingResponse>();
        }

        this.GetComponent<Health>().characterClass = characterClass;
        playerAnim = GetComponentInChildren<Animator>();
        //this.GetComponentInChildren<Animator>().Play("Idle");
    }
    public void Start()
    {
         //inventory things
        inventory = new Inventory();
        if(uiInventory)
        uiInventory.SetInventory(inventory);
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

        if (playerAnim.GetBool("performingAction") == true)
        {
            ZeroInput();
        }

        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }
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

                    //Debug.Log("right stick rotation");
                    rotationDirection = new Vector3(newRotation.x, newRotation.y, newRotation.z);
                }
            }
        }

        DrawLocation(desiredDirection);

        //Update the cycle timer
        if (cycleTimer >= 0)
            cycleTimer -= Time.deltaTime;

        InputSystem.Update();
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
        if (playerAnim.GetBool("performingAction") == false)
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

    }

    public void Move(InputAction.CallbackContext context)
    {
        if (playerAnim.GetBool("performingAction") == false)
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

        }
        //UpdatePlayer(desiredDirection);//Move the player
    }

    public void Rotate(InputAction.CallbackContext context)
    {
        //Camera Direction for calculating rotation
        camForward = Camera.main.transform.forward;
        camRight = Camera.main.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        if (playerAnim.GetBool("performingAction") == false)
        {
            // Old Input system
            rightInput = context.ReadValue<Vector2>();
            if (rightInput.x != 0 || rightInput.y != 0)
            {
                if (!rightStick)
                    rightStick = true;
                //Debug.Log(rightInput.x + " Right Stick true " + rightInput.y);
                CalcRotation(rightInput);
            }
            else
            {
                if (rightStick)
                    rightStick = false;
                //Debug.Log("Right Stick false");
            }
        }
    }

    public void Ability1(InputAction.CallbackContext context) // For the character's first ability (left bumper)
    {
        if (playerAnim.GetBool("performingAction") == false)
        {
            if (characterClass == CharacterClass.Paladin)
            {
                if (context.ReadValue<float>() == 1)
                    paladinEarthHealingSpring.UseAbility();
            }
            else if (characterClass == CharacterClass.Assassin)
            {
                if (context.ReadValue<float>() == 1)
                {
                    displayLocation = true;
                    //Debug.Log("Started");
                }

                if (context.ReadValue<float>() == 0)
                {
                    if(checkInside())
                    {
                        thunderStrike.UseAbility(abilityIndicator);                       
                    }
                    displayLocation = false;
                }
            }
            else if (characterClass == CharacterClass.Archer)
            {
                if (context.ReadValue<float>() == 1)
                {
                    if (!displayLocation)
                        displayLocation = true;
                    //Debug.Log("Started");
                }

                if (context.ReadValue<float>() == 0)//Button released and we are showing indicator
                {
                    if(checkInside())
                    {
                        arrowVolley.UseAbility();
                        
                    }
                    displayLocation = false;
                    //Debug.Log("arrow volley performed");
                }
            }
            else if (characterClass == CharacterClass.Warrior)
            {
                if (context.ReadValue<float>() == 1)
                {
                    displayLocation = true;
                    //Debug.Log("Started");
                }

                if (context.ReadValue<float>() == 0)
                {
                    if (checkInside())
                    {
                        warriorFalmingLeap.UseAbility(abilityIndicator);                        
                    }
                    displayLocation = false;
                    //Debug.Log("Performed");
                }
            }
        }
    }

    public void Ability2(InputAction.CallbackContext context)   // For the character's second ability (right bumper)
    {
        if (playerAnim.GetBool("performingAction") == false)
        {
            if (characterClass == CharacterClass.Paladin)
            {
                if (context.ReadValue<float>() == 1)//Button pressed
                    paladinTaunt.UseAbility();
            }
            else if (characterClass == CharacterClass.Assassin)
            {
                if (context.ReadValue<float>() == 1)
                    execution.UseAbility();
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
    }

    public void Special(InputAction.CallbackContext context)   // For the character's Evasion/Special
    {
        if (playerAnim.GetBool("performingAction") == false)
        {
            if (characterClass == CharacterClass.Paladin)
            {
                if (context.ReadValue<float>() == 1)//Button pressed
                {
                    paladinIronWall.UseAbility();
                }

                if (context.ReadValue<float>() == 0)//Button released
                {
                    paladinIronWall.EndAbility();
                }

            }
            else if (characterClass == CharacterClass.Assassin)
            {
                if (context.ReadValue<float>() == 1)//Button pressed
                {
                    if (!displayLocation)
                        displayLocation = true;
                }

                if (context.started == false && context.performed == true)//Button released and we are showing indicator
                {
                    if(checkInside())
                    {
                        electricDash.UseAbility(abilityIndicator);
                    }
                    displayLocation = false;
                    //Debug.Log("Special performed");
                }
            }
            else if (characterClass == CharacterClass.Archer)
            {
                if (context.ReadValue<float>() == 1)//Button pressed
                {
                    if (!leapOfFaith.isActive)
                    {
                        if (!displayLocation)
                            displayLocation = true;
                        leapOfFaith.isActive = true;
                    }
                }

                if (context.performed == true && !context.started)//Button released
                {
                    if(checkInside())
                    {
                        if (leapOfFaith.isActive)
                            leapOfFaith.UseAbility(abilityIndicator);
                    }
                    else
                    {
                        leapOfFaith.isActive = false;
                    }
                    if (displayLocation)
                        displayLocation = false;
                }
            }
            else if (characterClass == CharacterClass.Warrior)
            {
                if (context.ReadValue<float>() == 1)//Button pressed
                {
                    warriorRagingResponse.UseAbility();
                }

                if (context.ReadValue<float>() == 0)//Button released
                {

                }
            }
        }
    }

    public void Pause(InputAction.CallbackContext context)
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
        //if (cycleTimer <= 0)
        //{
        //    //cycle
        //    //Grab all colliders inside of the sphere which in our case acts as a circle with the enemy layer mask 
        //    Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, cycleRange, 1 << enemyLayerIndex);

        //    //loop through the skeletons near the player
        //    int i = 0;
        //    while (i < hitColliders.Length)
        //    {
        //        //if the enemy is targeted target the next enemy in the list
        //        if (hitColliders[i].gameObject.GetComponent<AI>().isTargeted == true)
        //        {
        //            hitColliders[i].gameObject.GetComponent<AI>().isTargeted = false;
        //            //If we have a increase in the list then increment else set the first one.
        //            if (hitColliders.Length < (i - 1))
        //            {
        //                hitColliders[i - 1].gameObject.GetComponent<AI>().isTargeted = true;
        //                targetedEnemy = hitColliders[i - 1].gameObject;
        //                Debug.Log("Current Selected Skeleton: " + hitColliders[i - 1].gameObject.name);
        //            }
        //            else
        //            {
        //                hitColliders[0].gameObject.GetComponent<AI>().isTargeted = true;
        //                targetedEnemy = hitColliders[0].gameObject;
        //                Debug.Log("Current Selected Skeleton: " + hitColliders[0].gameObject.name);
        //            }
        //        }
        //        else//If we cannot find the last targeted enemy 
        //        {
        //            //Grab and reset all enemies 
        //            Collider[] fixTargeting = Physics.OverlapSphere(this.transform.position, cycleRange * 10, 1 << enemyLayerIndex);

        //            int j = 0;
        //            while (j < fixTargeting.Length)
        //            {
        //                fixTargeting[j].gameObject.GetComponent<AI>().isTargeted = false;
        //                j++;
        //            }

        //            //Set the first enemy in the close radius of the player as the target
        //            hitColliders[0].gameObject.GetComponent<AI>().isTargeted = true;
        //            Debug.Log("Current Selected Skeleton: " + hitColliders[0].gameObject.name);
        //        }
        //        i++;
        //    }

        //    //once done cycling reset timer
        //    cycleTimer = cycleSpeed;
        //}
        //Debug.Log("CycleTargetB");
    }

    /// <summary>
    /// Cycle enemy targets forwards
    /// </summary>
    /// <param name="context">Input</param>
    public void CycleTargetF(InputAction.CallbackContext context)
    {
        //if (cycleTimer <= 0)
        //{
        //    //cycle
        //    //Grab all colliders inside of the sphere which in our case acts as a circle with the enemy layer mask 
        //    Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, cycleRange, 1 << enemyLayerIndex);

        //    //loop through the skeletons near the player
        //    int i = 0;
        //    while (i < hitColliders.Length)
        //    {
        //        //if the enemy is targeted target the next enemy in the list
        //        if (hitColliders[i].gameObject.GetComponent<AI>().isTargeted == true)
        //        {
        //            hitColliders[i].gameObject.GetComponent<AI>().isTargeted = false;
        //            //If we have a increase in the list then increment else set the first one.
        //            if (hitColliders.Length < (i + 1))
        //            {
        //                hitColliders[i + 1].gameObject.GetComponent<AI>().isTargeted = true;
        //                targetedEnemy = hitColliders[i + 1].gameObject;
        //                Debug.Log("Current Selected Skeleton: " + hitColliders[i + 1].gameObject.name + " : " + i);
        //            }
        //            else
        //            {
        //                hitColliders[0].gameObject.GetComponent<AI>().isTargeted = true;
        //                targetedEnemy = hitColliders[0].gameObject;
        //                Debug.Log("Current Selected Skeleton: " + hitColliders[0].gameObject.name);
        //            }
        //        }
        //        else//If we cannot find the last targeted enemy 
        //        {
        //            //Grab and reset all enemies 
        //            Collider[] fixTargeting = Physics.OverlapSphere(this.transform.position, cycleRange * 10, 1 << enemyLayerIndex);

        //            int j = 0;
        //            while (j < fixTargeting.Length)
        //            {
        //                fixTargeting[j].gameObject.GetComponent<AI>().isTargeted = false;
        //                j++;
        //            }

        //            //Set the first enemy in the close radius of the player as the target
        //            hitColliders[0].gameObject.GetComponent<AI>().isTargeted = true;
        //            Debug.Log("Current Selected Skeleton: " + hitColliders[0].gameObject.name);
        //        }
        //        i++;
        //    }

        //    //once done cycling reset timer
        //    cycleTimer = cycleSpeed;
        //}
        //Debug.Log("CycleTargetF");
    }

    public void DropItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Item droppedItem = inventory.ItemDrop();

            //We have a tomb on our hands :)
            if(droppedItem.itemType == Item.ItemType.TomeOfStat)
            {
                //Check which tomb it is then drop the item infront of the character
                if(droppedItem.stat == Item.statType.Health)
                {
                    dropItemScript.DropItem(TomeOfStatHealth, transform.localPosition + new Vector3(0, 0, 50f), 100);
                }
                else if(droppedItem.stat == Item.statType.Power)
                {
                    dropItemScript.DropItem(TomeOfStatPower, transform.localPosition + new Vector3(0, 0, 50f), 100);
                }
                else if (droppedItem.stat == Item.statType.Speed)
                {
                    dropItemScript.DropItem(TomeOfStatSpeed, transform.localPosition + new Vector3(0, 0, 50f), 100);
                }
                else if (droppedItem.stat == Item.statType.Stamina)
                {
                    dropItemScript.DropItem(TomeOfStatStamina, transform.localPosition + new Vector3(0, 0, 50f), 100);
                }
                else if (droppedItem.stat == Item.statType.PhysicalDefence)
                {
                    dropItemScript.DropItem(TomeOfStatPhysicalDefence, transform.localPosition + new Vector3(0, 0, 50f), 100);
                }
                else if (droppedItem.stat == Item.statType.MagicDefence)
                {
                    dropItemScript.DropItem(TomeOfStatMagicDefence, transform.localPosition + new Vector3(0, 0, 50f), 100);
                }
            }
            //We dont have a tomb on our hands :)
            else
            {
                //Check which item it is then drop the item infront of the character
                if (droppedItem.itemType == Item.ItemType.ElementalChainLink)
                {
                    dropItemScript.DropItem(ElementalChainLink, transform.localPosition + new Vector3(0, 0, 50f), 100);
                }
                else if (droppedItem.itemType == Item.ItemType.OrganOfDesperation)
                {
                    dropItemScript.DropItem(OrganOfDesperation, transform.localPosition + new Vector3(0, 0, 50f), 100);
                }
                else if (droppedItem.itemType == Item.ItemType.CharmOfPressure)
                {
                    dropItemScript.DropItem(CharmOfPressure, transform.localPosition + new Vector3(0, 0, 50f), 100);
                }
                else if (droppedItem.itemType == Item.ItemType.NeedleOfChance)
                {
                    dropItemScript.DropItem(NeedleOfChance, transform.localPosition + new Vector3(0, 0, 50f), 100);
                }
                else if (droppedItem.itemType == Item.ItemType.PiercingSheathe)
                {
                    dropItemScript.DropItem(PiercingSheathe, transform.localPosition + new Vector3(0, 0, 50f), 100);
                }
                
            }
        }
    }

    Collider[] GetInSphereOverlap(Vector3 pos, float r, int layer)
    {
        Collider[] hitColliders = Physics.OverlapSphere(pos, r, layer);

        return hitColliders;
    }

    private void DrawLocation(Vector2 inp)
    {
        if (abilityIndicator)
        {
            //if our indicator isnt active yet activate it
            if (!abilityIndicator.activeInHierarchy && displayLocation)
            {
                abilityIndicator.SetActive(true);
            }

            //if active update the location
            if (abilityIndicator.activeInHierarchy && displayLocation)
            {

                float range = 0;
                float abilityInput = rightInput.magnitude;

                if (characterClass == CharacterClass.Archer)
                {
                    if (leapOfFaith.isActive)
                    {
                        //abilityInput *= -1;
                    }
                    // set the range to the archers volley range
                    //range = arrowVolley.range;
                }
                else if (characterClass == CharacterClass.Assassin)
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
                    range = 10;

                abilityIndicator.transform.localPosition = new Vector3(0.0f, 0.0f, abilityInput * range);
                //Debug.Log(indicatorLocation);
            }

            if (abilityIndicator.activeInHierarchy && !displayLocation)
            {
                abilityIndicator.SetActive(false);
            }
        }
        else if (abilityIndicator == null)
        {
            //Debug.Log("Please set a ability indicator GameObject");
        }
    }

    void ZeroInput()
    {
        desiredDirection = Vector3.zero;
        rotationDirection = Vector3.zero;
    }

    bool checkInside()
    {
        if(abilityIndicator.GetComponent<AbilityIndicator>().insideTerrain)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnGUI()
    {

    }
}