using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float damage = 10f;
    public GameObject bullet;
    public Transform firePoint;

    Vector2 aimdir;


    void Start()
    {
        
    }

    void Update()
    {
        // get bullet direction
        aimdir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aimdir = aimdir.normalized;

        // shoot bullet
        if (Input.GetButtonDown("Fire1"))
        {
            BulletMovement bulletMovement = Instantiate(bullet, firePoint.position, Quaternion.identity).GetComponent<BulletMovement>();
            bulletMovement.moveDirection = aimdir;
            bulletMovement.damage = damage;
        }
    }
}
