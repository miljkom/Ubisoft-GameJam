using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnEnemiesOnThirdRoot : MonoBehaviour
{
    [SerializeField] private List<Transform> enemyPositions;
    [SerializeField] private GameObject enemy;

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyPositions.Count; i++)
        {
            Instantiate(enemy, enemyPositions[i].position, quaternion.identity);
        }
    }
}
