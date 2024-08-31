using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Health health;
    [SerializeField] private float rate;

    private void Update()
    {
        healthBar.fillAmount = health.currentHealth / rate;
    }
}
