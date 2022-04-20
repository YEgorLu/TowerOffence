using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerScript : MonoBehaviour
{
    public LevelManagerScript LvlMngrScr;
    public float timeToSpawn = 3;
    public GameObject monsterPrefab;
    int spawnCount;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timeToSpawn <= 0)
        {
            StartCoroutine(SpawnMonster(spawnCount + 1));
            timeToSpawn = 3;
        }

        timeToSpawn -= Time.deltaTime;
    }

    IEnumerator SpawnMonster(int monsterCount)
    {
        spawnCount++;

        for (var i = 0; i < monsterCount; i++)
        {
            var tmpEnemy = Instantiate(monsterPrefab);
            tmpEnemy.transform.SetParent(gameObject.transform, false);
            var startCellPosition = LvlMngrScr.wayPoints[0].transform;
            var startPos = new Vector3(startCellPosition.position.x - startCellPosition.GetComponent<SpriteRenderer>().bounds.size.x / 2,
                                        startCellPosition.position.y - startCellPosition.GetComponent<SpriteRenderer>().bounds.size.y / 2);

            tmpEnemy.transform.position = startPos;

            yield return new WaitForSeconds(0.2f);
        } 
    }
}
