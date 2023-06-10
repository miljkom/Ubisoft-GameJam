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
    public List<Root> rootInfo = new List<Root>();
    public List<Plant> plants = new List<Plant>();
    public bool gameOver = false;
    private GameManager()
    {
        instance = this;
        playerInfo = new Player();
        rootInfo = new List<Root>();
        plants = new List<Plant>();
    }

    private void Start()
    {
        PlayerInit();
    }

    private void PlayerInit()
    {
        playerInfo.battery = 100;
        playerInfo.corrosion = 0;
        playerInfo.water = 3;
        playerInfo.seed = 1;
    }

    private void RootInit(int index)
    {
        rootInfo.Add(new Root(100, 60));
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
            Debug.LogError("KRAJ");
        }
    }

    public void AddCorrosion()
    {
        playerInfo.corrosion += 0.66f;
        if (playerInfo.corrosion >= 100)
        {
            gameOver = true;
            GameOver();
        }
           
    }

    private void GameOver()
    {
        throw new NotImplementedException();
    }

    public void SectorWithIndexCleared(int indexOfSector)
    {
        RootInit(indexOfSector);
        plants[indexOfSector].plantState = Plant.WaterState.Second;
    }
}