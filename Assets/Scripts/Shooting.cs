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
 
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && fireTimer <= 0)
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
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, angle * 1.5f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, angle * 1.5f);
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
    }
}
