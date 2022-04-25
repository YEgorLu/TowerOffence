using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    float range = 2;
    public float CurrCooldown, Cooldown;

    public GameObject TowerProjectile;

    bool CanShoot()
    {
        return CurrCooldown <= 0;
    }

    void SearchTarget()
    {
        Transform nearestMonster = null;
        var nearestMonsterDistance = Mathf.Infinity;
        
        foreach(var monster in GameObject.FindGameObjectsWithTag("Monster"))
        {
            var currDistance = Vector2.Distance(transform.position, monster.transform.position);
            if (currDistance < nearestMonsterDistance && currDistance <= range)
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
        CurrCooldown = Cooldown;

        var proj = Instantiate(TowerProjectile);
        proj.transform.position = transform.position;
        proj.GetComponent<TowerProjectileScript>().SetTarget(monster);
    }

    void Start()
    {
        
    }


    void Update()
    {
        if (CanShoot())
            SearchTarget();

        if (CurrCooldown > 0)
            CurrCooldown -= Time.deltaTime;
    }
}
