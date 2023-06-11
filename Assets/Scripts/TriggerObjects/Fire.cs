using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite _prompt;
    public Sprite InteractionPrompt => _prompt;
    public bool Interact(Interactor interactor)
    {
        if (GameManager.Instance.playerInfo.water == 3)
        {
            Debug.LogError("Interaction with fire");
            GameManager.Instance.playerInfo.water -= 1;
            var index = GameManager.Instance.playerInfo.water;
            GameManager.Instance.waterIcon.sprite = GameManager.Instance.waterStates[index];
            
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
