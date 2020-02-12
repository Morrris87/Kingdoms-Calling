//  Name: TauntCollider.cs
//  Author: Connor Larsen
//  Function: Script attached to the Taunt ability collider that is placed by the Paladin

using UnityEngine;

public class TauntCollider : MonoBehaviour
{
    // Public Variables
    public float timerLength = 1f;  // Time in seconds for the collider to last before being destroyed

    // Private Variables
    private float abilityLifeTimer; // The ability timer
    private bool cooldownActive;    // Bool which determines if the cooldown is running

    // Start is called before the first frame update
    void Start()
    {
        abilityLifeTimer = timerLength; // Sets the length of the cooldown to the amount stored in timerLength
        cooldownActive = true;          // Starts the cooldown timer
        TauntEnemies();                 // Taunt all enemies in the collider
    }

    // Update is called once per frame
    void Update()
    {
        // If the abilityLifeTimer has time left and cooldownActive is true...
        if (abilityLifeTimer >0f && cooldownActive)
        {
            // Decrease timer by deltaTime every frame
            abilityLifeTimer -= Time.deltaTime;
        }

        // If the cooldown has finished...
        if (abilityLifeTimer <= 0f)
        {
            // Stops the cooldown timer
            cooldownActive = false;

            // Destroy the collider for the ability
            Destroy(gameObject);
        }
    }

    public void TauntEnemies()
    {
        // Grab all colliders in the hitboc of the ability
        Collider[] cols = Physics.OverlapBox(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.extents, GetComponent<Collider>().transform.rotation, LayerMask.GetMask("Enemy"));

        // Cycle through each collider in the cols array
        foreach (Collider c in cols)
        {
            // Enemy gets taunted
            c.GetComponent<AI>().isTaunted = true;
        }
    }
}