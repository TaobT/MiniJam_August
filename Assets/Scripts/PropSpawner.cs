using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropSpawner : MonoBehaviour
{
    [SerializeField] private float maxTimeToSpawn;
    [SerializeField] private float minTimeToSpawn;
    [SerializeField] private GameObject[] props;
    private bool canSpawn = true;

    private float spawnTime;
    private Vector2 randomPosition;

    private void Start()
    {
        spawnTime = Random.Range(minTimeToSpawn, maxTimeToSpawn);
    }

    private void Update()
    {
        randomPosition = new Vector2(Random.Range(-17, 17), Random.Range(-7, 7));

        if (canSpawn)
        {
            StartCoroutine(SpawnProp());
            canSpawn = false;
        }
    }

    private IEnumerator SpawnProp()
    {
        yield return new WaitForSeconds(spawnTime);
        GameObject prop = Instantiate(props[Random.Range(0, props.Length)], randomPosition, Quaternion.identity);
        prop.transform.position = randomPosition;
        prop.GetComponent<PropForTomatoes>().SetSpawner(this);
        spawnTime = Random.Range(minTimeToSpawn, maxTimeToSpawn);
    }

    public void PropDied()
    {
        canSpawn = true;
    }
}
