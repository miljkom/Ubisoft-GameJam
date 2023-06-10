using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour,IInteractable
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;
    public bool Interact(Interactor interactor)
    {
        if (GameManager.Instance.playerInfo.water < 3)
        {
            Debug.LogError("Interaction with water");
            GameManager.Instance.playerInfo.water = 3;
            //TODO nesto nemam pojma
            return true;
        }
        Debug.LogError("Full tank");
        return false;
    }
    
}
