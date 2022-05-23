using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemScript : MonoBehaviour, IPointerClickHandler
{
    Monster selfMonster;
    GameControllerScript gameCS;
    public Image MonsterImage;
    public Text MonsterPrice;
    public Text MonstersHaveText;
    public int MonstersHave;

    public void Start()
    {
        gameCS = FindObjectOfType<GameControllerScript>();
    }
    public void SetItem(Monster monster) 
    {
        selfMonster = monster;
        MonsterImage.sprite = monster.Spr;
        MonsterPrice.text = monster.Price.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(GameManagerScript.Instance.MoneyCount >= selfMonster.Price)
        {
            gameCS.AllMonsters.Add(new Monster(selfMonster));
            MonstersHave++;
            MonstersHaveText.text = MonstersHave.ToString();
            GameManagerScript.Instance.MoneyCount -= selfMonster.Price;
        }
    }
}
