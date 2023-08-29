using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWormController : MonoBehaviour
{
    enum States { inactive, move };
    States state;

    public float speed = 100f;
    public int horizontalDir = 1;
    public float moveTimerMin = 1f;
    public float moveTimerMax = 5f;
    public float maxLineLength = 6f;
    public GameObject dieExplosion;

    float moveTimer = 1f;
    Vector3 origin;
    Rigidbody2D rb;
    Transform target;
    GameObject block;
    bool isQuitting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        origin = rb.position;
        state = States.inactive;
        isQuitting = false;
    }

    private void Update()
    {
        // flip sprite
        transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x), 1, 1);
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
                // change move direction
                moveTimer -= Time.deltaTime;
                if (moveTimer <= 0)
                {
                    ChangeDirection();
                }

                float distance = Vector3.Distance(transform.position, origin);
                if (distance > maxLineLength)
                {
                    horizontalDir *= -1;
                }
                break;
        }
    }

    void FixedUpdate()
    {
        if (state == States.move)
        {
            rb.velocity = new Vector3(speed * horizontalDir * Time.deltaTime, rb.velocity.y);
        }
    }

    void ChangeDirection()
    {
        moveTimer = Random.Range(moveTimerMin, moveTimerMax);
        horizontalDir = Random.Range(-1, 1);
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
