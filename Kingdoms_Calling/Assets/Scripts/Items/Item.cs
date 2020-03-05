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

        }
        else if (itemType == ItemType.PiercingSheathe)
        {

        }
        else if (itemType == ItemType.TomeOfStat)
        {
            
            if (stat == statType.Health)
            {
            }
            else if (stat == statType.Power)
            {
            }
            else if (stat == statType.Speed)
            {
            }
            else if (stat == statType.Stamina)
            {
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
