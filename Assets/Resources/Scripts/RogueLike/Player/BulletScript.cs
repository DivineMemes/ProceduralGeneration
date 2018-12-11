using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float lifetime;
    private void Awake()
    {
        StartCoroutine(shotlife());
    }

    IEnumerator shotlife()
    {
        yield return new WaitForSeconds(lifetime);
        {
            Destroy(gameObject);
        }
    }
}
