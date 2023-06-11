using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnEnemiesOnFirstRoot : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPositions;
    [SerializeField] private GameObject enemy;

    private bool _destroyedAll;

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
        if(_destroyedAll) return;
        foreach (var enemy in enemyPositions)
        {
            if (enemy != null)
            {
                _destroyedAll = false;
                return;
            }
        }

        GameManager.Instance.CompleteQuest(9);
        GameManager.Instance.StartFire();
        GameManager.turnOffCorutine = true;
        _destroyedAll = true;
    }
}
