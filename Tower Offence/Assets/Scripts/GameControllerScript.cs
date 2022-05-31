using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public Tower[] AllTowers = new Tower[5];
    public TowerProjectile[] AllProjectiles = new TowerProjectile[5];
    public List<Monster> AllMonsters = new List<Monster>();
    public List<Monster> AllMonsterTypes = new List<Monster>();
    public int DeadMonstersCount;

    private void Awake()
    {
        AllTowers[2] = new Tower(2, 5, .5f, @"TowerSprites/turret_red_double");
        AllTowers[3] = new Tower(3, 6.5f, .8f, @"TowerSprites/turret_purple_single");
        AllTowers[4] = new Tower(4, 5, 1f, @"TowerSprites/turret_red_beam");

        AllProjectiles[2] = new TowerProjectile(10, 7);
        AllProjectiles[3] = new TowerProjectile(10, 3);
        AllProjectiles[4] = new TowerProjectile(7, 10);

        AllMonsterTypes.Add(new Monster(20, 2, 10, @"MonsterSprites/Monster4"));
        AllMonsterTypes.Add(new Monster(30, 1.2f, 20, @"MonsterSprites/Monster1"));
        AllMonsterTypes.Add(new Monster(15, 5, 15, @"MonsterSprites/Monster2"));
        AllMonsterTypes.Add(new Monster(600, .8f, 500, @"MonsterSprites/Monster3"));
    }
}
