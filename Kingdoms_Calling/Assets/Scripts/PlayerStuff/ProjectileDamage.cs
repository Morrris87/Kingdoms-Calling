//  Name: ProjectileDamage.cs
//  Author: Connor Larsen
//  Date: 2/4/2020

using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public enum Attacker { NONE, PLAYER, SKELETON };
    public Attacker attacker;

    private float arrowLife;

    // Start is called before the first frame update
    void Start()
    {
        attacker = Attacker.NONE;
        arrowLife = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward;
        if (arrowLife <= 0f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            arrowLife -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (attacker == Attacker.PLAYER)
        {
            if (other.tag == "White" || other.tag == "Grey" || other.tag == "Purple")
            {
                other.GetComponent<Health>().Damage(10);
            }
        }
        else if (attacker == Attacker.SKELETON)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<Health>().Damage(10);
            }
        }

        Destroy(this.gameObject);
    }
}