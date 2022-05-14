using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    public GameObject TowerProjectile;
    GameControllerScript gameCS;
    Tower selfTower;
    public TowerType selfType;
    bool CanShoot()
    {
        return selfTower.CurrCooldown <= 0;
    }

    void SearchTarget()
    {
        if (!CanShoot()) 
            return;

        Transform nearestMonster = null;
        var nearestMonsterDistance = Mathf.Infinity;
        
        foreach(var monster in GameObject.FindGameObjectsWithTag("Monster"))
        {
            var currDistance = Vector2.Distance(transform.position, monster.transform.position);
            if (currDistance < nearestMonsterDistance && currDistance <= selfTower.Range)
            {
                nearestMonster = monster.transform;
                nearestMonsterDistance = currDistance;
            }
        }

        if (nearestMonster != null)
            Shoot(nearestMonster);
    }

    void Shoot(Transform monster)
        {
        selfTower.CurrCooldown = selfTower.Cooldown;

        var proj = Instantiate(TowerProjectile);
        proj.GetComponent<TowerProjectileScript>().selfProjectile = gameCS.AllProjectiles[(int)selfType];
        proj.GetComponent<TowerProjectileScript>().selfTower = selfTower;
        proj.transform.position = transform.position;
        proj.GetComponent<TowerProjectileScript>().SetTarget(monster);
    }

    void Start()
    {
        gameCS = FindObjectOfType<GameControllerScript>();
        selfTower = new Tower(gameCS.AllTowers[(int)selfType]);

        InvokeRepeating("SearchTarget", 0, .1f);
    }


    void Update()
    {
        /*if (CanShoot())
            SearchTarget();*/

        if (selfTower.CurrCooldown > 0)
            selfTower.CurrCooldown -= Time.deltaTime;
    }
}
