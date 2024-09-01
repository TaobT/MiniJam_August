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

    private void Start()
    {
        spawnTime = Random.Range(minTimeToSpawn, maxTimeToSpawn);
    }

    private void Update()
    {
        if (canSpawn)
        {
            StartCoroutine(SpawnProp());
            canSpawn = false;
        }
    }

    private IEnumerator SpawnProp()
    {
        yield return new WaitForSeconds(spawnTime);
        GameObject prop = Instantiate(props[Random.Range(0, props.Length)], transform.position, Quaternion.identity);
        prop.transform.SetParent(transform, false);
        prop.GetComponent<PropForTomatoes>().SetSpawner(this);
        spawnTime = Random.Range(minTimeToSpawn, maxTimeToSpawn);
    }

    public void PropDied()
    {
        canSpawn = true;
    }
}
