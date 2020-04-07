using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringToSpawnOnLoad : MonoBehaviour
{
    bool bringCharacters = false;
    public float timeToWait = 1f;

    GameObject archer;
    GameObject assassin;
    GameObject paladin;
    GameObject warrior;

    public GameObject archerSpawn;
    public GameObject assassinSpawn;
    public GameObject paladinSpawn;
    public GameObject warriorSpawn;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        bringCharacters = true;
        archer = GameObject.Find("Character_Archer(Clone)");
        assassin = GameObject.Find("Character_Assassin(Clone)");
        paladin = GameObject.Find("Character_Paladin(Clone)");
        warrior = GameObject.Find("Character_Warrior(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        if(bringCharacters)
        {
            if(timer >= timeToWait)
            {
                if(archer != null)
                {
                    Debug.Log("A Original: " + archer.transform.localPosition);
                    archer.transform.localPosition = archerSpawn.transform.position;
                    Debug.Log("A new: " + archer.transform.localPosition);
                }
                if(assassin != null)
                {
                    assassin.transform.localPosition = assassinSpawn.transform.position;
                }
                if (paladin != null)
                {
                    paladin.transform.localPosition = paladinSpawn.transform.position;
                }
                if (warrior != null)
                {
                    warrior.transform.localPosition = warriorSpawn.transform.position;
                }
                GameObject.Destroy(this);
            }
            timer += Time.deltaTime;
        }

    }
}
