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

    //AudioVariables

    public AK.Wwise.Event KnifeSFX;
 
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && fireTimer <= 0)
        {
            Shoot();
            fireTimer = fireRate;
            KnifeSFX.Post(gameObject);
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
    }
}
