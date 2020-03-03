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
}
