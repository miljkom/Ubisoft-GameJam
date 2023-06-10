using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public bool Interact(Interactor interactor)
    {
        if (GameManager.Instance.playerInfo.seed > 0)
        {
            Debug.LogError("Interaction with tree");
            GameManager.Instance.playerInfo.seed--;
            //TODO nesto nemam pojma
            return true;
        }
        Debug.LogError("No seed for tree");
        return false;
    }
}
