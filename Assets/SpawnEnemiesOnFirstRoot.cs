using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnEnemiesOnFirstRoot : MonoBehaviour
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

    private void Update()
    {
        foreach (var enemy in enemyPositions)
        {
            if (enemy != null)
                return;
        }
        GameManager.Instance.CompleteQuest(9);
        GameManager.turnOffCorutine = true;
    }
}
