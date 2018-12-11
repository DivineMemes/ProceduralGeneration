using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Rigidbody rb;
    Vector3 forwardDir;
    Vector3 rightDir;
    Transform Camera;
    public float camSpeed;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Camera = GetComponentInChildren<Camera>().transform;
    }


    void FixedUpdate()
    {

        rb.MovePosition(transform.position + (forwardDir + rightDir) * speed);

        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.down * camSpeed);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * camSpeed);
        }


    }
    void Update()
    {
        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        forwardDir = gameObject.transform.forward * vert;
        rightDir = Camera.transform.right * horz;
    }
}
