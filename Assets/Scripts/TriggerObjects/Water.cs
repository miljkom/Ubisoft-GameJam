using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour,IInteractable
{
    [SerializeField] private Sprite _prompt;
    public Sprite InteractionPrompt => _prompt;
    public bool Interact(Interactor interactor)
    {
        if (GameManager.Instance.playerInfo.water < 3)
        {
            if (GameManager.Instance.questsCompleted <= 1) return false;
            Debug.LogError("Interaction with water");
            GameManager.Instance.playerInfo.water = 3;
            GameManager.Instance.waterIcon.sprite = GameManager.Instance.waterStates[3];
            GameManager.Instance.WaterCollected();
            //TODO nesto nemam pojma
            return true;
        }
        Debug.LogError("Full tank");
        return false;
    }

    public bool CanInteract(Interactor interactor)
    {
        return GameManager.Instance.playerInfo.water < 3 && GameManager.Instance.questsCompleted > 1;
    }
    
}
