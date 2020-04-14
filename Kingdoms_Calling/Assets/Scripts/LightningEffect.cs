using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningEffect : MonoBehaviour
{
    float abilityLifeTimer;
    bool cooldownActive;

    // Start is called before the first frame update
    void Start()
    {
        abilityLifeTimer = 1f;
        cooldownActive = true;
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

            // Destroy the collider for the ability
            Destroy(gameObject);
        }
    }
}
