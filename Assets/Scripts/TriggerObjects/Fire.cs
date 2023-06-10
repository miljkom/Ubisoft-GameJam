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
            GameManager.Instance.playerInfo.water -= 3;
            
            //TODO nesto nemam pojma
            return true;
        }
        Debug.LogError("Not enough water");
        return false;
    }
}
