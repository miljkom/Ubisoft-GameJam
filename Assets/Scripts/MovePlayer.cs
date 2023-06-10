using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward / 10;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right / 10;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward / 10;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right / 10;
        }
    }
}
