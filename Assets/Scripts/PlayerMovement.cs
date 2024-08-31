using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private float powerUpDuration = 5f;
    [SerializeField] private float powerDownDuration = 5f;
    [SerializeField] private Health health;
    public static PlayerMovement Instance { get; private set; }

    private Vector2 movement;
    private float powerUpTime;
    private float powerDownTime;
    private bool isPowerDown = false;
    private bool isPoweredUp = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        AdjustPlayerFacingDirection();

        if (powerUpTime > 0 && !isPowerDown)
        {
            isPoweredUp = true;
            speed = 10f;
            tr.emitting = true;
            powerUpTime -= Time.deltaTime;
        }
        else
        {
            isPoweredUp = false;
            speed = 8f;
            tr.emitting = false;
        }

        if(powerDownTime > 0)
        {
            isPowerDown = true;
            speed = 5f;
            powerDownTime -= Time.deltaTime;

        } else if(!isPoweredUp)
        {
            isPowerDown = false;
            speed = 8f;
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

    public void PowerDown()
    {
        powerDownTime += powerDownDuration;
    }
}
