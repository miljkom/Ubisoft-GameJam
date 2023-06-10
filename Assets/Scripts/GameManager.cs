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
    private GameManager()
    {
        instance = this;
        playerInfo = new Player();
    }

    private void Start()
    {
        playerInfo.battery = 100;
        playerInfo.corrosion = 100;
        playerInfo.water = 100;
    }

    public void RefillResources()
    {
        playerInfo.battery -= 5f;
        playerInfo.corrosion -= 5f;
    }
}