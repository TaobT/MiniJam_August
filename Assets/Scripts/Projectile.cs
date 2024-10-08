using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Sprite[] bulletSprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private float timeBeforeBulletDisapear = 3f;

    private void Awake()
    {
        spriteRenderer.sprite = bulletSprites[Random.Range(0, bulletSprites.Length)];
    }

    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);

        if (timeBeforeBulletDisapear > 0)
        {
            timeBeforeBulletDisapear -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if(collision.gameObject.tag == "Boss")
        {
            health.TakeDamage(1);
        }

        if (collision.gameObject.tag == "Minion")
        {
            health.TakeDamage(1);
        }

        Destroy(gameObject);
    }

    public void UpdateMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
