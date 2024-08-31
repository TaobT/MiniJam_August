using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    private float randomMoveTimer;
    [SerializeField] private GameObject rottenTomatoPrefab;
    [SerializeField] private int spawnTime;

    private void Start()
    {
        int random = Random.Range(0, 1);

        if(random == 0)
        {
            CirclePatern();
        } else
        {
            RandomPatern();
        }
    }

    private void Update()
    {
        float randomMoveTime = Random.Range(3, 8);
        int randomMove = Random.Range(0, 3);

        if(randomMoveTimer <= 0)
        {
            if (randomMove == 0)
            {
                CirclePatern();
            }
            else if (randomMove == 1)
            {
                RandomPatern();
            } else if (randomMove == 2)
            {
                Teleport();
            }

            randomMoveTimer = randomMoveTime;
        } else
        {
            randomMoveTimer -= Time.deltaTime;
        }
    }

    private void CirclePatern()
    {
        Boss.instance.angleSpread = 359f;
        Boss.instance.oscillate = false;
        Boss.instance.stagger = false;
        Boss.instance.projectilesPerBurst = 20;
        Boss.instance.Attack();
    }

    private void RandomPatern()
    {
        Boss.instance.angleSpread = 60f;
        Boss.instance.oscillate = true;
        Boss.instance.stagger = true;
        Boss.instance.projectilesPerBurst = 5;
        Boss.instance.Attack();
    }

    private void Teleport()
    {
        transform.position = new Vector3(Random.Range(-11, 11), Random.Range(4, -4f));

        for(int i = 0; i <spawnTime; i++)
        {
            Vector2 randomSpawnPosition = new Vector3(Random.Range(-11, 11), 6);
            Instantiate(rottenTomatoPrefab, randomSpawnPosition, Quaternion.identity);
        }
    }
}
