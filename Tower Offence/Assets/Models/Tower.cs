using UnityEngine;

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
