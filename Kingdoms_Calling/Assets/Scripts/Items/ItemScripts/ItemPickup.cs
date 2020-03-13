/*
 * Item Pickup script placed on Item prefab
 * Created by: Bradley Williamson
 * On: 03/12/20
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Item to Drop")]
    public Item.ItemType item;

    Collider thisCollider;

    // Start is called before the first frame update
    void Start()
    {
        thisCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// When we enter a trigger collider check if its a player if yes then add it to their inventory
    /// and delete the item gameobject
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        //Verify we are colliding with a player
        //Debug.Log("Collision Detected");
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Player Collision Detected");
            //Add the item to the characters inventory
            other.gameObject.GetComponent<CharacterManager>().inventory.AddItem(new Item { itemType = item });
            //Remove the item drop
            Destroy(this.gameObject);
        }
    }
}
