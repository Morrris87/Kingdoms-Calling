//  Name: Objectives.cs
//  Author: Connor Larsen  
//  Date: 1/23/2020

using UnityEngine;
using UnityEngine.UI;

public class Objectives : MonoBehaviour
{
    // Variables for the three objective items in the Objectives panel on the HUD
    public Text objective1;
    public Text objective2;
    public Text objective3;

    // Default value for the objective items when no objective is assigned
    private string empty;
    private const int NUM_OBJECTIVES = 3;

    // Start is called before the first frame update
    void Start()
    {
        empty = ""; // Set the empty variable to have an empty string
    }

    // Update is called once per frame
    void Update()
    {
        // Checks to see if objective1 is empty
        if (objective1.text == empty)
        {
            if (objective2.text != empty)   // First check to see if objective 2 is empty
            {
                objective1.text = objective2.text;  // Transfer text from 2 to 1
                objective2.text = empty;            // Remove text from 2
            }
        }

        // Checks to see if objective2 is empty
        if (objective2.text == empty)
        {
            if (objective3.text != empty)   // First check to see if objective 3 is empty
            {
                objective2.text = objective3.text;  // Transfer text from 3 to 2
                objective3.text = empty;            // Remove text from 3
            }
        }
    }

    public void SetObjective(string newObjective, int index)
    {
        if (index == 1)
        {
            objective1.text = newObjective;
        }
        else if (index == 2)
        {
            objective2.text = newObjective;
        }
        else if (index == 3)
        {
            objective3.text = newObjective;
        }
        else
        {
            Debug.Log("ERROR: SetObjective index not within range");
        }
    }
}