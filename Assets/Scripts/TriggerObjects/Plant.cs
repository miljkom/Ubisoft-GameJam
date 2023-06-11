using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite _prompt;
    public WaterState plantState;
    public Sprite InteractionPrompt => _prompt;
    public bool Interact(Interactor interactor)
    {
        if (GameManager.Instance.playerInfo.seed > 0)
        {
            Debug.LogError("Interaction with plant");
            GameManager.Instance.playerInfo.seed--;
            GameManager.Instance.plants.Add(this);
            GameManager.Instance.seedIcon.sprite = GameManager.Instance.seedStates[0];
            plantState = WaterState.First;
            //TODO nesto nemam pojma
            return true;
        }
        Debug.LogError("No seed for tree");
        return false;
    }

    public bool CanInteract(Interactor interactor)
    {
        return GameManager.Instance.playerInfo.seed > 0;
    }

    public enum WaterState
    {
        First = 1,
        Second = 2
    }
}
