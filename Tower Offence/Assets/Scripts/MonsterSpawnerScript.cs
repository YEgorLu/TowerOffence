using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerScript : MonoBehaviour
{
    public LevelManagerScript LvlMngrScr;
    GameControllerScript gameCS;
    public float timeToSpawn = 5;
    public GameObject monsterPrefab;
    public GameObject vaweSetPrefab;
    public bool vaweSetCreated;
    int spawnCount;

    // Start is called before the first frame update
    void Start()
    {
        gameCS = FindObjectOfType<GameControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!vaweSetCreated && gameCS.DeadMonstersCount >= gameCS.AllMonsters.Count)
            CreateVaweSet();
    }

    private void CreateVaweSet()
    {
        GameObject tempSet = Instantiate(vaweSetPrefab);
        tempSet.transform.SetParent(GameObject.Find("Canvas").transform, false);
        vaweSetCreated = true;
    }

    public void StartVawe()
    {
        spawnCount = gameCS.AllMonsters.Count;
        StartCoroutine(SpawnMonster(spawnCount));
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
