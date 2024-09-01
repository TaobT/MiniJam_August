using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("PowerUp")]
    [SerializeField] private float normalSpeed = 8f;
    [SerializeField] private float tomatoSpeed = 10f;
    [SerializeField] private float rottenSpeed = 5f;
    [SerializeField] private int tomatoHeal = 5;
    [Space]
    [SerializeField] private float powerUpDuration = 5f;
    [SerializeField] private float powerDownDuration = 5f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [Space]
    [SerializeField] private TrailRenderer tr;
    [Space]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite spritePowerUp;
    [SerializeField] private Sprite spriteNormal;
    [Space]
    [SerializeField] private Health health;
    [Space]
    [SerializeField] private Camera playerCam;

    [Header("Audio")]
    [SerializeField] private AudioClip[] footsteps;
    [SerializeField] private AudioClip powerUp;
    [SerializeField] private float timeBeforeNextAudio;

    public static PlayerMovement Instance { get; private set; }

    private Vector2 movement;
    private float powerUpTime;
    private float powerDownTime;
    private bool isPowerDown = false;
    private bool isPoweredUp = false;
    private Vector2 mousePos;
    private float timeBeforeNextAudioTimer;

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
        if(OptionsUI.instance.isPaused)
        {
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = playerCam.ScreenToWorldPoint(Input.mousePosition);
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

        if (movement.sqrMagnitude > 0 && timeBeforeNextAudioTimer <= 0)
        {
            AudioManager.instance.PlaySFX(footsteps[Random.Range(0, footsteps.Length)]);
            timeBeforeNextAudioTimer = timeBeforeNextAudio;
        }
        else
        {
            timeBeforeNextAudioTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * currentSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
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
        AudioManager.instance.PlaySFX(powerUp);
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
