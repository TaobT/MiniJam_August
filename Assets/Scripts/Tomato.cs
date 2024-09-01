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
    [Space]
    [SerializeField] private Sprite rottenTomato;
    [SerializeField] private Sprite normalTomato;
    [SerializeField] private Sprite healTomato;

    PlayerMovement playerMovement;
    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        if (pickRandomTypeAtStart) SelectRandomTomatoType();
    }

    private void Update()
    {
        if(transform.position.y <= -6f)
        {
            Destroy(gameObject);
        }

        switch(type)
        {
            case TomatoType.Speed:
                _spriteRenderer.sprite = normalTomato;
                break;

            case TomatoType.Rotten:
                _spriteRenderer.sprite = rottenTomato;
                break;
            case TomatoType.Healing:
                _spriteRenderer.sprite = healTomato;
                break;
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
        int randomValue = Random.Range(0,6);
        if (randomValue == 0 || randomValue == 1)
        {
            type = TomatoType.Speed;
        }
        else if (randomValue == 2)
        {
            type = TomatoType.Healing;
        }
        else
        {
            type = TomatoType.Rotten;
        }
    }
}
