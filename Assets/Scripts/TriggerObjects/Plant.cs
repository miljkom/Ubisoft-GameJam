using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public WaterState plantState;
    public string InteractionPrompt => _prompt;
    public bool Interact(Interactor interactor)
    {
        if (GameManager.Instance.playerInfo.seed > 0)
        {
            Debug.LogError("Interaction with plant");
            GameManager.Instance.playerInfo.seed--;
            GameManager.Instance.plants.Add(this);
            plantState = WaterState.First;
            //TODO nesto nemam pojma
            return true;
        }
        Debug.LogError("No seed for tree");
        return false;
    }

    public enum WaterState
    {
        First = 1,
        Second = 2
    }
}
