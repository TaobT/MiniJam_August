using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    public enum TomatoType
    {
        Normal,
        Rotten
    }

    [SerializeField] private TomatoType type;
    [Space]
    [SerializeField] private float healthLoseTime = 5f;

    PlayerMovement playerMovement;
    private void Awake()
    {
       playerMovement = FindObjectOfType<PlayerMovement>();
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
                case TomatoType.Normal:
                    playerMovement.PowerUp();
                    Destroy(gameObject);

                    break;

                case TomatoType.Rotten:
                    playerMovement.PowerDown();
                    Destroy(gameObject);
                    break;
            }

        }
    }
}
