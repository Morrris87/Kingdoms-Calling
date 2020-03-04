using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TombOfStat : MonoBehaviour
{
    Sprite ItemImage;

    string thisTag;

    enum statType {Health,Power,Speed,Stamina,PhysicalDefence,MagicDefence};
    statType stat;

    CharacterManager manager;
    Health health;

    // Start is called before the first frame update
    void Start()
    {
        //get tag
        thisTag = this.tag;
        //set stat type to tag
        if(thisTag == "Health")
        {
            stat = statType.Health;
        }
        else if (thisTag == "Power")
        {
            stat = statType.Power;
        }
        else if (thisTag == "Speed")
        {
            stat = statType.Speed;
        }
        else if (thisTag == "Stamina")
        {
            stat = statType.Stamina;
        }
        else if (thisTag == "PhysicalDefence")
        {
            stat = statType.PhysicalDefence;
        }
        else if (thisTag == "MagicDefence")
        {
            stat = statType.MagicDefence;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        OnPickUp(stat);
    }
    void OnPickUp(statType statPickUp)
    {
       if(statPickUp == statType.Health)
        {
            
        }
       else if(statPickUp == statType.MagicDefence)
        {
            
        }
        else if (statPickUp == statType.PhysicalDefence)
        {

        }
        else if (statPickUp == statType.Power)
        {

        }
        else if (statPickUp == statType.Stamina)
        {

        }
        else if (statPickUp == statType.Speed)
        {

        }
    }
}
