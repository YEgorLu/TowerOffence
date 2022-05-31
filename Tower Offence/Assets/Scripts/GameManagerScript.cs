using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;
    public LevelManagerScript LvlMS;
    public MonsterSpawnerScript Spawner;
    public GameObject MainMenu;
    public GameObject LevelSelector;
    public GameObject Menu;
    public int MoneyCount;
    public int VaweCount;
    public Text MoneyText;
    public Text VaweText;

    void Awake()
    {
        Instance = this;
        MoneyCount = 100;
        VaweCount = 15;
        LevelSelector.SetActive(false);
        LvlMS = FindObjectOfType<LevelManagerScript>();
        Spawner = FindObjectOfType<MonsterSpawnerScript>();
    }

    void Update()
    {
        MoneyText.text = MoneyCount.ToString();
        VaweText.text = VaweCount.ToString();

        if (VaweCount == 0 && Spawner.gameObject.GetComponentInChildren<MonsterScript>() == null)
        {
            ToMenu();
            VaweCount--;
        }
    }

    void EndGame()
    {
        Spawner.DestroyAllMonsters();
        LvlMS.DeletePreviousLevel();
    }

    public void ToMenu()
    {
        EndGame();
        Menu.SetActive(true);
        MainMenu.SetActive(true);
    }

    public void PlayButton()
    {
        MainMenu.SetActive(false);
        LevelSelector.SetActive(true);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void LevelButton(int mapNumber)
    {
        MoneyCount = 100;
        VaweCount = 15;
        
        FindObjectOfType<LevelManagerScript>().CreateLevel(mapNumber);
        FindObjectOfType<GameControllerScript>().AllMonsters.Clear();
        FindObjectOfType<HPScript>().ResetHealth();
        LevelSelector.SetActive(false);
        Menu.SetActive(false);
    }
}
