using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public float moveSpeed = 15f;
    private Rigidbody rb;
    [SerializeField] private Animator _animatior;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        if (Keyboard.current.aKey.isPressed)
        {
            _animatior.Play("Robot_Move_R");
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            _animatior.Play("Robot_Move_L");
        }
        else if (Keyboard.current.wKey.isPressed)
        {
            _animatior.Play("Robot_Move_R");
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            _animatior.Play("Robot_Move_L");
        }
        else
        {
            _animatior.Play("Robot_Idle");
        }
        Vector3 move = new Vector3(xMove, 0, zMove) * moveSpeed;
        move.Normalize();
        rb.velocity = move;
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }

}
