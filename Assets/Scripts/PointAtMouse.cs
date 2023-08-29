using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtMouse : MonoBehaviour
{
    public bool alwaysPoint = false;

    private void OnEnable()
    {
        Point();
    }

    private void Update()
    {
        if (alwaysPoint)
        {
            Point();
        }
    }

    private void Point()
    {
        Vector3 aimDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (angle > 90 && angle < 270)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
