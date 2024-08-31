using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private float powerUpDuration = 5f;
    public static PlayerMovement Instance { get; private set; }

    private Vector2 movement;
    private float powerUpTime;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        AdjustPlayerFacingDirection();

        if (powerUpTime > 0)
        {
            speed = 12f;
            tr.emitting = true;
            powerUpTime -= Time.deltaTime;
        }
        else
        {
            speed = 8f;
            tr.emitting = false;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * speed * Time.deltaTime);
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            Vector3 localScale = transform.localScale;
            localScale.z = -1f;
            transform.localScale = localScale;
        }
        else
        {
            Vector3 localScale = transform.localScale;
            localScale.z = 1f;
            transform.localScale = localScale;
        }
    }
    
    public void PowerUp()
    {
        powerUpTime += powerUpDuration;
    }
}
