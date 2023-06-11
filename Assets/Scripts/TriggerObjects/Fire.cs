using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite _prompt; 
    private Material _material;
    private float _destroyInSeconds = 0;
    private float startingFloat = 0f;
    private float destroyingFloat = 1f;
    public Sprite InteractionPrompt => _prompt;

    private void Awake()
    {
        _material = GetComponent<ParticleSystemRenderer>().material;
    }

    public bool Interact(Interactor interactor)
    {
        if (GameManager.Instance.playerInfo.water > 0)
        {
            Debug.LogError("Interaction with fire");
            GameManager.Instance.playerInfo.water -= 1;
            var index = GameManager.Instance.playerInfo.water;
            GameManager.Instance.waterIcon.sprite = GameManager.Instance.waterStates[index];
            while (_destroyInSeconds < 1)
            {
                _destroyInSeconds += Time.deltaTime * 0.01f;
                _material.SetFloat("_DissolveValue",
                    Mathf.Lerp(startingFloat, destroyingFloat, _destroyInSeconds));
                Destroy(gameObject);
            }
            
            //TODO nesto nemam pojma
            return true;
        }
        Debug.LogError("Not enough water");
        return false;
    }

    public bool CanInteract(Interactor interactor)
    {
        return GameManager.Instance.playerInfo.water > 0;
    }
}
