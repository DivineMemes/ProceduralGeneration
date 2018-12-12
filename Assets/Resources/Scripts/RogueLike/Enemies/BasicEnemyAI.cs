using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    public float radius;
    public float recalc;
    public float wanderSpeed;
    public bool timer = false;
    Vector3 wanderValue;
    void Update()
    {
        if(!timer)
        {
            wanderValue = wanderPoints();
            StartCoroutine(newPos());
            timer = true;
        }
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, wanderValue, Time.deltaTime * wanderSpeed);
    }


    public Vector3 wanderPoints()
    {
        Vector3 target;
        target = Vector3.zero;
        target.x = Random.insideUnitCircle.x * radius;
        target.z = Random.insideUnitCircle.y * radius;
        return target;
    }

    IEnumerator newPos()
    {
        yield return new WaitForSeconds(recalc);
        timer = false;
    }
}
