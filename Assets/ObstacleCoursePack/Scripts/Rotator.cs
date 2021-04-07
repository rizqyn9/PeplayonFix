using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 3f;
    public bool obstacle1;
    public bool obstacle2;

    // Update is called once per frame
    private void Update()
    {
        if (MovableObs.ready)
        {
            if (obstacle1)
            {
                transform.Rotate(0f, 0f, speed * Time.deltaTime / 0.01f, Space.Self);
            }
            else if (obstacle2)
            {
                transform.Rotate(0f, speed * Time.deltaTime / 0.01f, 0f);
            }
        }
    }
}