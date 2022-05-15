using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerScript : MonoBehaviour
{
    public LevelManagerScript LvlMngrScr;
    GameControllerScript gameCS;
    public float timeToSpawn = 5;
    public GameObject monsterPrefab;
    int spawnCount = 2;


    // Start is called before the first frame update
    void Start()
    {
        gameCS = FindObjectOfType<GameControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnCount = gameCS.AllMonsters.Count;
        if (timeToSpawn <= 0)
        {
            StartCoroutine(SpawnMonster(spawnCount));
            timeToSpawn = 3;
        }

        timeToSpawn -= Time.deltaTime;
    }

    IEnumerator SpawnMonster(int monsterCount)
    {
        for (var i = 0; i < monsterCount; i++)
        {
            var tmpMonster = Instantiate(monsterPrefab);
            tmpMonster.transform.SetParent(gameObject.transform, false);
            tmpMonster.GetComponent<MonsterScript>().selfMonster = new Monster(gameCS.AllMonsters[Random.Range(0, gameCS.AllMonsters.Count)]);
            var startCellPosition = LvlMngrScr.wayPoints[0].transform;
            var startPos = new Vector3(startCellPosition.position.x - startCellPosition.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                        startCellPosition.position.y - startCellPosition.GetComponent<SpriteRenderer>().bounds.size.y / 2);

            tmpMonster.transform.position = startPos;

            yield return new WaitForSeconds(0.1f);
        } 
    }
}
