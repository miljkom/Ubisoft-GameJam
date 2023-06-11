using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiasmasInSector : MonoBehaviour
{
    [SerializeField] private List<RespawningMiasma> miasmasInSector;
    [SerializeField] private float percentTillSectorClean;
    [SerializeField] private int indexOfSector;

    public static Action MiasmasDestroyed;

    private void Awake()
    {
        MiasmasDestroyed += CheckIsSectorClean;
    }

    private void CheckIsSectorClean()
    {
        var miasmasActive = 0;
        var miasmasDestroyed = 0;
        foreach (var miasma in miasmasInSector)
        {
            if (miasma.IsDestroyed)
                miasmasDestroyed++;
            else miasmasActive++;
        }

        if ((float)miasmasDestroyed / miasmasInSector.Count >= percentTillSectorClean / 100)
        {
            GameManager.Instance.SectorWithIndexCleared(indexOfSector);
            foreach (var miasma in miasmasInSector)
            {
                miasma.SectorCleared = true;
                miasma.DestroyedStarted = true;
                GameManager.Instance.sunIcon.sprite = GameManager.Instance.sunStates[0];
            }
        }

    }

}
