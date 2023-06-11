using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    [SerializeField] public Image waterIcon;
    [SerializeField] public Image batteryIcon;
    [SerializeField] public Image seedIcon;
    [SerializeField] public Image sunIcon;
    [SerializeField] public List<Sprite> waterStates;
    [SerializeField] public List<Sprite> batteryStates;
    [SerializeField] public List<Sprite> seedStates;
    [SerializeField] public List<Sprite> sunStates;
    [SerializeField] public GameObject corrosionBar;
    [SerializeField] public GameObject healthBar;
    [SerializeField] public GameObject sectorBar;
    public int questsCompleted;
    [SerializeField] private TextMeshProUGUI questText;

    private bool _firstMiasmaDestroyed = false;
    private List<string> _quests;
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
        sectorIncreased += IncreaseSectorBar;
        healthBarImage = healthBar.GetComponent<Image>();
        sectorBarImage = sectorBar.GetComponent<Image>();
        corrosionBarImage = corrosionBar.GetComponent<Image>();
        _quests = Quests.quests;
    }

    public static UnityAction<float> sectorIncreased;
    private void IncreaseSectorBar(float fillAmount)
    {
        sectorBarImage.fillAmount = fillAmount;
    }

    public Image corrosionBarImage;
    public Image healthBarImage;
    public Image sectorBarImage;
    private void PlayerInit()
    {
        playerInfo.battery = 100;
        playerInfo.corrosion = 0;
        playerInfo.water = 1;
        playerInfo.seed = 1;
        batteryIcon.sprite = batteryStates[2];
        seedIcon.sprite = seedStates[1];
        waterIcon.sprite = waterStates[1];
        sunIcon.sprite = sunStates[1];
        corrosionBar.GetComponent<Image>().fillAmount = 0;
        healthBar.GetComponent<Image>().fillAmount = 0;
        sectorBar.SetActive(true);
        healthBar.SetActive(false);
        sectorBar.GetComponent<Image>().fillAmount = 0;
    }

    private void Update()
    {
        corrosionBarImage.fillAmount = playerInfo.corrosion/100;
    }
    

    private void RootInit(int index)
    {
        rootInfo.Add(new Root(100, 60));
        playerInfo.seed++;
        seedIcon.sprite = seedStates[1];
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
        //plants[indexOfSector].plantState = Plant.WaterState.Second;
    }

    public void StartNewQuest()
    {
        Quests.quests.RemoveAt(0);
        questText.text = Quests.quests[0];
        questsCompleted++;
    }

    public void FirstMiasmaDestroyed()
    {
        if(!_firstMiasmaDestroyed && questsCompleted == 0)
            StartNewQuest();
    }

    public void PurpleMiasmaDestroyed()
    {
        if(!_firstMiasmaDestroyed && questsCompleted == 1)
            StartNewQuest();
    }
}