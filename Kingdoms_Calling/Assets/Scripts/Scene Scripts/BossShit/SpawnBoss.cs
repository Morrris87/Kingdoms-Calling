using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject bossSpawnArea;
    public GameObject bossPrefab;
    public GameObject areaA;
    public GameObject areaB;
    public GameObject areaC;
    int totalNum;
    int maxNum;
    [HideInInspector]
    public int currentNumber;
    int aNum;
    int bNum;
    int cNum;
    int aMax;
    int bMax;
    int cMax;
    bool allSpawned;
    // Start is called before the first frame update
    void Start()
    {
        aNum = areaA.GetComponent<Spawn>().totalSkeletoNumber;
        bNum = areaA.GetComponent<Spawn>().totalSkeletoNumber;
        cNum = areaA.GetComponent<Spawn>().totalSkeletoNumber;
        totalNum = (aNum + bNum + cNum);

        aMax = areaA.GetComponent<Spawn>().maxSkeletonNumber;
        bMax = areaA.GetComponent<Spawn>().maxSkeletonNumber;
        cMax = areaA.GetComponent<Spawn>().maxSkeletonNumber;
        maxNum = (aMax+bMax+cMax);

        currentNumber = totalNum;
        allSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (allSpawned == false)
        {
            if (totalNum >= maxNum)
            {
                allSpawned = true;
            }
        }
        else if(allSpawned == true)
        {
            // keep track of current number
            if(currentNumber == 0)
            {
                //bossCut sceene
                //SpawnBoss
                Instantiate(bossPrefab, bossSpawnArea.transform.position, Quaternion.identity);
            }
        }
    }
}
