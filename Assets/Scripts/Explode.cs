using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    bool isQuitting = false;
    public GameObject dieExplosion;

    /*private void Start()
    {
        isQuitting = false;
    }

    void OnApplicationQuit()
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

    public void BlowUp()
    {
        Instantiate(dieExplosion, transform.position, Quaternion.identity);
    }
}
