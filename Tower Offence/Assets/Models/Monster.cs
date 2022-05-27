using UnityEngine;

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
