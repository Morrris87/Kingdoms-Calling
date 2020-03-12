using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public enum ItemType
    {
        ElementalChainLink,
        OrganOfDesperation,
        TomeOfStat,
        CharmOfPressure,
        NeedleOfChance,
        PiercingSheathe,
    }
    public enum statType
    {
        notTomb,
        Health,
        Power,
        Speed,
        Stamina,
        PhysicalDefence,
        MagicDefence
    };
    public statType stat;
    public ItemType itemType;
    UI_Inventory player;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.CharmOfPressure:          return ItemAssets.Instance.CharmOfPressure;
            case ItemType.ElementalChainLink:       return ItemAssets.Instance.ElementalChainLink;
            case ItemType.NeedleOfChance:           return ItemAssets.Instance.NeedleOfChance;
            case ItemType.OrganOfDesperation:       return ItemAssets.Instance.OrganOfDesperation;
            case ItemType.PiercingSheathe:          return ItemAssets.Instance.PiercingSheathe;
            case ItemType.TomeOfStat:               return ItemAssets.Instance.TomeOfStat;

        }
    }
    public void GetEffect()
    {
        if (itemType == ItemType.CharmOfPressure)
        {

        }
        else if (itemType == ItemType.ElementalChainLink)
        {

        }
        else if (itemType == ItemType.NeedleOfChance)
        {

        }
        else if (itemType == ItemType.OrganOfDesperation)
        {
            // if organ of desperation is not in the item list

            if(player.playerHealth.currentHealth == 0)
            {
                player.playerHealth.currentHealth = (player.playerHealth.maxHealth / 2);
                //remove this from list
                // delete item
            }
            // else dont pick up

        }
        else if (itemType == ItemType.PiercingSheathe)
        {

        }
        else if (itemType == ItemType.TomeOfStat)
        {

            //ALL THIS SHIT NEEDS TO BE BALLANCED JUST FUCKING WRITING IT NOW FUCKING FUCK
            
            if (stat == statType.Health)
            {
                // get the playeers stats  
                if (player.playerHealth.maxHealth != 10)
                {
                    player.playerHealth.maxHealth += 1;
                }
                else
                {
                    //DROP
                }
            }
            else if (stat == statType.Power)
            {
                if (player.playerAttack.AttackDamage != 10)
                {
                    player.playerAttack.AttackDamage += 1;
                }
                else
                {
                    //DROP
                }
            }
            else if (stat == statType.Speed)
            {
                if (player.playerSpeed.speed != 10)
                {
                    player.playerSpeed.speed += 1;
                }
                else
                {
                    //DROP
                }
            }
            else if (stat == statType.Stamina)
            {
                if (player.playerStamina.maxStamina != 10)
                {
                    player.playerStamina.maxStamina += 1;
                }
                else
                {
                    //DROP
                }
            }
            else if (stat == statType.PhysicalDefence)
            {
            }
            else if (stat == statType.MagicDefence)
            {
            }
        }
    }
}
