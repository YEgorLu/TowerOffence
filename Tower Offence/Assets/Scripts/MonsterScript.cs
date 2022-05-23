using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    GameManagerScript MoneyMNGR;
    GameControllerScript gameCS;
    List<GameObject> wayPoints = new List<GameObject>();
    public Monster selfMonster;
    float oneSpriteLength;
    float passedWay;
    /*Timer aliveTimer = new Timer();*/
    float startMoney = 2.5f;


    public float currHealth;
    int wayIndex;

    void Start()
    {
        //GetComponent<SpriteRenderer>().sprite = selfMonster.Spr;
        GetWayPoints();
        oneSpriteLength = wayPoints[0].GetComponent<SpriteRenderer>().bounds.size.x;
        gameCS = FindObjectOfType<GameControllerScript>();
        MoneyMNGR = GameManagerScript.Instance;
        //aliveTimer.Start();
    }


    void Update()
    {
        Move();
        currHealth = selfMonster.Health;
    }

    private void GetWayPoints()
    {
        wayPoints = GameObject.Find("LevelGroup").GetComponent<LevelManagerScript>().wayPoints;
    }

    private void Move()
    {
        var currentWayPoint = wayPoints[wayIndex].transform;
        var currentWayPosition = new Vector3(currentWayPoint.position.x + oneSpriteLength / 2, currentWayPoint.position.y - oneSpriteLength / 2);

        var dir = currentWayPosition - transform.position;

        transform.Translate(dir.normalized * Time.deltaTime * selfMonster.Speed);
        passedWay += Time.deltaTime * selfMonster.Speed;

        if (Vector3.Distance(transform.position, currentWayPosition) < 0.1f)
        {
            if (wayIndex < wayPoints.Count - 1)
                wayIndex++;
            else
                Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        selfMonster.Health -= damage;
        CheckAlive();
    }

    void CheckAlive()
    {
        if (selfMonster.Health <= 0)
        {
            var moneyToGive = (int)(startMoney * passedWay / oneSpriteLength);
            MoneyMNGR.MoneyCount += moneyToGive;
            gameCS.DeadMonstersCount++;
            Destroy(gameObject);
        }
    }

    public void StartSlow(float duration, float slowValue)
    {
        selfMonster.Speed = selfMonster.StartSpeed;
        StopCoroutine("GetSlow");
        StartCoroutine(GetSlow(duration, slowValue));
    }

    IEnumerator GetSlow(float duration, float slowValue)
    {
        selfMonster.Speed -= slowValue;
        yield return new WaitForSeconds(duration);
        selfMonster.Speed = selfMonster.StartSpeed;
    }

    public void AOEDamage(float range, float dmg)
    {
        var monsters = new List<MonsterScript>();
        foreach (var monster in GameObject.FindGameObjectsWithTag("Monster"))
            if (Vector2.Distance(transform.position, monster.transform.position) <= range)
                monsters.Add(monster.GetComponent<MonsterScript>());

        foreach (var monster in monsters)
            monster.TakeDamage(dmg);
    }
}
