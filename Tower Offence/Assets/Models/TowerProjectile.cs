using UnityEngine;

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
