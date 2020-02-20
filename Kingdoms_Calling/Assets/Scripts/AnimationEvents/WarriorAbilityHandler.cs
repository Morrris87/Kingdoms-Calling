using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAbilityHandler : MonoBehaviour
{
    private FlamingLeap flamingLeap;
    private AxeWhirlwind axeWhirlwind;
    private AxeWhirlwindCollider axeWhirlwindCollider;
    private float rotationTimer;

    // Start is called before the first frame update
    void Start()
    {
        flamingLeap = GetComponentInParent<FlamingLeap>();
        axeWhirlwind = GetComponentInParent<AxeWhirlwind>();
    }

    public void FlamingLeapEvent()
    {
        // Place the collder for the ability where the player lands
        Instantiate(flamingLeap.areaOfEffect, transform.position, Quaternion.identity);
    }

    public void SpinningAxeEvent()
    {
        // Place the collder for the ability in the spawn area
        Instantiate(axeWhirlwind.areaOfEffect, transform.position, Quaternion.identity);
        rotationTimer = axeWhirlwindCollider.timerLength;
    }
}