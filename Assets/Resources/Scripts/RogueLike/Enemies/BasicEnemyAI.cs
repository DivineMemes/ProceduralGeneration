using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    public float currentHealth;
    float health = 20;
    public Transform Player;
    public float radius;
    public float recalc;
    public float wanderSpeed;
    public float chaseSpeed;
    public float detection;
    public bool timer = false;
    bool inRange;
    Vector3 wanderValue;
    float lastX;
    float lastZ;

    void Awake()
    {
        currentHealth = health;
        lastX = gameObject.transform.position.x;
        lastZ = gameObject.transform.position.z;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }

        if (!inRange)
        {
            if(!timer)
            {
                wanderValue = wanderPoints();
                StartCoroutine(newPos());
                timer = true;
            }
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, wanderValue, Time.deltaTime * wanderSpeed);
        }
        else
        {
            FollowPlayer();
        }

        if (Vector3.Distance(gameObject.transform.position, Player.position) <= detection)
        {
            lastX = gameObject.transform.position.x;
            lastZ = gameObject.transform.position.z;
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }
        


    public Vector3 wanderPoints()
    {
        Vector3 target;
        target = Vector3.zero;
        target.x = Random.insideUnitCircle.x * radius + lastX;
        target.z = Random.insideUnitCircle.y * radius + lastZ;
        return target;
    }

    void FollowPlayer()
    {   
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Player.position, Time.deltaTime * chaseSpeed);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Bullet"))
        {
            currentHealth -= 5;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator newPos()
    {
        yield return new WaitForSeconds(recalc);
        timer = false;
    }
}
