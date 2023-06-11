using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiasmasInSector : MonoBehaviour
{
    [SerializeField] private List<RespawningMiasma> miasmasInSector;
    [SerializeField] private float percentTillSectorClean;
    [SerializeField] private int indexOfSector;

    public void CheckIsSectorClean()
    {
        if(indexOfSector == 0)
            GameManager.Instance.FirstMiasmaDestroyed();
        var miasmasActive = 0;
        var miasmasDestroyed = 0;
        foreach (var miasma in miasmasInSector)
        {
            if (miasma.IsDestroyed)
                miasmasDestroyed++;
            else miasmasActive++;
        }
        if(GameManager.Instance.questsCompleted < 6) return;
        
        if ((float)miasmasDestroyed / miasmasInSector.Count >= percentTillSectorClean / 100)
        {
            GameManager.Instance.SectorWithIndexCleared(indexOfSector);
            if (indexOfSector == 1)
                GameManager.Instance.SecondSectorCleared();
            foreach (var miasma in miasmasInSector)
            {
                miasma.SectorCleared = true;
                miasma.DestroyedStarted = true;
                GameManager.Instance.sunIcon.sprite = GameManager.Instance.sunStates[0];
            }
        }
    }

    public void CheckIfAllPurpleDestroyed()
    {
        if (indexOfSector == 0)
        {
            foreach (var miasma in miasmasInSector)
            {
                if (miasma.isPurple == true && !miasma.IsDestroyed)
                    return;
            }
            GameManager.Instance.PurpleMiasmaDestroyed();
        }
    }
}
