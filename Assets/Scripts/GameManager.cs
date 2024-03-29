using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public List<GameObject> rootObjects = new List<GameObject>();
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
    [SerializeField] public Hub hub;
    [SerializeField] public Sprite waterIconQuest;
    [SerializeField] public Sprite plantIconQuest;
    public int questsCompleted;
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private List<GameObject> fires;

    private bool _firstMiasmaDestroyed = false;
    private List<string> _quests;
    private bool _secondSecondCleared;
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
        healthBarImage = healthBar.GetComponent<Image>();
        corrosionBarImage = corrosionBar.GetComponent<Image>();
        _quests = Quests.quests;
    }

    public void StartFire()
    {
        foreach (var fire in fires)
        {
            fire.SetActive(true);
        }
    }

    public Image corrosionBarImage;
    public Image healthBarImage;
    private void PlayerInit()
    {
        playerInfo.battery = 100;
        playerInfo.corrosion = 0;
        playerInfo.water = 0;
        playerInfo.seed = 1;
        batteryIcon.sprite = batteryStates[2];
        seedIcon.sprite = seedStates[1];
        waterIcon.sprite = waterStates[0];
        sunIcon.sprite = sunStates[1];
        corrosionBar.GetComponent<Image>().fillAmount = 0;
        healthBar.GetComponent<Image>().fillAmount = 0;
    }

    private void Update()
    {
        corrosionBarImage.fillAmount = playerInfo.corrosion/100;
        if (playerInfo.battery > 66f)
        {
            batteryIcon.sprite = batteryStates[2];
        }
        else if (playerInfo.battery is < 66f and > 33f)
        {
            batteryIcon.sprite = batteryStates[1];
        }
        else if (playerInfo.battery == 0)
        {
            batteryIcon.sprite = batteryStates[0];
        }

        if (questsCompleted > 8)
        {
            healthBar.GetComponent<Image>().fillAmount = rootInfo[0].health;
        }
    }
    

    private void RootInit(int index)
    {
        rootInfo.Add(new Root(100, 5));
        StartCoroutine(timerRoot(rootInfo[index], index));
        rootObjects[index].SetActive(true);
        healthBar.GetComponent<Image>().fillAmount = 100;
        playerInfo.seed++;
        seedIcon.sprite = seedStates[1];
    }

    public void RefillResources()
    {
        if (playerInfo.battery < 100)
        {
            playerInfo.battery += 8f;
        }
        else
        {
            CompleteQuest(6);
            CompleteQuest8AndSpawnEnemies();
        }
    }

    private void CompleteQuest8AndSpawnEnemies()
    {
        if (questsCompleted == 8)
        {
            StartNewQuest();
            FindObjectOfType<SpawnEnemiesOnFirstRoot>().SpawnEnemies();
            StartCoroutine(LoseHealthFirst());
        }
    }

    public static bool turnOffCorutine = false;
    public IEnumerator LoseHealthFirst()
    {
        rootInfo[0].health -= 1;
        if (turnOffCorutine)
            yield break;
        yield return new WaitForSeconds(1f);
        StartCoroutine(LoseHealthFirst());
    }
    public IEnumerator LoseHealthSecond()
    {
        rootInfo[0].health -= 1;
        rootInfo[1].health -= 1;
        //TODO prekini korutinu
        yield return new WaitForSeconds(1f);
        StartCoroutine(LoseHealthSecond());
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
        playerInfo.corrosion += 0.22f;
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
        plants[indexOfSector].plant0.SetActive(false);
        plants[indexOfSector].plant1.SetActive(false);
        plants[indexOfSector].plant2.SetActive(true);
        //plants[indexOfSector].plantState = Plant.WaterState.Second;
        if(questsCompleted == 7)
            StartNewQuest();
    }

    private IEnumerator timerRoot(Root root,int index)
    {
        root.timer--;
        if (root.timer == 0)
        {
            hub.hubs[index].SetActive(false);
            hub.hubs[index+1].SetActive(true);
            yield break;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(timerRoot(root,index));
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

    public void WaterIconShowed()
    {
        if (questsCompleted == 2)
        {
            StartNewQuest();
            playerInfo.battery -= 33f;
        }
    }

    public void WaterCollected()
    {
        if (questsCompleted == 3)
            StartNewQuest();
    }

    
    public void CompleteQuest(int indexOfQuest)
    {
        if(questsCompleted == indexOfQuest)
            StartNewQuest();
    }

    public void SecondSectorCleared()
    {
        FindObjectOfType<SpawnEnemiesOnFirstRoot>().SpawnEnemies();
        FindObjectOfType<SpawnEnemiesOnSecondRoot>().SpawnEnemies();
    }
}