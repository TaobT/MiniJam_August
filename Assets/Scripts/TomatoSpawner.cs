using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoSpawner : MonoBehaviour
{
    public GameObject tomatoPrefab;

    private float startDelay = 2.0f;
    private float spawnInterval;
    void Start()
    {
        Invoke("Spawn", startDelay);
    }
    void Spawn()
    {
        spawnInterval = Random.Range(12f, 20f);

        Vector2 randomSpawnPosition = new Vector3(Random.Range(-11, 11), Random.Range(5, -5f));
        Instantiate(tomatoPrefab, randomSpawnPosition, Quaternion.identity);

        Invoke("Spawn", spawnInterval);
    }
}
