using UnityEngine;
using UnityEngine.Serialization;

public class RespawningMiasma : MonoBehaviour
{
    [SerializeField] private float timerTillSpawningAgain;
    [SerializeField] private GameObject miasmaRenderer;
    
    [HideInInspector] public bool IsDestroyed;
    [HideInInspector] public bool DestroyedStarted;
    [HideInInspector] public bool SectorCleared;
    
    private float _elapsedTimeInSeconds = 0f;
    private Material _material;
    private float _destroyInSeconds = 0;
    private float startingFloat = 0f;
    private float destroyingFloat = 1f;

    private bool _respawnMiasma;

    private void Awake()
    {
        _material = miasmaRenderer.GetComponent<ParticleSystemRenderer>().material;
    }

    private void Update()
    {
        if (IsDestroyed && !SectorCleared)
        {
            _elapsedTimeInSeconds += Time.deltaTime;
            if (timerTillSpawningAgain <= _elapsedTimeInSeconds)
            {
                _elapsedTimeInSeconds = 0f;
                _respawnMiasma = true;
            }
        }

        if (DestroyedStarted)
        {
            _destroyInSeconds += Time.deltaTime * 1;
            _material.SetFloat("_DissolveValue", 
                Mathf.Lerp(startingFloat, destroyingFloat, _destroyInSeconds));
            if (_destroyInSeconds >= 1 && !SectorCleared)
            {
                DestroyedStarted = false;
                IsDestroyed = true;
                _elapsedTimeInSeconds = 0;
                MiasmasInSector.MiasmasDestroyed?.Invoke();
                GameManager.sectorIncreased?.Invoke(0.66f);
            }
        }

        if (_respawnMiasma)
        {
            _destroyInSeconds -= Time.deltaTime * 1;
            _material.SetFloat("_DissolveValue", 
                Mathf.Lerp(startingFloat, destroyingFloat, _destroyInSeconds));
            if (_destroyInSeconds <= 0)
            {
                IsDestroyed = false;
                _elapsedTimeInSeconds = 0;
                _respawnMiasma = false;
            }
        }
    }

    public void DestroyMiasma()
    {
        if(IsDestroyed || DestroyedStarted) return;
        GameManager.Instance.AddCorrosion();
        DestroyedStarted = true;
        _destroyInSeconds = 0f;
    }
    
}
