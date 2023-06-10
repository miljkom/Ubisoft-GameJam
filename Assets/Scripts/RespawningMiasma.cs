using System;
using UnityEngine;

public class RespawningMiasma : MonoBehaviour
{
    [SerializeField] private float timerTillSpawningAgain;
    [SerializeField] private GameObject miasmaRenderer;
    [HideInInspector] public bool SectorCleared;

    private bool _isDestroyed;
    private float _elapsedTimeInSeconds = 0f;
   
    private void Update()
    {
        if (_isDestroyed && !SectorCleared)
        {
            _elapsedTimeInSeconds += Time.deltaTime;
            if (timerTillSpawningAgain <= _elapsedTimeInSeconds)
            {
                _elapsedTimeInSeconds = 0f;
                miasmaRenderer.SetActive(true);
                _isDestroyed = false;
            }
        }
    }

    public void DestroyMiasma()
    {
        if(_isDestroyed) return;
        _isDestroyed = true;
        _elapsedTimeInSeconds = 0;
        miasmaRenderer.SetActive(false);
    }
}
