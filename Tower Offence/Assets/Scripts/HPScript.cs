using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPScript : MonoBehaviour
{
    public GameObject[] Hearts;
    GameManagerScript gameMS;
    int currentHealth;

    void Start()
    {
        currentHealth = 3;
        gameMS = FindObjectOfType<GameManagerScript>();
    }

    public void LooseHealth()
    {
        Hearts[--currentHealth].SetActive(false);
        if (currentHealth <= 0) 
            gameMS.ToMenu();
    }

    public void ResetHealth()
    {
        currentHealth = 3;
        foreach (var heart in Hearts)
            heart.SetActive(true);
    }
}
