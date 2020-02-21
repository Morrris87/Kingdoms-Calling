using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboTextTimer : MonoBehaviour
{
    private float waitTime = 2f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = waitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Text>().text != "")    // If the text in Combo Text is not blank...
        {
            // If timer hasn't completed...
            if (timer > 0f)
            {
                // Subtract timer by deltaTime
                timer -= Time.deltaTime;
            }
            else
            {
                GetComponent<Text>().text = "";
                timer = waitTime;
            }
        }
    }
}