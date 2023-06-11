using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesOnSecondRoot : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPositions;
    [SerializeField] private GameObject enemy;

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemyPositions.Count; i++)
        {
            foreach (var enemy in enemyPositions)
            {
                enemy.SetActive(true);
            }
        }
    }
}
