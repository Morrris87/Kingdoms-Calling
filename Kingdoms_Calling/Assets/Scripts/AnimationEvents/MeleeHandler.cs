using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHandler : MonoBehaviour
{
    public GameObject hitbox;

    private BasicAttack basicAttack;

    // Start is called before the first frame update
    void Start()
    {
        basicAttack = GetComponentInParent<BasicAttack>();
    }

    public void MeleeEvent()
    {
        // Take away player's stamina
        GetComponentInParent<Stamina>().DepleteStamina((int)basicAttack.AttackStaminaLoss);

        // DEBUG: Hitbox is visible
        hitbox.GetComponent<MeshRenderer>().enabled = true;

        // Grab all colliders in the hitbox for the weapon
        Collider[] cols = Physics.OverlapBox(basicAttack.weaponHitbox.bounds.center, basicAttack.weaponHitbox.bounds.extents, basicAttack.weaponHitbox.transform.rotation, LayerMask.GetMask("Enemy"));

        // Cycle through each collider in the cols array and deal damage to each enemy inside
        foreach (Collider c in cols)
        {
            c.GetComponentInParent<Health>().Damage(1);
        }
    }

    public void HitboxEvent()
    {
        // DEBUG: Hitbox is invisible
        hitbox.GetComponent<MeshRenderer>().enabled = false;
    }
}