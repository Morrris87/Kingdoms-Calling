//  Name: Spawn.cs
//  Author: ZAC KINDY
//  Function:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    int totalSkeletoNumber;
    public int maxSkeletonNumber;
    int packSize;
    int spawnerNumberA;
    int spawnerNumberB;
    int spawnerNumberC;

    //Pack Size
    int whitePackNumber = 6;
    int greyPackNUmber = 3;
    int purplePackNumber = 1;

    //skeletons
     public GameObject skeletonWhite;
     public GameObject skeletonGrey;
     public GameObject skeletonPurple;

    GameObject spawnZone;

    // make a GameObject array for the spawners
    GameObject SpawnZoneA;
    GameObject SpawnZoneB;
    GameObject SpawnZoneC;

    GameObject[] SpawnersZoneA;
    GameObject[] SpawnersZoneB;
    GameObject[] SpawnersZoneC;

    bool spawnZoneAisTriggered = false;
    bool spawnZoneBisTriggered = false;
    bool spawnZoneCisTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        //skeletonWhite = GameObject.FindGameObjectWithTag("White");
        //skeletonGrey = GameObject.FindGameObjectWithTag("Grey");
        //skeletonPurple = GameObject.FindGameObjectWithTag("Purple");

        SpawnZoneA = GameObject.FindGameObjectWithTag("SpawnZoneA");
        SpawnZoneB = GameObject.FindGameObjectWithTag("SpawnZoneB");
        SpawnZoneC = GameObject.FindGameObjectWithTag("SpawnZoneC");

        SpawnersZoneA = GameObject.FindGameObjectsWithTag("SpawnerA");
        SpawnersZoneB = GameObject.FindGameObjectsWithTag("SpawnerB");
        SpawnersZoneC = GameObject.FindGameObjectsWithTag("SpawnerC");
        spawnerNumberA = SpawnersZoneA.Length;
        spawnerNumberB = SpawnersZoneB.Length;
        spawnerNumberC = SpawnersZoneC.Length;
    }

    // Update is called once per frame
    void Update()
    {

         for (int i = 0; i < maxSkeletonNumber; i++)
         {
            if (totalSkeletoNumber >= maxSkeletonNumber)
            {
                //no more spawning
            }
            else
            {
                //if SpawnZoneA is triggered
                if (spawnZoneAisTriggered == true)
                {
                    for (int d = 0; d < spawnerNumberA; d++)
                    {
                        spawnZone = SpawnersZoneA[d];
                        SpawnSkeletons();
                    }    
                }
                //else if SpawnZoneB is triggered
                else if (spawnZoneBisTriggered == true)
                {
                    for (int d = 0; d < spawnerNumberB; d++)
                    {
                        spawnZone = SpawnersZoneB[d];
                        SpawnSkeletons();
                    }
                }
                //else if SpawnZoneC is triggered
                else if (spawnZoneCisTriggered == true)
                {
                    for (int d = 0; d < spawnerNumberC; d++)
                    {
                        spawnZone = SpawnersZoneC[d];
                        SpawnSkeletons();
                    }
                }
            }
        } 

    }
    public void SpawnSkeletons()
    {
        //10% for purple pack size 1
        //20% for grey pack size 3
        //70% for white pack size 6

        //look up off setting enemys
        if (Random.value > 0.7)
        {
            packSize = whitePackNumber;
            for (int i = 0; i < packSize; i++)
            {
                if (i == 0)
                {
                    Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x - 0.5f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                    totalSkeletoNumber++;
                }
                else if (i == 1)
                {
                    Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x - 0.5f, spawnZone.transform.position.y, spawnZone.transform.position.z - 2f), Quaternion.identity);
                    totalSkeletoNumber++;
                }
                else if (i == 2)
                {
                    Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x - 0.5f, spawnZone.transform.position.y, spawnZone.transform.position.z - 1f), Quaternion.identity);
                    totalSkeletoNumber++;
                }
                else if (i == 3)
                {
                    Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x + 0.5f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                    totalSkeletoNumber++;
                }
                else if (i == 4)
                {
                    Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x + 0.5f, spawnZone.transform.position.y, spawnZone.transform.position.z - 2f), Quaternion.identity);
                    totalSkeletoNumber++;
                }
                else if (i == 5)
                {
                    Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x + 0.5f, spawnZone.transform.position.y, spawnZone.transform.position.z - 1f), Quaternion.identity);
                    totalSkeletoNumber++;
                }
            }
        }//white 70%
        else if(Random.value > 0.2)
        {
            packSize = greyPackNUmber;
            for (int i = 0; i < packSize; i++)
            {
                if (i == 0)
                {
                    Instantiate(skeletonGrey, spawnZone.transform.position, Quaternion.identity);
                    totalSkeletoNumber++;
                }
                else if(i == 1)
                {
                    Instantiate(skeletonGrey, new Vector3(spawnZone.transform.position.x + 1f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                    totalSkeletoNumber++;
                }
                else if(i == 2)
                {
                    Instantiate(skeletonGrey, new Vector3(spawnZone.transform.position.x - 1f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                    totalSkeletoNumber++;
                }
            }
        }//grey 20%
        else if(Random.value > 0.1)
        {
            packSize = purplePackNumber;
            for (int i = 0; i < packSize; i++)
            {
                Instantiate(skeletonPurple, spawnZone.transform.position, Quaternion.identity);
                totalSkeletoNumber++;
            }
        }//purple 10%

    }

    public void spawnSkeletonsForBoss(string tempColour)
    {
        if (tempColour == "white")
        {
            packSize = whitePackNumber;
            for (int i = 0; i < packSize; i++)
            {
                if (i == 0)
                {
                    Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x - 0.5f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                }
                else if (i == 1)
                {
                    Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x - 0.5f, spawnZone.transform.position.y, spawnZone.transform.position.z - 2f), Quaternion.identity);
                }
                else if (i == 2)
                {
                    Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x - 0.5f, spawnZone.transform.position.y, spawnZone.transform.position.z - 1f), Quaternion.identity);
                }
                else if (i == 3)
                {
                    Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x + 0.5f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                }
                else if (i == 4)
                {
                    Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x + 0.5f, spawnZone.transform.position.y, spawnZone.transform.position.z - 2f), Quaternion.identity);
                }
                else if (i == 5)
                {
                    Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x + 0.5f, spawnZone.transform.position.y, spawnZone.transform.position.z - 1f), Quaternion.identity);
                }
            }
        }
        else if (tempColour == "grey")
        {
            packSize = greyPackNUmber;
            for (int i = 0; i < packSize; i++)
            {
                if (i == 0)
                {
                    Instantiate(skeletonGrey, spawnZone.transform.position, Quaternion.identity);
                }
                else if (i == 1)
                {
                    Instantiate(skeletonGrey, new Vector3(spawnZone.transform.position.x + 1f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                }
                else if (i == 2)
                {
                    Instantiate(skeletonGrey, new Vector3(spawnZone.transform.position.x - 1f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                }
            }
        }
    }

    public void SpawnClones(GameObject bossPrefab, GameObject SpawnZoneOne, GameObject SpawnZoneTwo, GameObject SpawnZoneThree,GameObject SpawnZoneFour)
    {
        int maxClones = 4;
        GameObject[] SpawnZoneArray = {SpawnZoneOne,SpawnZoneTwo,SpawnZoneThree,SpawnZoneFour};
        //Spawn BossPrefab

        //Spawn at CloneOneSpawn ect

        //for loop 
        // GET GAME OBJECT FOR BOSS AI SCRIPT

        for(int i = 0; i < maxClones; i++)
        {

            Instantiate(bossPrefab, new Vector3(SpawnZoneArray[i].transform.position.x, SpawnZoneArray[i].transform.position.y, SpawnZoneArray[i].transform.position.z), Quaternion.identity);

        }
    }
    private void OnGUI()
    {
        //GUI.Label(new Rect(60, 30, 30, 30), totalSkeletoNumber.ToString());
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (this.tag == "SpawnZoneA")
            {
                spawnZoneAisTriggered = true;
            }
            else if(this.tag == "SpawnZoneB")
            {
                spawnZoneBisTriggered = true;
            }
            else if (this.tag == "SpawnZoneC")
            {
                spawnZoneCisTriggered = true;
            }
        }
    }
}
