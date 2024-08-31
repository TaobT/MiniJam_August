using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    private void Update()
    {
        if(transform.position.y <= -6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();

            if(playerMovement != null)
            {
                playerMovement.PowerUp();
                Destroy(gameObject);
            }
        }
    }
}
