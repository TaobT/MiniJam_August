using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    private float randomMoveTimer;
    [SerializeField] private GameObject rottenTomatoPrefab;
    [SerializeField] private int spawnTime;

    enum SpawnerType { Straight, Spin }
    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f;


    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;

    [Header("Boss Moves")]
    [SerializeField] private float minRandomMoveTime;
    [SerializeField] private float maxRandomMoveTime;
    [SerializeField] private Health bossHealth;

    private GameObject spawnedBullet;
    private float timer = 0f;

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
        if (bossHealth.currentHealth < 50)
        {
            minRandomMoveTime = 2;
            maxRandomMoveTime = 5;
        }

        float randomMoveTime = Random.Range(minRandomMoveTime, maxRandomMoveTime);

        int randomMove = Random.Range(0, 4);

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
            } else if(randomMove == 3)
            {
                SpinPatern();
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

    private void SpinPatern()
    {
        if (bullet)
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }


}
