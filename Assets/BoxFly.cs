using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxFly : MonoBehaviour
{
    bool IsLeft = true;
    float time = 0f;
    void Update()
    {
        switch(IsLeft)
        {
            case true: transform.position += Vector3.left * 1f * Time.deltaTime;
            time-=Time.deltaTime;
                if (time <= -3f) { time = 0f; IsLeft = false; } break;
            case false: transform.position += Vector3.right * 1f * Time.deltaTime;
                time -= Time.deltaTime;
                if (time <= -3f) { time = 0f; IsLeft = true; } break;
        }
    }
}
