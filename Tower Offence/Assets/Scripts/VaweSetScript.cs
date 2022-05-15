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
    // Start is called before the first frame update
    void Start()
    {
        gameCS = FindObjectOfType<GameControllerScript>();
        spawner = FindObjectOfType<MonsterSpawnerScript>();

        foreach(var monster in gameCS.AllMonsters)
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

    // Update is called once per frame
    void Update()
    {
        /*if(gameCS.DeadMonstersCount == gameCS.AllMonsters.Count)
        {
            foreach (var monster in gameCS.AllMonsters)
            {
                var tmpItem = Instantiate(ItemPref);
                tmpItem.transform.SetParent(ItemGrid, false);
                tmpItem.GetComponent<ItemScript>().SetItem(monster);
            }
        }*/
    }
}
