using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [Header("Spawn Rotten Tomato")]
    private float randomMoveTimer;
    [SerializeField] private GameObject rottenTomatoPrefab;
    [SerializeField] private int rottenTomatoSpawnAmount;

    [Header("Bullet Attributes")]
    public GameObject bullet;
    [SerializeField] private float fireRate;

    [Header("Boss Moves")]
    [SerializeField] private float minRandomMoveTime;
    [SerializeField] private float maxRandomMoveTime;
    [SerializeField] private Health bossHealth;

    [Header("Spawn Minions Move")]
    [SerializeField] private GameObject minionPrefab;
    [SerializeField] private int minionSpawnAmount;

    private GameObject spawnedBullet;
    private float timer;

    private void Start()
    {
        int random = Random.Range(0, 1);

        if (random == 0)
        {
            CirclePatern();
        }
        else
        {
            RandomPatern();
        }
    }

    private void Update()
    {
        if (bossHealth.currentHealth < 50)
        {
            minRandomMoveTime = 2;
            maxRandomMoveTime = 5;
        }

        float randomMoveTime = Random.Range(minRandomMoveTime, maxRandomMoveTime);

        int randomMove = Random.Range(0, 5);

        if (randomMoveTimer <= 0)
        {
            if (randomMove == 0)
            {
                CirclePatern();
            }
            else if (randomMove == 1)
            {
                RandomPatern();
            }
            else if (randomMove == 2)
            {
                Teleport();
            }
            else if (randomMove == 3)
            {
                SpinningPatern();
            } else if(randomMove == 4)
            {
                SpawnMinion();
            }

            randomMoveTimer = randomMoveTime;
        }
        else
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

        for (int i = 0; i < rottenTomatoSpawnAmount; i++)
        {
            Vector2 randomSpawnPosition = new Vector3(Random.Range(-11, 11), 6);
            Instantiate(rottenTomatoPrefab, randomSpawnPosition, Quaternion.identity);
        }
    }

    private void SpinningPatern()
    {
        Boss.instance.angleSpread = 359f;
        Boss.instance.oscillate = true;
        Boss.instance.stagger = true;
        Boss.instance.projectilesPerBurst = 20;
        Boss.instance.Attack();
    }

    private void SpawnMinion()
    {
        for (int i = 0; i < minionSpawnAmount; i++)
        {
            Vector3 postion = transform.position;
            postion.x = postion.x + Random.Range(-2, 2);
            postion.y = postion.y + Random.Range(-2, 2);
            minionPrefab.GetComponent<Minions>().speed = Random.Range(2, 4);
            Instantiate(minionPrefab, postion, Quaternion.identity);
        }
    }
}
