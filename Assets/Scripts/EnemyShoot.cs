using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int damage = 1;
    public float timer = 1f;
    public float bulletSpeed = 200f;
    public Transform target;
    public Transform firePoint;

    Vector3 aimDir;

    void Start()
    {
        InvokeRepeating("Shoot", timer, timer);
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        aimDir = (target.position - firePoint.position).normalized;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        EnemyBulletMovement bulletMovement = bullet.GetComponent<EnemyBulletMovement>();
        bulletMovement.moveVector = aimDir * bulletSpeed;
        bulletMovement.damage = damage;
    }
}
