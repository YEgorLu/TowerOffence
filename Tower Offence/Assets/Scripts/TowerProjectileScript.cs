using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectileScript : MonoBehaviour
{
    Transform target;
    GameControllerScript gameCS;
    public TowerProjectile selfProjectile;
    public Tower selfTower;

    void Start()
    {
        gameCS = FindObjectOfType<GameControllerScript>();
        selfProjectile = gameCS.AllProjectiles[selfTower.Type];
    }

    void Update()
    {
        Move();
    }

    public void SetTarget(Transform monster)
    {
        target = monster;
    }

    private void Move()
    {
        if (target != null)
        {
            if (Vector2.Distance(target.position, transform.position) <= 0.1f)
            {
                Hit();
            }
            else
            {
                var dir = target.position - transform.position;
                transform.Translate(dir.normalized * Time.deltaTime * selfProjectile.Speed);
            }
        }
        else
            Destroy(gameObject);
    }

    private void Hit()
    {
        var targetScript = target.GetComponent<MonsterScript>();
        switch (selfTower.Type)
        {
            case (int)TowerType.SlowTower:
                targetScript.StartSlow(3, 1);
                targetScript.TakeDamage(selfProjectile.Damage);
                break;
            case (int)TowerType.AOETower:
                targetScript.AOEDamage(1.5f, selfProjectile.Damage);
                break;
            case (int)TowerType.StandartTower:
                targetScript.TakeDamage(selfProjectile.Damage);
                break;
        }
        Destroy(gameObject);
    }
}
