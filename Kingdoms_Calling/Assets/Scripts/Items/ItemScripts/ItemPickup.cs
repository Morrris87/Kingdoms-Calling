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
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Inventory>().AddItem(new Item { itemType = item });
            Destroy(this.gameObject);
        }
    }
}
