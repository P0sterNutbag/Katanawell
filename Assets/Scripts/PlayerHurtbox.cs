using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtbox : MonoBehaviour
{
    public PlayerHealth player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!player.invincible)
        {
            GameObject collisionObj = collision.transform.gameObject;
            if (collisionObj.layer == LayerMask.NameToLayer("Enemy Bullet"))
            {
                EnemyBulletMovement enemy = collisionObj.GetComponent<EnemyBulletMovement>();
                player.TakeDamage(enemy.damage);
                Destroy(collisionObj);
            }
            else if (collisionObj.layer == LayerMask.NameToLayer("Hitbox"))
            {
                EnemyHitbox enemy = collisionObj.GetComponent<EnemyHitbox>();
                player.TakeDamage(enemy.damage);
            }
        }
    }
}
