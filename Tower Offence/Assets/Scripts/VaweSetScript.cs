using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class VaweSetScript : MonoBehaviour
{
    GameControllerScript gameCS;
    MonsterSpawnerScript spawner;
    public GameObject ItemPref;
    public Transform ItemGrid;

    void Start()
    {
        gameCS = FindObjectOfType<GameControllerScript>();
        spawner = FindObjectOfType<MonsterSpawnerScript>();

        foreach(var monster in gameCS.AllMonsterTypes)
        {
            var tmpItem = Instantiate(ItemPref);
            tmpItem.transform.SetParent(ItemGrid, false);
            tmpItem.GetComponent<ItemScript>().SetItem(monster);
        }
    }

    public void Delete()
    {
        spawner.vaweSetCreated = false;
        Destroy(gameObject);
    }

    public void StartVawe()
    {
        gameCS.DeadMonstersCount = 0;
        spawner.StartVawe();
    }
}
