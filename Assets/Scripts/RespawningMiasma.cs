using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RespawningMiasma : MonoBehaviour
{
    [SerializeField] private float timerTillSpawningAgain;
    [SerializeField] private GameObject miasmaRenderer;
    [HideInInspector] public bool SectorCleared;

    private bool _isDestroyed;
    private float _elapsedTimeInSeconds = 0f;
    private Material _material;
    //private bool _destroyedStarted;
    private float _destroyInSeconds = 0;
    private float startingFloat = 0f;
    private float destroyingFloat = 1f;
    private bool _destroyedStarted;
    private bool _respawnMiasma;

    private void Awake()
    {
        _material = miasmaRenderer.GetComponent<ParticleSystemRenderer>().material;
    }

    private void Update()
    {
        if (_isDestroyed && !SectorCleared)
        {
            _elapsedTimeInSeconds += Time.deltaTime;
            if (timerTillSpawningAgain <= _elapsedTimeInSeconds)
            {
                _elapsedTimeInSeconds = 0f;
                _respawnMiasma = true;
            }
        }

        if (_destroyedStarted)
        {
            _destroyInSeconds += Time.deltaTime;
            _material.SetFloat("_DissolveValue", 
                Mathf.Lerp(startingFloat, destroyingFloat, _destroyInSeconds + Time.deltaTime));
            if (_destroyInSeconds >= 1)
            {
                _destroyedStarted = false;
                _isDestroyed = true;
                _elapsedTimeInSeconds = 0;
            }
        }

        if (_respawnMiasma)
        {
            _destroyInSeconds -= Time.deltaTime;
            _material.SetFloat("_DissolveValue", 
                Mathf.Lerp(startingFloat, destroyingFloat, _destroyInSeconds - Time.deltaTime));
            if (_destroyInSeconds <= 0)
            {
                _isDestroyed = false;
                _elapsedTimeInSeconds = 0;
                _respawnMiasma = false;
            }
        }
    }

    public void DestroyMiasma()
    {
        if(_isDestroyed || _destroyedStarted) return;
        _destroyedStarted = true;
        _destroyInSeconds = 0f;
    }
}
