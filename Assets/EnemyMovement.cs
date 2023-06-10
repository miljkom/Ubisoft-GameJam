using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Vector2 _moveTo;
    private bool _shouldMove;
    public void MoveEnemy(Vector2 playerPosition)
    {
        _shouldMove = true;
        _moveTo = playerPosition;
    }

    private void Update()
    {
        if (_shouldMove)
        {
            transform.LookAt(_moveTo);
            transform.position -= transform.forward;
        }
    }
}
