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
        Invoke("DestroyObject",1f);
    }

    private void Update()
    {
        if (_shouldMove)
        {
            transform.LookAt(_moveTo);
            transform.position -= transform.forward;
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
