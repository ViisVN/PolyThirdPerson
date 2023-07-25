using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFly2 : MonoBehaviour
{
    bool IsLeft = true;
    float time = 0f;
    void Update()
    {
        switch (IsLeft)
        {
            case true:
                transform.position += Vector3.down * 1f * Time.deltaTime;
                time -= Time.deltaTime;
                if (time <= -2f) { time = 0f; IsLeft = false; }
                break;
            case false:
                transform.position += Vector3.up * 1f * Time.deltaTime;
                time -= Time.deltaTime;
                if (time <= -2f) { time = 0f; IsLeft = true; }
                break;
        }
    }
}
