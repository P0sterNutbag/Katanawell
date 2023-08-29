using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour
{
    public PlayerMovement player;
    public GameController control;
    public PlayerScore score;
    public AudioSource kill;
    public AudioSource kill2;
    BoxCollider2D hitbox;

    private void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObj = collision.transform.gameObject;
        Vector2 direction = collision.GetContact(0).normal;
        if (direction.y == 1)
        {
            Destroy(collisionObj);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObj = collision.transform.gameObject;
        if (collisionObj.CompareTag("Stompable"))
        {
            var collisionPoint = collision.ClosestPoint(transform.position);
            var collisionNormal = new Vector2(transform.position.x, transform.position.y) - collisionPoint;
            if (collisionNormal.y > 0)
            {
                //Destroy(collisionObj);
                EnemyHealth enemy = collisionObj.GetComponent<EnemyHealth>();
                enemy.TakeDamage(1);
                player.Stomp();
                score.IncreaseScore();
                kill.Play();
                kill2.Play();
                GameController.kills++;
            }
        }
    }
}
