using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherPaladinCombo : MonoBehaviour
{
    private bool timerStarted;
    private float timer;
    private float timerLength;

    private void Start()
    {
        timerStarted = false;
        timerLength = 5f;
        timer = timerLength;
    }

    // ActivateCombo is called when an ability triggers an elemental proc on an enemy it hits
    public void ActivateCombo(GameObject target)
    {
        // Remove proc
        target.GetComponent<ElementManager>().thisElement = ElementManager.ClassElement.NONE;

        // Stop enemy movement
        target.GetComponent<Animator>().enabled = false;

        target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        target.GetComponent<AI>().skeletonStats.speed = 0;

        // Play rooting animation
        target.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 1, target.transform.position.z);

        // Remove any damage modifiers (enemies take normal damage in this state)
        target.GetComponent<Health>().ActivateTrueDamage(false);
    }
}