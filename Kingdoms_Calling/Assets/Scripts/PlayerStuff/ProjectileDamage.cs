//  Name: ProjectileDamage.cs
//  Author: Connor Larsen
//  Date: 2/4/2020

using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public enum Attacker { PLAYER, SKELETON, NONE };
    public Attacker attacker;
    public BasicAttack shot;
    private float arrowLife;

    // Start is called before the first frame update
    void Start()
    {
        shot = new BasicAttack();
        arrowLife = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * 20 * Time.deltaTime;
        if (arrowLife <= 0f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            arrowLife -= Time.deltaTime;

            Collider[] cols = Physics.OverlapBox(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.extents, GetComponent<Collider>().transform.rotation, LayerMask.GetMask("Enemy", "Player"));
            foreach (Collider c in cols)
            {
                if (attacker == Attacker.PLAYER && shot.zacAttackBool == true)
                {
                    c.GetComponent<Health>().Damage((2));
                    Destroy(gameObject);
                }
                else if(attacker == Attacker.PLAYER && shot.zacAttackBool == false)
                {
                    c.GetComponent<Health>().Damage((1));
                    Destroy(gameObject);
                }
                else if(attacker == Attacker.SKELETON)
                {
                    c.GetComponent<Health>().Damage((1));
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

}