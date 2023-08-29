using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject dieExplosion;
    float health = 1f;

    void Update()
    {
        
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Instantiate(dieExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
