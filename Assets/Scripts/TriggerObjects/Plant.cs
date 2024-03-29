using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite _prompt;
    [SerializeField] public GameObject plant0;
    [SerializeField] public GameObject plant1;
    [SerializeField] public GameObject plant2;
    public WaterState plantState;
    public Sprite InteractionPrompt => _prompt;
    public bool Interact(Interactor interactor)
    {
        if (GameManager.Instance.playerInfo.seed > 0)
        {
            if (GameManager.Instance.playerInfo.water == 0) return false;
            Debug.LogError("Interaction with plant");
            GameManager.Instance.CompleteQuest(4);
            GameManager.Instance.playerInfo.seed--;
            GameManager.Instance.plants.Add(this);
            GameManager.Instance.seedIcon.sprite = GameManager.Instance.seedStates[0];
            plant1.SetActive(true);
            plantState = WaterState.First;
            //TODO nesto nemam pojma
            return true;
        }
        Debug.LogError("No seed for tree");
        return false;
    }

    public bool CanInteract(Interactor interactor)
    {
        return GameManager.Instance.playerInfo.seed > 0 && GameManager.Instance.playerInfo.water > 0;
    }

    public enum WaterState
    {
        None = 0,
        First = 1,
        Second = 2
    }
}
