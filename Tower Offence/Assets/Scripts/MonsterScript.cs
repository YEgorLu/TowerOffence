using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    List<GameObject> wayPoints = new List<GameObject>();
    int wayIndex;
    int speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        wayPoints = GameObject.Find("Main Camera").GetComponent<GameControllerScript>().wayPoints;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 dir = wayPoints[wayIndex].transform.position - transform.position;

        transform.Translate(dir.normalized * Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, wayPoints[wayIndex].transform.position) < 0.1f)
        {
            if (wayIndex < wayPoints.Count - 1)
                wayIndex++;
            else
                Destroy(gameObject);
        }
    }
}
