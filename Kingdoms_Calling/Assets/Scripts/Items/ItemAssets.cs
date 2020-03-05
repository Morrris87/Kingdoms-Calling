using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    //https://www.youtube.com/watch?v=2WnAOV7nHW0 at 7:59

    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Sprite ElementalChainLink;
    public Sprite OrganOfDesperation;
    public Sprite TomeOfStat;
    public Sprite CharmOfPressure;
    public Sprite NeedleOfChance;
    public Sprite PiercingSheathe;
}
