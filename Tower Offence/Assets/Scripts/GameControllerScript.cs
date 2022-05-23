using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public List<Tower> AllTowers = new List<Tower>();
    public List<TowerProjectile> AllProjectiles = new List<TowerProjectile>();
    public List<Monster> AllMonsters = new List<Monster>();
    public List<Monster> AllMonsterTypes = new List<Monster>();
    public int DeadMonstersCount;

    private void Awake()
    {
        AllTowers.Add(new Tower(0, 10, .8f, @"TowerSprites/turret_purple_single"));
        AllTowers.Add(new Tower(1, 5, .3f, @"TowerSprites/turret_red_single"));

        AllProjectiles.Add(new TowerProjectile(10, 5));
        AllProjectiles.Add(new TowerProjectile(7, 10));

        AllMonsterTypes.Add(new Monster(25, 2, 10, @"MonsterSprites/Monster1"));
        AllMonsterTypes.Add(new Monster(10, 5, 15, @"MonsterSprites/Monster2"));
    }
}

public class Tower
{
    public float Range, Cooldown, CurrCooldown = 0;
    public int Type;
    public Sprite Spr;
    public Tower(int type, float range, float cooldown, string path)
    {
        Type = type;
        Range = range;
        Cooldown = cooldown;
        Spr = Resources.Load<Sprite>(path);
    }
    public Tower(Tower other)
    {
        Type = other.Type;
        Range = other.Range;
        Cooldown = other.Cooldown;
        Spr = other.Spr;
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
    public int Price;

    public Monster(float health, float speed, int price, string path)
    {
        Health = health;
        StartSpeed = Speed = speed;
        Price = price;
        Spr = Resources.Load<Sprite>(path);
    }

    public Monster(Monster other)
    {
        Health = other.Health;
        StartSpeed = Speed = other.StartSpeed;
        Price = other.Price;
        Spr = other.Spr;
    }
}

public enum TowerType
{
    SlowTower,
    AOETower
}
