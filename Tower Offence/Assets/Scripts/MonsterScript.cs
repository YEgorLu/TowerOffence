using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    List<GameObject> wayPoints = new List<GameObject>();

    int wayIndex;
    int speed = 3;


    void Start()
    {
        GetWayPoints();
    }


    void Update()
    {
        Move();
    }

    private void GetWayPoints()
    {
        wayPoints = GameObject.Find("LevelGroup").GetComponent<LevelManagerScript>().wayPoints;
    }

    private void Move()
    {
        var currentWayPoint = wayPoints[wayIndex].transform;
        var currentWayPosition = new Vector3(currentWayPoint.position.x + currentWayPoint.GetComponent<SpriteRenderer>().bounds.size.x / 2, currentWayPoint.position.y - currentWayPoint.GetComponent<SpriteRenderer>().bounds.size.y / 2);

        var dir = currentWayPosition - transform.position;

        transform.Translate(dir.normalized * Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, currentWayPosition) < 0.1f)
        {
            if (wayIndex < wayPoints.Count - 1)
                wayIndex++;
            else
                Destroy(gameObject);
        }
    }
}
