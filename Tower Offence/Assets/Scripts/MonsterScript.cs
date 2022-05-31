using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Timers;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{ 
    GameManagerScript MoneyMNGR;
    GameControllerScript gameCS;
    HPScript HP;
    List<GameObject> wayPoints = new List<GameObject>();
    float oneSpriteLength;
    float passedWay;
    float startMoney = 3.5f;
    int wayIndex;
    public Monster selfMonster;
    public float fullHP;
    public Color FullHPCol, NoHPCol;
    public Image HealthBar;
    

    void Start()
    {
        GetWayPoints();
        oneSpriteLength = wayPoints[0].GetComponent<SpriteRenderer>().bounds.size.x;
        gameCS = FindObjectOfType<GameControllerScript>();
        HP = FindObjectOfType<HPScript>();
        MoneyMNGR = GameManagerScript.Instance;
    }


    void Update()
    {
        Move();
        
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
            {
                HP.LooseHealth();
                if (selfMonster.Price == 500)
                    HP.LooseHealth();

                gameCS.DeadMonstersCount++;
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        selfMonster.Health -= damage;
        StartCoroutine(HealthBarUpdate(selfMonster.Health + damage));
        CheckAlive();
    }

    IEnumerator HealthBarUpdate(float oldHP)
    {
        oldHP--;
        HealthBar.fillAmount = oldHP / fullHP;
        HealthBar.color = Color.Lerp(NoHPCol, FullHPCol, oldHP / fullHP);
        if (oldHP <= selfMonster.Health) yield break;
        yield return new WaitForSeconds(.01f);
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
        if (selfMonster.Speed < .3f)
            selfMonster.Speed = .3f;
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
