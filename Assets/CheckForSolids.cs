using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForSolids : MonoBehaviour
{
    void Start()
    {
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				Destroy(gameObject);
			}
		}
	}
}
