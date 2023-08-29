using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] chunks;
    public int spawnIntervalMin = 8;
    public int spawnIntervalMax = 24;
    public int levelHeight = 64;
    public int xScale = 1;

    void Start()
    {
        int interval = 0;
        for (int i = 0; i < levelHeight; i += interval)
        {
            interval = Random.Range(spawnIntervalMin, spawnIntervalMax);
            Vector3 moveVector = new Vector3(0, -interval, 0);
            transform.Translate(moveVector);

            GameObject chunk = chunks[Random.Range(0, chunks.Length)];
            GameObject inst = Instantiate(chunk, transform.position, Quaternion.identity);
            inst.transform.localScale = new Vector3(xScale, 1, 1);
        }

        Destroy(gameObject);
    }
}
