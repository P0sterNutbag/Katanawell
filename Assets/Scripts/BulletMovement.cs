using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float moveSpeed = 100f;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public Vector2 moveDirection;

    Rigidbody2D rb;

    void Start()
    {
        //Vector2 moveDirNorm = moveDirection.normalized;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = moveDirection * moveSpeed;

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.transform.gameObject;
        if (collisionObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        } 
        else if (collisionObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collisionObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
