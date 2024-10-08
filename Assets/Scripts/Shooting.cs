using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Stats")]
    [SerializeField] private float fireRate;
    private float fireTimer;

    [Header("Audio")]
    [SerializeField] private AudioClip knifeShoot;
 
    private void Update()
    {
        if (OptionsUI.instance.isPaused)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && fireTimer <= 0)
        {
            Shoot();
            fireTimer = fireRate;
        } else
        {
            fireTimer -= Time.deltaTime;
        }

        Rotation();
    }

    private void Rotation()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookDir = mousePos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
        AudioManager.instance.PlaySFX(knifeShoot);
    }
}
