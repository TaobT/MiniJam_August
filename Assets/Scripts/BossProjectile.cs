using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float timeBeforeBulletDisapear = 3f;

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

        if (collision.gameObject.tag == "Player")
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
