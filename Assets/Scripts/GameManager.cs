using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance ??= new GameManager(); }
    }

    public Player playerInfo;
    public List<Root> rootInfo;
    public bool gameOver = false;
    private GameManager()
    {
        instance = this;
        playerInfo = new Player();
        rootInfo = new List<Root>();
    }

    private void Start()
    {
        PlayerInit();
    }

    private void PlayerInit()
    {
        playerInfo.battery = 100;
        playerInfo.corrosion = 0;
        playerInfo.water = 100;
        playerInfo.seed = 1;
    }

    private void RootInit(int index)
    {
        rootInfo[index].health = 100;
        rootInfo[index].timer = 60;
        playerInfo.seed++;
    }

    public void RefillResources()
    {
        if (playerInfo.battery < 100)
        { 
            playerInfo.battery += 0.5f;
        }
    }
    public void LoseBattery()
    {
        if (playerInfo.battery > 0)
        { 
            playerInfo.battery -= 0.5f;
        }
        else
        {
            gameOver = true;
        }
    }

    public void AddCorrosion()
    {
        playerInfo.corrosion += 0.66f;
    }

    public void SectorWithIndexCleared(int indexOfSector)
    {
        RootInit(indexOfSector);
    }
}