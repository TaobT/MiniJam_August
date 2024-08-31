using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{    
    [SerializeField] private float normalSpeed = 8f;
    [SerializeField] private float tomatoSpeed = 10f;
    [SerializeField] private float rottenSpeed = 5f;
    [SerializeField] private int tomatoHeal = 5;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite spritePowerUp;
    [SerializeField] private Sprite spriteNormal;
    [SerializeField] private float powerUpDuration = 5f;
    [SerializeField] private float powerDownDuration = 5f;
    [SerializeField] private Health health;
    public static PlayerMovement Instance { get; private set; }

    private Vector2 movement;
    private float powerUpTime;
    private float powerDownTime;
    private bool isPowerDown = false;
    private bool isPoweredUp = false;

    private float currentSpeed;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        AdjustPlayerFacingDirection();

        if (powerUpTime > 0 && !isPowerDown)
        {
            isPoweredUp = true;
            currentSpeed = tomatoSpeed;
            tr.emitting = true;
            powerUpTime -= Time.deltaTime;
            sr.sprite = spritePowerUp;
        }
        else
        {
            isPoweredUp = false;
            currentSpeed = normalSpeed;
            tr.emitting = false;
            sr.sprite = spriteNormal;
        }

        if(powerDownTime > 0)
        {
            isPowerDown = true;
            currentSpeed = rottenSpeed;
            powerDownTime -= Time.deltaTime;

        } else if(!isPoweredUp)
        {
            isPowerDown = false;
            currentSpeed = 8f;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * currentSpeed * Time.fixedDeltaTime);
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

    public void HealUp()
    {
        health.Heal(tomatoHeal);
    }
}
