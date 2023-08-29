using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFade : MonoBehaviour
{
    public float alphaMinus = 0.1f;

    SpriteRenderer sprite;
    float alpha = 1f;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
        alpha -= alphaMinus * Time.deltaTime;

        if (alpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}
