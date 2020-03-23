/*
 * Character select character card script
 * Created by: Bradley Williamson
 * On: 03/23/20
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class CharacterCardScript : MonoBehaviour
{
    //What character this card is    
    public enum character { NONE, Warrior, Paladin, Assassin, Archer };
    [Header("Character")]
    public character thisCharacter;
    public bool isSelected = true;
    public GameObject selectedBy;
    public GameObject selectedCircle;

    // Start is called before the first frame update
    void Start()
    {
        //selectedCircle = GetComponentInChildren<Image>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelected)
        {
            if(selectedCircle.activeSelf == false)
            {
                selectedCircle.SetActive(true);
            }
        }else
        {
            if(selectedCircle.activeSelf == true)
            {
                selectedCircle.SetActive(false);
            }
        }
    }
    
    public void CharacterPicked(GameObject objSelecting)
    {                
        //We're already selected by someone
        if(isSelected)
        {
            //check if the person trying to select is the same person already selected if so unselect
            if (selectedBy == objSelecting)
            {
                isSelected = false;
                selectedBy = null;
                objSelecting.GetComponent<CursorControllerScript>().alreadySelectedCharacter = false;
            }
        }
        //we arn't selected yet
        else if(!isSelected)
        {
            if(objSelecting != null)
            {
                if(objSelecting.GetComponent<CursorControllerScript>().alreadySelectedCharacter == false)
                {
                    isSelected = true;
                    selectedBy = objSelecting;
                    objSelecting.GetComponent<CursorControllerScript>().alreadySelectedCharacter = true;
                }
            }
        }
    }
}
