using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpiderController : MonoBehaviour
{
    enum States { inactive, move, fall };
    States state;

    public float speed = 100f;
    public int damage = 1;
    public int VerticalDir = 1;
    public float moveTimerMin = 1f;
    public float moveTimerMax = 5f;
    public float maxLineLength = 6f;
    public LineRenderer line;
    public GameObject dieExplosion;

    float moveTimer = 1f;
    Vector3 origin;
    Rigidbody2D rb;
    Transform target;
    GameObject block;
    EnemyShoot shooting;
    bool isQuitting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        origin = rb.position;
        target = GameObject.FindWithTag("Player").transform;
        shooting = GetComponent<EnemyShoot>();
        state = States.inactive;
        isQuitting = false;

        // get block
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y+3, transform.position.z), Vector2.up, 0.1f);
        if (hit.collider != null)
        {
            block = hit.collider.gameObject;
        }
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
                shooting.enabled = false;
                break;

            case States.move:
                shooting.enabled = true;

                // change move direction
                moveTimer -= Time.deltaTime;
                if (moveTimer <= 0)
                {
                    ChangeDirection();
                }

                float distance = Vector3.Distance(transform.position, origin);
                if (distance > maxLineLength)
                {
                    VerticalDir = 1;
                }

                // cut line
                Vector3 start = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                RaycastHit2D hit = Physics2D.Raycast(start, Vector2.up, distance+2);
                if (hit.collider != null)
                {
                    GameObject inst = hit.collider.transform.gameObject;
                    StompEnemy playerBox = inst.GetComponent<StompEnemy>();
                    if (playerBox != null)
                    {
                        PlayerMovement player = target.gameObject.GetComponent<PlayerMovement>();
                        if (player.state == PlayerMovement.States.slice)
                        {
                            state = States.fall;
                            rb.gravityScale = 1;
                            line.enabled = false;
                        }
                    }
                }

                if (block == null)
                {
                    state = States.fall;
                    rb.gravityScale = 3;
                    line.enabled = false;
                }
                break;

            case States.fall:
                   
                break;
        }
    }

    void FixedUpdate()
    {
        if (state == States.move)
        {
            rb.velocity = new Vector3(0f, speed * VerticalDir * Time.deltaTime);
        }
    }

    void ChangeDirection()
    {
        moveTimer = Random.Range(moveTimerMin, moveTimerMax);
        VerticalDir = Random.Range(-1, 1);
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

    public void Explode()
    {
        Instantiate(dieExplosion, transform.position, Quaternion.identity);
    }
}
