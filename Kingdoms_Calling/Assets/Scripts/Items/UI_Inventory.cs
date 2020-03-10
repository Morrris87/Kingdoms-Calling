using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    public Transform itemSlotContainer;
    public Transform itemSlotTemplate;
    public GameObject currentPlayer;

    [HideInInspector]
    public CharacterManager playerSpeed;
    [HideInInspector]
    public Stamina playerStamina;
    [HideInInspector]
    public Health playerHealth;
    [HideInInspector]
    public BasicAttack playerAttack;

    private void Awake()
    {
        //itemSlotContainer = transform.Find("itemSlotContainer");
        //itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

        playerSpeed = currentPlayer.GetComponent<CharacterManager>();
        playerStamina = currentPlayer.GetComponent<Stamina>();
        playerHealth = currentPlayer.GetComponent<Health>();
        playerAttack = currentPlayer.GetComponent<BasicAttack>();
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        int x = -2;
        int y = 0;
        float itemSlotCellSize = 70f;
        foreach(Item item in inventory.GetItemList())
        {
            //RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("ItemImage").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
        }
    }

}
