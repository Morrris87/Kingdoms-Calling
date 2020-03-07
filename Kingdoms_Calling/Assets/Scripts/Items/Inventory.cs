using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory
{
    //add a max for inventory
    int maxItems = 5;


    private List<Item> itemList;
    public Inventory()
    {
        itemList = new List<Item>();

        //test
        AddItem(new Item { itemType = Item.ItemType.CharmOfPressure });
        AddItem(new Item { itemType = Item.ItemType.OrganOfDesperation });
        AddItem(new Item { itemType = Item.ItemType.PiercingSheathe });
        AddItem(new Item { itemType = Item.ItemType.TomeOfStat });
        AddItem(new Item { itemType = Item.ItemType.TomeOfStat });
    }
    public void AddItem(Item item)
    {
        //if not max add 
        if (itemList.Count != maxItems)
        {
            itemList.Add(item);
        }
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
    //GameObject characterClass;
    //CharacterManager cManager;

    //GameObject itemOneUI;
    //GameObject itemTwoUI;
    //GameObject itemThreeUI;
    //GameObject itemFourUI;

    //public Sprite backGround;
    //[HideInInspector]
    //public List<GameObject> ItemList;

    // Start is called before the first frame update
    //void Start()
    //{
    //    cManager = GetComponent<CharacterManager>();

    //    if (cManager.characterClass != CharacterManager.CharacterClass.NONE)
    //    {
    //        if (cManager.characterClass == CharacterManager.CharacterClass.Archer)
    //        {
    //            itemOneUI = GameObject.Find("Canvas/PlayerHUD/Archer/Archer_Item1/ItemImage");
    //            itemTwoUI = GameObject.Find("Canvas/PlayerHUD/Archer/Archer_Item2/ItemImage");
    //            itemThreeUI = GameObject.Find("Canvas/PlayerHUD/Archer/Archer_Item3/ItemImage");
    //            itemFourUI = GameObject.Find("Canvas/PlayerHUD/Archer/Archer_Item4/ItemImage");
    //        }
    //        else if (cManager.characterClass == CharacterManager.CharacterClass.Assassin)
    //        {
    //            itemOneUI = GameObject.Find("Canvas/PlayerHUD/Assassin/Assassin_Item1/ItemImage");
    //            itemTwoUI = GameObject.Find("Canvas/PlayerHUD/Assassin/Assassin_Item2/ItemImage");
    //            itemThreeUI = GameObject.Find("Canvas/PlayerHUD/Assassin/Assassin_Item3/ItemImage");
    //            itemFourUI = GameObject.Find("Canvas/PlayerHUD/Assassin/Assassin_Item4/ItemImage");
    //        }
    //        else if (cManager.characterClass == CharacterManager.CharacterClass.Paladin)
    //        {
    //            itemOneUI = GameObject.Find("Canvas/PlayerHUD/Paladin/Paladin_Item1/ItemImage");
    //            itemTwoUI = GameObject.Find("Canvas/PlayerHUD/Paladin/Paladin_Item2/ItemImage");
    //            itemThreeUI = GameObject.Find("Canvas/PlayerHUD/Paladin/Paladin_Item3/ItemImage");
    //            itemFourUI = GameObject.Find("Canvas/PlayerHUD/Paladin/Paladin_Item4/ItemImage");
    //        }
    //        else if (cManager.characterClass == CharacterManager.CharacterClass.Warrior)
    //        {
    //            itemOneUI = GameObject.Find("Canvas/PlayerHUD/Warrior/Warrior_Item1/ItemImage");
    //            itemTwoUI = GameObject.Find("Canvas/PlayerHUD/Warrior/Warrior_Item2/ItemImage");
    //            itemThreeUI = GameObject.Find("Canvas/PlayerHUD/Warrior/Warrior_Item3/ItemImage");
    //            itemFourUI = GameObject.Find("Canvas/PlayerHUD/Warrior/Warrior_Item4/ItemImage");
    //        }
    //    }

    //    itemOneUI.GetComponent<Image>().sprite = backGround;
    //    itemTwoUI.GetComponent<Image>().sprite = backGround;
    //    itemThreeUI.GetComponent<Image>().sprite = backGround;
    //    itemFourUI.GetComponent<Image>().sprite = backGround;
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

