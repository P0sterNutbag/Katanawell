using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum States { walk, wallgrab, ladder, slice, dead };
    public States state;

    public float runSpeed = 40f;
    public float jumpForce = 120f;
    public float stompJumpForce = 60f;
    public bool airControl = true;
    public float airSpeed = 0.15f;
    public float wallJumpSpeed = 80f;
    public float ladderSpeed = 40f;
    public float sliceSpeed = 100f;
    public float sliceDis = 5f;
    public float slashesMax = 3f;
    public Animator animator;
    public AudioSource swoosh;
    public AudioSource jump;
    public GameObject aimline;
    public GameObject slash;

    [HideInInspector] public float horizontalMove = 0f;
    float verticalMove = 0f;
    float xInput = 0f;
    float yInput = 0f;
    float wallGrab = 0;
    float grav;
    [HideInInspector] public float slashesLeft;
    float ladderX = 0f;
    bool onLadder = false;
    bool keySlice;
    Vector2 aimDir;
    Vector3 sliceDir;
    Vector3 sliceOrig;

    CharacterController2D controller;
    Rigidbody2D rb;
    BoxCollider2D myCollider;
    GameObject currentOneWayPlatform = null;
    PlayerHealth playerHealth;

    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        playerHealth = GetComponent<PlayerHealth>();
        grav = rb.gravityScale;
        state = States.walk;
        slashesLeft = slashesMax;
    }

    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        keySlice = Input.GetButtonDown("Fire1");

        switch (state)
        {
            case States.walk:
                if (controller.m_Grounded || airControl)
                {
                    horizontalMove = xInput * runSpeed;
                }
                else if (wallGrab == 0)
                {
                    horizontalMove += xInput * airSpeed;
                    horizontalMove = Mathf.Clamp(horizontalMove, -runSpeed, runSpeed);
                }

                if (Input.GetButtonDown("Jump"))
                {
                    if (controller.m_Grounded || wallGrab != 0)
                    {
                        verticalMove = jumpForce;
                        jump.Play();
                        if (wallGrab != 0)
                        {
                            horizontalMove = -Mathf.Sign(horizontalMove) * wallJumpSpeed;
                            transform.Translate(new Vector3(0.25f * Mathf.Sign(wallGrab), 0, 0));
                            wallGrab = 0;
                        }
                    }
                }

                // go through one way platform
                if (yInput < 0)
                {
                    if (currentOneWayPlatform != null)
                    {
                        StartCoroutine(DisableCollision());
                    }
                }

                // aim at Camera
                aimDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                aimDir = aimDir.normalized;

                // refill slashes
                if (controller.m_Grounded)
                {
                    slashesLeft = slashesMax;
                    //ammoHud.SetAmmo();
                }

                // switch to slice
                if (keySlice && slashesLeft > 0)
                {
                    sliceOrig = transform.position;
                    sliceDir = aimDir;
                    rb.gravityScale = 0;
                    slashesLeft--;
                    //ammoHud.SetAmmo();
                    state = States.slice;
                    // animation
                    animator.SetBool("Slashing", true);
                    // sound
                    swoosh.Play();
                }

                // switch to ladder
                if (onLadder && Input.GetButtonDown("Ladder"))
                {
                    rb.velocity = Vector2.zero;
                    rb.gravityScale = 0;
                    transform.Translate(new Vector3(ladderX - transform.position.x, 0, 0));
                    verticalMove = 0;
                    horizontalMove = 0;
                    state = States.ladder;
                }
                break;

            case States.slice:
                // move
                Vector3 sliceVector = sliceDir * sliceSpeed;
                horizontalMove = sliceVector.x;
                verticalMove = sliceVector.y;

                // exit slice
                float dis = Vector3.Distance(sliceOrig, transform.position);
                if (dis > sliceDis)
                {
                    rb.velocity = Vector3.zero;
                    verticalMove = Mathf.Clamp(verticalMove, -50f, 50f);
                    rb.gravityScale = grav;
                    state = States.walk;
                    // animation
                    animator.SetBool("Slashing", false);
                }
                break;

            case States.ladder:
                // move up and down ladder
                verticalMove = yInput * ladderSpeed;

                // get off ladder
                if (!onLadder || xInput != 0)
                {
                    rb.gravityScale = grav;
                    horizontalMove = xInput * runSpeed;
                    verticalMove = 0;
                    state = States.walk;
                }
                break;

            case States.dead:
                horizontalMove = 0;
                animator.SetBool("Dead", true);
                rb.gravityScale = grav;
                aimline.SetActive(false);
                slash.SetActive(false);
                break;
        }

        // animation
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetBool("Grounded", controller.m_Grounded);
    }

    public void Stomp()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0);
        verticalMove = stompJumpForce;
        slashesLeft = slashesMax;
        //ammoHud.SetAmmo();
    }

    void FixedUpdate()
    {
        // grab wall
        if (state == States.walk)
        {
            if (wallGrab != 0)
            {
                rb.velocity = Vector2.zero;
                rb.gravityScale = 0;
            }
            else
            {
                rb.gravityScale = grav;
            }
        }

        // move character
        controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime);
        verticalMove = 0;
        wallGrab = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObj = collision.transform.gameObject;
        if (collisionObj.layer == LayerMask.NameToLayer("Ground"))
        {
            // Stop falling when you grab a wall
            Vector2 direction = collision.GetContact(0).normal;
            if (!controller.m_Grounded && direction.x != 0 && direction.y == 0)
            {
                horizontalMove = 0;
                rb.velocity = Vector2.zero;
            }

            // identify one way platform
            if (collisionObj.CompareTag("One Way"))
            {
                currentOneWayPlatform = collisionObj;
            }

            // stop when you slice a wall
            if (state == States.slice && !collisionObj.CompareTag("Brick"))
            {
                state = States.walk;
                // animation
                animator.SetBool("Slashing", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObj = collision.transform.gameObject;
        if (collisionObj.CompareTag("Ladder"))
        {
            onLadder = true;
            ladderX = collisionObj.transform.position.x;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collisionObj = collision.transform.gameObject;
        if (collisionObj.CompareTag("Ladder"))
        {
            onLadder = false;
        }
    }

/*    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject collisionObj = collision.transform.gameObject;
        if (collisionObj.layer == LayerMask.NameToLayer("Ground"))
        {
            // grab or let go of wall
            Vector2 direction = collision.GetContact(0).normal;
            if (!controller.m_Grounded && direction.x == -xInput && direction.y == 0)
            {
                wallGrab = direction.x;
            } 
            else
            {
                wallGrab = 0;
            }
        }
    }*/

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject collisionObj = collision.transform.gameObject;
        if (collisionObj.layer == LayerMask.NameToLayer("Ground"))
        {
            wallGrab = 0;

            // identify one way platform
            if (collisionObj.CompareTag("One Way"))
            {
                currentOneWayPlatform = null;
            }
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(myCollider, platformCollider);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(myCollider, platformCollider, false);
    }

}
