using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;

    public bool isDisplayed = false;
    private void Start()
    {
        uiPanel.SetActive(false);
        isDisplayed = false;
    }

    public void SetUp(Sprite spriteIcon)
    {
        uiPanel.GetComponent<Image>().sprite = spriteIcon;
        uiPanel.SetActive(true);
        isDisplayed = true;
    }

    public void Close()
    {
        uiPanel.SetActive(false);
        isDisplayed = false;
    }
}
