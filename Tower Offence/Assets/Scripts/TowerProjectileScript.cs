using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectileScript : MonoBehaviour
{
    Transform target;
    float speed = 7;
    public int damage = 10;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetTarget(Transform monster)
    {
        target = monster;
    }

    private void Move()
    {
        if (target != null)
        {
            if (Vector2.Distance(target.position, transform.position) <= 0.1f)
            {
                target.GetComponent<MonsterScript>().TakeDamage(damage);
                Destroy(gameObject);
            }
            else
            {
                var dir = target.position - transform.position;
                transform.Translate(dir.normalized * Time.deltaTime * speed);
            }
        }
        else
            Destroy(gameObject);
    }
}
