using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichHandler : MonoBehaviour
{
    // Public Variables
    public GameObject magicProjectile;
    public Transform spawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MagicAttackEvent()
    {
        Instantiate(magicProjectile, spawner.position, Quaternion.LookRotation(transform.forward, Vector3.up)); // Fire the projectile
    }
}