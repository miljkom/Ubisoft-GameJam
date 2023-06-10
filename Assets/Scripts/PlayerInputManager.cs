using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public float moveSpeed = 15f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(xMove, 0, zMove) * moveSpeed;
        move.Normalize();
        rb.velocity = move;
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }

}
