using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoProjectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject tomatoSplatterPrefab;
    [SerializeField] private GameObject pickableTomatoPrefab;
    [Header("Stats")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    [Header("Audio")]
    [SerializeField] private AudioClip[] tomatoSplatt;

    private float speed;
    private Vector2 landingPosition = Vector2.zero;
    private bool isSplatt;
    public void SetLandingPosition(Vector2 landingPosition, bool splatt)
    {
        speed = Random.Range(minSpeed, maxSpeed);
        this.landingPosition = landingPosition;
        isSplatt = splatt;
    }

    private void FixedUpdate()
    {
        if (landingPosition == Vector2.zero)
        {
            Destroy(gameObject);
            Debug.LogError("Landing position not set");
        }
        transform.position = Vector2.MoveTowards(transform.position, landingPosition, speed * Time.fixedDeltaTime);
        if (Vector2.Distance(transform.position, landingPosition) < 0.1f)
        {
            if (isSplatt)
            {
                float randomZRotation = Random.Range(0, 360);
                Instantiate(tomatoSplatterPrefab, transform.position, Quaternion.Euler(0, 0, randomZRotation));
                AudioManager.instance.PlaySFX(tomatoSplatt[Random.Range(0, 2)]);
            }
            else
            {
                Instantiate(pickableTomatoPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
