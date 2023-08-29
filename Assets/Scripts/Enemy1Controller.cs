using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    enum States { inactive, move };
    States state;

    public float speed;
    public float rotationSpeed = 1f;
    public int damage = 1;
    public float bounceForce = 150;
    public float movementSmoothing = 0.01f;
    public GameObject dieExplosion;

    Transform target;
    Rigidbody2D rb;
    bool isQuitting = false;

    void Start()
    {
        state = States.inactive;
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        isQuitting = false;
    }

    private void Update()
    {
        switch (state)
        {
            case States.inactive:
                float targetDis = Vector3.Distance(target.position, transform.position);
                if (targetDis < 16 && GameController.startTimer)
                {
                    state = States.move;
                }
                break;

            case States.move:
                transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x), 1, 1);
                break;
        }
    }

    void FixedUpdate()
    {
        Vector3 targetDir = target.position - transform.position;
        Vector3 targetVelocity = targetDir.normalized * speed * Time.deltaTime;
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, movementSmoothing);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Vector2 bounceDir = transform.position - collision.transform.position;
            Vector2 bounceVelocity = bounceDir.normalized * bounceForce * Time.deltaTime;
            rb.velocity = bounceVelocity;
        }
    }

    public void Explode()
    {
        Instantiate(dieExplosion, transform.position, Quaternion.identity);
    }

    /*void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!isQuitting)
        {
            Instantiate(dieExplosion, transform.position, Quaternion.identity);
        }
    }*/
}
