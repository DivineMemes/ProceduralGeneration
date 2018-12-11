using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Vector3 ScreenPoint;
    public GameObject projectile;
    Rigidbody rb;
    Vector3 forwardDir;
    Vector3 rightDir;
    Transform myCamera;
    Camera cam;
    bool timer = false;
    public float shotSpeed;
    public float cooldown;
    public float camSpeed;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        myCamera = GetComponentInChildren<Camera>().transform;
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
        ScreenPoint = Input.mousePosition;
        ScreenPoint.z = 10.0f;
        Vector3 pPoint = /*transform.position = */cam.ScreenToWorldPoint(ScreenPoint);
        //Transform transformPpoint = new Vector3(pPoint.x, pPoint.y, pPoint.z);
        if (Input.GetButton("Fire1") && !timer)
        {
            timer = true;
            GameObject spawnedProjectile = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
            spawnedProjectile.transform.LookAt(pPoint);
            
            spawnedProjectile.GetComponent<Rigidbody>().AddForce(spawnedProjectile.transform.forward * shotSpeed);
            //spawnedProjectile.GetComponent<Rigidbody>().AddForce(heading, ForceMode.Impulse);
            StartCoroutine(shotCooldown());
        }

        float horz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        forwardDir = gameObject.transform.forward * vert;
        rightDir = myCamera.transform.right * horz;
    }

    IEnumerator shotCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        {
            timer = false;
        }
    }
}
