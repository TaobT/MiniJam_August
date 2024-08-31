using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Health>().TakeDamage(1);
        Destroy(gameObject);
    }
}
