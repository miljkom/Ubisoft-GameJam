using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private GameObject framePanel;

    public bool isDisplayed = false;
    private void Start()
    {
        uiPanel.SetActive(false);
        framePanel.SetActive(false);
        isDisplayed = false;
    }

    public void SetUp(Sprite spriteIcon)
    {
        uiPanel.GetComponent<Image>().sprite = spriteIcon;
        framePanel.SetActive(true);
        uiPanel.SetActive(true);
        isDisplayed = true;
    }

    public void Close()
    {
        uiPanel.SetActive(false);
        framePanel.SetActive(false);
        isDisplayed = false;
    }
}
