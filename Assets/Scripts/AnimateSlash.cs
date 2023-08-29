using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateSlash : MonoBehaviour
{
    public GameObject katanaSlash;
    public PlayerMovement player;
    public Animator animator;
    public GameObject afterimage;
    public float afterimageTimerMax = 0.025f;
    float afterimageTimer = 0f;

    bool isSlashing;

    // Update is called once per frame
    void Update()
    {
        // set direction
        if (player.horizontalMove > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (player.horizontalMove < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // activate slash
        isSlashing = animator.GetBool("Slashing");
        if (isSlashing)
        {
            if (!katanaSlash.activeSelf)
            {
                katanaSlash.SetActive(true);
            }
        } 
        else
        {
            katanaSlash.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        // afterimage
        if (player.state == PlayerMovement.States.slice)
        {
            if (afterimageTimer <= 0)
            {
                GameObject inst = Instantiate(afterimage, transform.position, Quaternion.identity);
                inst.transform.localScale = transform.localScale;
                afterimageTimer = afterimageTimerMax;
            }
            else
            {
                afterimageTimer -= Time.fixedDeltaTime;
            }
        }
    }
}
