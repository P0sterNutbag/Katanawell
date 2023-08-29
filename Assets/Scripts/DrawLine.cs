using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public Transform enemy;
    Vector3 origin;
    LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.startWidth = 0.08f;
        line.endWidth = 0.08f;
        origin = new Vector3(enemy.position.x, enemy.position.y+3);
    }

    void Update()
    {
        line.SetPosition(1, enemy.position);
        line.SetPosition(0, origin);
    }
}
