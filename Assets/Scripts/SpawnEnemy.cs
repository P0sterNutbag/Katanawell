using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemies;
    public float spawnChance = 0.5f;

    void Start()
    {
        if (Random.Range(0f, 1f) <= spawnChance)
        {
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];
            Instantiate(enemy, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
