//  Name: Spawn.cs
//  Author: ZAC KINDY
//  Function:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Spawn : MonoBehaviour
{
    int totalSkeletoNumber;
    public int maxSkeletonNumber;
    int packSize;
    int spawnerNumberA;
    int spawnerNumberB;
    int spawnerNumberC;

    float skeletonSpawnTimer;
    public float skeletonTimerLength = 2.0f;

    //Pack Size
    int whitePackNumber = 6;
    int greyPackNUmber = 3;
    int purplePackNumber = 1;

    //white
    public GameObject skeletonWhiteSword;
    public GameObject skeletonWhiteMace;
    public GameObject skeletonWhiteBow;
    //Grey
    public GameObject skeletonGreySword;
    public GameObject skeletonGreyMace;
    public GameObject skeletonGreyBow;
    //purple
    public GameObject skeletonPurpleSword;
    public GameObject skeletonPurpleMace;
    public GameObject skeletonPurpleBow;


    //skeletons
    GameObject skeletonWhite;
    GameObject skeletonGrey;
    GameObject skeletonPurple;

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

    [HideInInspector]
    public string skeletonClass;
    bool SpawningAInProgress = false;
    bool SpawningBInProgress = false; 
    bool SpawningCInProgress = false;

    // Start is called before the first frame update
    void Start()
    {

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
        if (totalSkeletoNumber >= maxSkeletonNumber)
        {

        }
        else
        {
            for (int i = 0; i < maxSkeletonNumber; i++)
            {

                //if SpawnZoneA is triggered
                if (spawnZoneAisTriggered == true)
                {
                    int d = 0;

                    skeletonSpawnTimer -= Time.deltaTime;

                    if (totalSkeletoNumber >= maxSkeletonNumber && skeletonSpawnTimer > 0)
                    {
                        //no more spawning
                    }
                    else if (skeletonSpawnTimer <= 0 && d != spawnerNumberA)
                    {
                        spawnZone = SpawnersZoneA[d];
                        skeletonSpawnTimer = skeletonTimerLength;

                        SpawnSkeletons();
                        skeletonSpawnTimer = 0;
                        d++;


                    }

                }
                //else if SpawnZoneB is triggered
                else if (spawnZoneBisTriggered == true)
                {

                    int d = 0;

                    skeletonSpawnTimer = skeletonTimerLength;
                    if (totalSkeletoNumber >= maxSkeletonNumber && skeletonSpawnTimer > 0)
                    {
                        //no more spawning
                    }
                    else if (skeletonSpawnTimer <= 0 && d != spawnerNumberB)
                    {
                        spawnZone = SpawnersZoneB[d];

                        SpawnSkeletons();
                        skeletonSpawnTimer = 0;
                        d++;


                    }

                }
                //else if SpawnZoneC is triggered
                else if (spawnZoneCisTriggered == true)
                {

                    int d = 0;

                    skeletonSpawnTimer = skeletonTimerLength;
                    if (totalSkeletoNumber >= maxSkeletonNumber && skeletonSpawnTimer > 0)
                    {
                        //no more spawning
                    }
                    else if (skeletonSpawnTimer <= 0 && d != spawnerNumberC)
                    {
                        spawnZone = SpawnersZoneC[d];

                        SpawnSkeletons();
                        skeletonSpawnTimer = 0;
                        d++;


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
        Debug.Log("Spawned");
            //look up off setting enemys
            if (Random.value > 0.7)
            {
                packSize = whitePackNumber;
                for (int i = 0; i < packSize; i++)
                {
                    if (i == 0)
                    {
                        skeletonWhite = ChooseSkeletonClass(skeletonWhiteSword, skeletonWhiteMace, skeletonWhiteBow);
                        Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x - 0.5f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                        //skeletonWhite.GetComponent<AI>().thisSkeletonClass = skeletonClass;
                        totalSkeletoNumber++;
                    }
                    else if (i == 1)
                    {
                        skeletonWhite = ChooseSkeletonClass(skeletonWhiteSword, skeletonWhiteMace, skeletonWhiteBow);
                        Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x - 0.5f, spawnZone.transform.position.y, spawnZone.transform.position.z - 2f), Quaternion.identity);
                        // skeletonWhite.GetComponent<AI>().thisSkeletonClass = skeletonClass;
                        totalSkeletoNumber++;
                    }
                    else if (i == 2)
                    {
                        skeletonWhite = ChooseSkeletonClass(skeletonWhiteSword, skeletonWhiteMace, skeletonWhiteBow);
                        Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x - 0.5f, spawnZone.transform.position.y, spawnZone.transform.position.z - 1f), Quaternion.identity);
                        // skeletonWhite.GetComponent<AI>().thisSkeletonClass = skeletonClass;
                        totalSkeletoNumber++;
                    }
                    else if (i == 3)
                    {
                        skeletonWhite = ChooseSkeletonClass(skeletonWhiteSword, skeletonWhiteMace, skeletonWhiteBow);
                        Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x + 0.5f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                        // skeletonWhite.GetComponent<AI>().thisSkeletonClass = skeletonClass;
                        totalSkeletoNumber++;
                    }
                    else if (i == 4)
                    {
                        skeletonWhite = ChooseSkeletonClass(skeletonWhiteSword, skeletonWhiteMace, skeletonWhiteBow);
                        Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x + 0.5f, spawnZone.transform.position.y, spawnZone.transform.position.z - 2f), Quaternion.identity);
                        // skeletonWhite.GetComponent<AI>().thisSkeletonClass = skeletonClass;
                        totalSkeletoNumber++;
                    }
                    else if (i == 5)
                    {
                        skeletonWhite = ChooseSkeletonClass(skeletonWhiteSword, skeletonWhiteMace, skeletonWhiteBow);
                        Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x + 0.5f, spawnZone.transform.position.y, spawnZone.transform.position.z - 1f), Quaternion.identity);
                        //skeletonWhite.GetComponent<AI>().thisSkeletonClass = skeletonClass;
                        totalSkeletoNumber++;
                    }    
                    
                }
          
        }
        //white 70%
            else if (Random.value > 0.2)
        {
            packSize = greyPackNUmber;
            for (int i = 0; i < packSize; i++)
            {
                if (i == 0)
                {
                    skeletonGrey = ChooseSkeletonClass(skeletonGreySword, skeletonGreyMace, skeletonGreyBow);
                    Instantiate(skeletonGrey, spawnZone.transform.position, Quaternion.identity);
                    //skeletonGrey.GetComponent<AI>().thisSkeletonClass = skeletonClass;
                    totalSkeletoNumber++;
                }
                else if (i == 1)
                {
                    skeletonGrey = ChooseSkeletonClass(skeletonGreySword, skeletonGreyMace, skeletonGreyBow);
                    Instantiate(skeletonGrey, new Vector3(spawnZone.transform.position.x + 1f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                    // skeletonGrey.GetComponent<AI>().thisSkeletonClass = skeletonClass;
                    totalSkeletoNumber++;
                }
                else if (i == 2)
                {
                    skeletonGrey = ChooseSkeletonClass(skeletonGreySword, skeletonGreyMace, skeletonGreyBow);
                    Instantiate(skeletonGrey, new Vector3(spawnZone.transform.position.x - 1f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                    //skeletonGrey.GetComponent<AI>().thisSkeletonClass = skeletonClass;
                    totalSkeletoNumber++;
                }
            }
            
        }//grey 20%
        else if (Random.value > 0.1)
        {
            packSize = purplePackNumber;
            for (int i = 0; i < packSize; i++)
            {
                skeletonPurple = ChooseSkeletonClass(skeletonPurpleSword, skeletonPurpleMace, skeletonPurpleBow);
                Instantiate(skeletonPurple, spawnZone.transform.position, Quaternion.identity);
                //skeletonPurple.GetComponent<AI>().thisSkeletonClass = skeletonClass;
                totalSkeletoNumber++;
            }
           
        }//purple 10%
        
    }
    public GameObject ChooseSkeletonClass(GameObject sword, GameObject mace, GameObject bow)
    {
        if (Random.value > 0.5)
        {
            skeletonClass = "Sword";
        }//50% sword
        else if (Random.value > 0.3)
        {
            skeletonClass = "Mace";
        }//30% mace
        else if (Random.value > 0.2)
        {
            skeletonClass = "Bow";
        }//20% bow

        if (skeletonClass == "Sword")
        {
            return sword;
        }//returns the Sword skeleton Prefab
        else if (skeletonClass == "Mace")
        {
            return mace;
        }//returns the Mace skeleton Prefab
        else if (skeletonClass == "Bow")
        {
            return bow;
        }//returns the Bow skeleton Prefab
        return null;
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
                    skeletonWhite = ChooseSkeletonClass(skeletonWhiteSword, skeletonWhiteMace, skeletonWhiteBow);
                    Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x - 2f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                }
                else if (i == 1)
                {
                    skeletonWhite = ChooseSkeletonClass(skeletonWhiteSword, skeletonWhiteMace, skeletonWhiteBow);
                    Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x - 2f, spawnZone.transform.position.y, spawnZone.transform.position.z - 8f), Quaternion.identity);
                }
                else if (i == 2)
                {
                    skeletonWhite = ChooseSkeletonClass(skeletonWhiteSword, skeletonWhiteMace, skeletonWhiteBow);
                    Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x - 2f, spawnZone.transform.position.y, spawnZone.transform.position.z - 4f), Quaternion.identity);
                }
                else if (i == 3)
                {
                    skeletonWhite = ChooseSkeletonClass(skeletonWhiteSword, skeletonWhiteMace, skeletonWhiteBow);
                    Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x + 2f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                }
                else if (i == 4)
                {
                    skeletonWhite = ChooseSkeletonClass(skeletonWhiteSword, skeletonWhiteMace, skeletonWhiteBow);
                    Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x + 2f, spawnZone.transform.position.y, spawnZone.transform.position.z - 8f), Quaternion.identity);
                }
                else if (i == 5)
                {
                    skeletonWhite = ChooseSkeletonClass(skeletonWhiteSword, skeletonWhiteMace, skeletonWhiteBow);
                    Instantiate(skeletonWhite, new Vector3(spawnZone.transform.position.x + 2f, spawnZone.transform.position.y, spawnZone.transform.position.z - 4f), Quaternion.identity);
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
                    skeletonGrey = ChooseSkeletonClass(skeletonGreySword, skeletonGreyMace, skeletonGreyBow);
                    Instantiate(skeletonGrey, spawnZone.transform.position, Quaternion.identity);
                }
                else if (i == 1)
                {
                    skeletonGrey = ChooseSkeletonClass(skeletonGreySword, skeletonGreyMace, skeletonGreyBow);
                    Instantiate(skeletonGrey, new Vector3(spawnZone.transform.position.x + 4f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
                }
                else if (i == 2)
                {
                    skeletonGrey = ChooseSkeletonClass(skeletonGreySword, skeletonGreyMace, skeletonGreyBow);
                    Instantiate(skeletonGrey, new Vector3(spawnZone.transform.position.x - 4f, spawnZone.transform.position.y, spawnZone.transform.position.z), Quaternion.identity);
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
        GUI.Label(new Rect(60, 30, 30, 30), totalSkeletoNumber.ToString());
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
