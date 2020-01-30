//  Name: CameraControl.cs
//  Author: Connor Larsen
//  Reference: Camera Control Script from Unity Tank Game Tutorial: https://learn.unity.com/project/tanks-tutorial
//  Date: 1/15/2020

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform[] players = new Transform[4];  // Array to store positions of all active players
    public float focusTime = 0.2f;                  // Time for camera to focus on the averagePos
    public float screenEdgeBuffer = 4f;             // Bounds for the camera
    public float minSize = 6.5f;                    // Minimum size for the camera

    [SerializeField] private Camera mainCamera; // The main camera
    private float zoomSpeed;                    // Speed for the camera zoom
    private Vector3 velocity;                   // Velocity for the camera movement
    private Vector3 finalPosition;              // Stores the final position for the camera

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();   // Every frame, update the camera's position
        ZoomCamera();   // Every frame, update the camera's zoom
    }

    private void ZoomCamera()
    {
        Vector3 desLocalPos = transform.InverseTransformPoint(finalPosition);
        float size = 0f;    // Start the size at 0

        // Loop through each of the targets
        for (int i = 0; i < players.Length; i++)
        {
            if (!players[i].gameObject.activeSelf)  // If player isn't active, skip
            {
                continue;
            }

            Vector3 targetLocalPos = transform.InverseTransformPoint(players[i].position);  // Grab the players position
            Vector3 desPostoTarget = targetLocalPos - desLocalPos;                          // Get the position from the desired position of the camera

            size = Mathf.Max(size, Mathf.Abs(desPostoTarget.y));                        // Grabs the largest y coordinate of the players
            size = Mathf.Max(size, Mathf.Abs(desPostoTarget.x) / mainCamera.aspect);    // Calculate the largest size based on the player being to the left or right of the camera
        }

        size += screenEdgeBuffer;           // Add the edge buffer
        size += Mathf.Max(size, minSize);   // If size is smaller than the minimum, grab the minimum

        mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize, size, ref zoomSpeed, focusTime);
    }

    private void MoveCamera()
    {
        // Find the best position for the camera based on where each character is positioned
        Vector3 averagePos = new Vector3();
        int playerCount = 0;

        // Goes through the array of players to find the amount playing
        for (int i = 0; i < players.Length; i++)
        {
            if (!players[i].gameObject.activeSelf)  // If player isn't active, skip
            {
                continue;
            }

            averagePos += players[i].position;  // Adds the current position to the average position
            playerCount++;                      // Increases the total by one
        }

        if (playerCount > 0)    // If one or more players...
        {
            averagePos /= playerCount;  // Divide players by averagePos
        }

        averagePos.y = transform.position.y;    // Keep the y coordinate the default
        finalPosition = averagePos;             // Stores averagePos in finalPosition

        // Move the camera
        transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref velocity, focusTime);
    }
}