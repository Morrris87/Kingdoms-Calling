//  Name: ProjectileDamage.cs
//  Author: Connor Larsen
//  Date: 2/4/2020

using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public enum Attacker { PLAYER, SKELETON, NONE };
    public Attacker attacker;
    BasicAttack shot;
    private float arrowLife;

    // Start is called before the first frame update
    void Start()
    {
        shot = GetComponent<BasicAttack>();
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

            Collider[] cols = Physics.OverlapBox(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.extents, GetComponent<Collider>().transform.rotation, LayerMask.GetMask("Enemy"));
            foreach (Collider c in cols)
            {
                if (shot.zacAttackBool == true)
                {
                    c.GetComponent<Health>().Damage((2));
                    Destroy(gameObject);
                }
                else if(shot.zacAttackBool == false)
                {
                    c.GetComponent<Health>().Damage((1));
                    Destroy(gameObject);
                }
            }
        }
    }

}