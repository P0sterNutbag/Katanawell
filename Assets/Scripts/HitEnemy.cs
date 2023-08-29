using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    public PlayerMovement player;
    public GameController control;
    public BoxCollider2D hitbox;
    public PlayerScore score;
    public AudioSource kill;
    public AudioSource kill2;

    private void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (player.state == PlayerMovement.States.slice)
        {
            hitbox.enabled = true;
        }
        else
        {
            hitbox.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObj = collision.transform.gameObject;
        if (collisionObj.layer == LayerMask.NameToLayer("Enemy")) {
            //StartCoroutine(control.FreezeTime(0.05f));
            //Destroy(collisionObj);
            EnemyHealth enemy = collisionObj.GetComponent<EnemyHealth>();
            enemy.TakeDamage(1);
            score.IncreaseScore();
            kill.Play();
            kill2.Play();
            GameController.kills++;
        }
        else if (collisionObj.CompareTag("Brick"))
        {
            Explode brick = collisionObj.GetComponent<Explode>();
            brick.BlowUp();
            Destroy(collisionObj);
        }
    }
}
