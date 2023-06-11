using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringPlant : MonoBehaviour,IInteractable
{
    [SerializeField] private Sprite _prompt;
    public Sprite InteractionPrompt => _prompt;
    public bool Interact(Interactor interactor)
    {
        if (GameManager.Instance.playerInfo.water > 0)
        {
            if (GameManager.Instance.questsCompleted <= 4) return false;
            Debug.LogError("Interaction with wateringPlant");
            GameManager.Instance.playerInfo.water--;
            GameManager.Instance.waterIcon.sprite = GameManager.Instance.waterStates[GameManager.Instance.playerInfo.water];
            GameManager.Instance.CompleteQuest(5);
            //TODO nesto nemam pojma
            return true;
        }
        Debug.LogError("No water");
        return false;
    }

    public bool CanInteract(Interactor interactor)
    {
        return GameManager.Instance.playerInfo.water > 0 && GameManager.Instance.questsCompleted > 4;
    }
}
