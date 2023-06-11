using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnEnemiesOnFirstRoot : MonoBehaviour
{
    [SerializeField] private List<Transform> enemyPositions;
    [SerializeField] private GameObject enemy;

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemyPositions.Count; i++)
        {
            Instantiate(enemy, enemyPositions[i].position, quaternion.identity);
        }
    }
}
