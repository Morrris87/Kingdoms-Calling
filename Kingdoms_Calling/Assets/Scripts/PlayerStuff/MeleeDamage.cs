//  Name: MeleeDamage.cs
//  Author: Connor Larsen
//  Date: 2/6/2020

using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    public enum Attacker { PLAYER, SKELETON, NONE };
    public Attacker attacker;

    //private float hitboxLife;

    // Start is called before the first frame update
    void Start()
    {
        attacker = Attacker.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Animator>());
    }
}