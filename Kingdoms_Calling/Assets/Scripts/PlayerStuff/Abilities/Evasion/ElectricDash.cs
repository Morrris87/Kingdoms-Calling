/*
 * Assassin Electric Dash Evasion ability
 * Created by: Bradley Williamson
 * On: 2/6/20
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectricDash : MonoBehaviour
{

    [Header("UI Element")]
    public Image abilityUI; // The UI Images for the abilities

    [Header("Ability Specs")]
    public float dashooldown;     //Cooldown time for the ability itself
    public float dashLength;        //how far you can dash
    public float dashDuration;     //How long the dash lasts
    public int dashDamage;       //How much damage the skill does at each tick
    public GameObject destinationMarker;
    Vector2 leapLocation;

    private bool isUsable;          // When ability is available for use, set this to true


    // Start is called before the first frame update
    void Start()
    {
        isUsable = true;        // Ability starts as usable
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Calling this function uses the ability
    public void UseAbility(Vector3 destPos)
    {
        // Ability has been used, so it needs to cooldown
        isUsable = false;

        // Start the cooldown timer

        // Update the UI with the time remaining

        // Play the ability animation

        // Start Ability
        StartCoroutine(MoveToPosition(transform, destPos, dashDuration));

    }

    /// <summary>
    /// Coroutine to move the players transform from its current position to a destination position in a given amount of time
    /// </summary>
    /// <param name="transform">Our current location</param>
    /// <param name="position">Our destination Position</param>
    /// <param name="timeToMove">Time to move to position</param>
    /// <returns></returns>
    IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        Vector3 currentPos = transform.position;
        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }
}