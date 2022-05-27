using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;
    public GameObject Menu;
    public int MoneyCount;
    public int VaweCount;
    public Text MoneyText;
    public Text VaweText;

    void Awake()
    {
        Instance = this;
        MoneyCount = 50;
        VaweCount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = MoneyCount.ToString();
        VaweText.text = VaweCount.ToString();

        if (VaweCount <= 0)
            ToMenu();
    }

    public void ToMenu()
    {
        Menu.SetActive(true);
    }

    public void PlayButton()
    {
        MoneyCount = 50;
        VaweCount = 10;
        FindObjectOfType<LevelManagerScript>().CreateLevel();

        Menu.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
