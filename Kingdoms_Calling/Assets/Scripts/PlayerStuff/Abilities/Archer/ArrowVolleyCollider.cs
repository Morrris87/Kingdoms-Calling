//  Name: ArrowVolleyCollider.cs
//  Author: Connor Larsen
//  Date: 2/8/2020

using UnityEngine;

public class ArrowVolleyCollider : MonoBehaviour
{
    public Collider abilityCollider;    // The collider for the ability that is placed by the Archer

    public float timerLength = 3f;  // Time in seconds for the collider to last before being destroyed
    private float abilityLifeTimer; // The ability timer
    private bool cooldownActive;    // Bool which determines if the cooldown is running

    // Start is called before the first frame update
    void Start()
    {
        abilityLifeTimer = timerLength; // Sets the length of the cooldown to the amount stored in timerLength
        cooldownActive = true;  // Starts the cooldown timer
    }

    // Update is called once per frame
    void Update()
    {
        // If the abilityLifeTimer has time left and cooldownActive is true...
        if (abilityLifeTimer > 0f && cooldownActive)
        {
            // Decrease timer by deltaTime every frame
            abilityLifeTimer -= Time.deltaTime;
        }

        // If the cooldown has finished...
        if (abilityLifeTimer <= 0f)
        {
            // Stops the cooldown timer
            cooldownActive = false;

            // Destroys the collider for the ability
            Destroy(gameObject);
        }
    }
}