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
        if(MoneyManagerScript.Instance.MoneyCount >= selfMonster.Price)
        {
            gameCS.AllMonsters.Add(new Monster(selfMonster));
            MoneyManagerScript.Instance.MoneyCount -= selfMonster.Price;
        }

    }
}
