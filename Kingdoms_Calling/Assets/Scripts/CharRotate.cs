using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharRotate : MonoBehaviour
{
    float speed = 30.0f;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(0f, transform.rotation.y + 1.0f, 0f,0.0f);
    }
}
