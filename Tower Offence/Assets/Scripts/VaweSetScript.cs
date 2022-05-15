using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class VaweSetScript : MonoBehaviour
{
    GameControllerScript gameCS;
    public GameObject ItemPref;
    public Transform ItemGrid;
    // Start is called before the first frame update
    void Start()
    {
        gameCS = FindObjectOfType<GameControllerScript>();

        foreach(var monster in gameCS.AllMonsters)
        {
            var tmpItem = Instantiate(ItemPref);
            tmpItem.transform.SetParent(ItemGrid, false);
            tmpItem.GetComponent<ItemScript>().SetItem(monster);
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public void StartVawe()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
