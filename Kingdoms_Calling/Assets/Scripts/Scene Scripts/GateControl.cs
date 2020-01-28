﻿//  Name: GateControl.cs
//  Author: Connor Larsen
//  Date: 1/24/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControl : MonoBehaviour
{
    // Private Variables
    private GameStatus gameStatus;
    private Collider gateCollider;  // Variable to hold the collider for the gate
    [SerializeField] private bool nextLevelAccess;   // Bool to control if the collider is enabled or not

    // Start is called before the first frame update
    void Start()
    {
        gateCollider = this.GetComponent<Collider>();   // Stores the collider on the gate into gateCollider
        nextLevelAccess = false;
        gameStatus = FindObjectOfType<GameStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nextLevelAccess)
        {
            gateCollider.isTrigger = true;
        }
        else
        {
            gateCollider.isTrigger = false;
        }
    }

    // Grabs the value of nextLevelAccess when called
    public bool GetNextLevelAccess()
    {
        return nextLevelAccess;
    }

    // Sets the value of nextLevelAccess to the given value when called
    public void SetNextLevelAccess(bool value)
    {
        nextLevelAccess = value;
    }

    // Called whenever a Collider enters the gate's box collider
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && nextLevelAccess)
        {
            gameStatus.LoadScene("BradleyTest");
        }
    }
}