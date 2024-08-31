using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    public enum TomatoType
    {
        Speed,
        Healing,
        Rotten
    }

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TomatoType type;
    [SerializeField] private bool pickRandomTypeAtStart;
    [SerializeField] private float tomatoSpeedProb = 0.5f;
    [SerializeField] private float tomatoHealProb = 0.1f;
    [SerializeField] private float tomatoRottenProb = 0.4f;
    [Space]
    [SerializeField] private float healthLoseTime = 5f;

    PlayerMovement playerMovement;
    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        if (pickRandomTypeAtStart) SelectRandomTomatoType();

        switch (type)
        {
            case TomatoType.Speed:
                _spriteRenderer.color = new Color(1, 0, 0);
                break;
            case TomatoType.Healing:
                _spriteRenderer.color = new Color(1, 0, 0.2f);
                break;
            case TomatoType.Rotten:
                _spriteRenderer.color = new Color(0.5f, 0, 0);
                break;
        }
    }

    private void Update()
    {
        if(transform.position.y <= -6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (type)
            {
                case TomatoType.Speed:
                    playerMovement.PowerUp();
                    Destroy(gameObject);
                    break;
                case TomatoType.Rotten:
                    playerMovement.PowerDown();
                    Destroy(gameObject);
                    break;
                case TomatoType.Healing:
                    playerMovement.HealUp();
                    Destroy(gameObject);
                    break;
            }

        }
    }

    private void SelectRandomTomatoType()
    {
        float randomValue = Random.value;
        if (randomValue <= tomatoSpeedProb)
        {
            type = TomatoType.Speed;
        }
        else if (randomValue <= tomatoSpeedProb + tomatoHealProb)
        {
            type = TomatoType.Healing;
        }
        else
        {
            type = TomatoType.Rotten;
        }
    }
}
