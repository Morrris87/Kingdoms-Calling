/*
 * Item Drop / Spawn script placed on Item spawn location / used to drop loot from boss
 * Created by: Bradley Williamson
 * On: 03/12/20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Testing Class code for items with a id, name, prefab
//[System.Serializable]
//public class Multitype
//{
//    public int x;
//    public string y;
//    public GameObject z;
//}

public class ItemDropSpawn : MonoBehaviour
{
    //Testing Class code
    //public Multitype[] things;

    [Header("Item to drop prefab")]
    public GameObject itemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if(itemPrefab == null)
        {
            Debug.Log(this.gameObject + " Is missing a item prefab to drop or is NULL");
        }
        CreateItem(transform.parent.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateItem(Vector3 location)
    {
        Instantiate(itemPrefab, location, Quaternion.identity);
    }

    public void DropItem(GameObject dropItemPrefab, Vector3 location, float rngChancePercent)
    {
        //Default our spawn all the time atm
        rngChancePercent = 101f;
        //Generate a random float to determine if we will spawn based on the rng sent in
        float rng = Random.Range(0, 100.0f);
        //If our random number is in the range spawn the item 
        if (rng < rngChancePercent)
            Instantiate(itemPrefab, location, Quaternion.identity);
    }
}
