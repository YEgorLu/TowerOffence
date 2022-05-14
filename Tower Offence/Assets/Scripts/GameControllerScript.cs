using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public List<Tower> AllTowers = new List<Tower>();
    public List<TowerProjectile> AllProjectiles = new List<TowerProjectile>();
    public List<Monster> AllMonsters = new List<Monster>();

    private void Awake()
    {
        AllTowers.Add(new Tower(0, 10, .8f));
        AllTowers.Add(new Tower(1, 5, .3f));

        AllProjectiles.Add(new TowerProjectile(10, 5));
        AllProjectiles.Add(new TowerProjectile(7, 10));

        AllMonsters.Add(new Monster(15, 2));
        AllMonsters.Add(new Monster(10, 5));
    }
}

public class Tower
{
    public float Range, Cooldown, CurrCooldown = 0;
    public int Type;
    public Tower(int type, float range, float cooldown)
    {
        Type = type;
        Range = range;
        Cooldown = cooldown;
    }
    public Tower(Tower other)
    {
        Type = other.Type;
        Range = other.Range;
        Cooldown = other.Cooldown;
    }
}

public class TowerProjectile
{
    public float Speed;
    public int Damage;
    public Sprite Spr;

    public TowerProjectile(float speed, int dmg)
    {
        Speed = speed;
        Damage = dmg;
    }
    public TowerProjectile(TowerProjectile other)
    {
        Speed = other.Speed;
        Damage = other.Damage;
    }

}

public class Monster
{
    public float Health, Speed, StartSpeed;
    public Sprite Spr;

    public Monster(float health, float speed)
    {
        Health = health;
        StartSpeed = Speed = speed;
    }

    public Monster(Monster other)
    {
        Health = other.Health;
        StartSpeed = Speed = other.StartSpeed;
    }
}

public enum TowerType
{
    SlowTower,
    AOETower
}
