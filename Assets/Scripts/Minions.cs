using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minions : MonoBehaviour
{
    public float speed;
    private float distance;
    private bool isAttacking = false;

    private void Update()
    {
        if (OptionsUI.instance.isPaused)
        {
            return;
        }

        distance = Vector2.Distance(transform.position, PlayerMovement.Instance.transform.position);
        Vector2 direction = PlayerMovement.Instance.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(this.transform.position, PlayerMovement.Instance.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (OptionsUI.instance.isPaused)
        {
            return;
        }

        Health health = collision.gameObject.GetComponent<Health>();

        if (collision.gameObject.tag == "Player" && !isAttacking)
        {
            isAttacking = true;
            health.TakeDamage(1);
            StartCoroutine(ResetAttack());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (OptionsUI.instance.isPaused)
        {
            return;
        }

        Health health = collision.gameObject.GetComponent<Health>();

        if (collision.gameObject.tag == "Player" && !isAttacking)
        {
            isAttacking = true;
            health.TakeDamage(1);
            StartCoroutine(ResetAttack());
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
}
