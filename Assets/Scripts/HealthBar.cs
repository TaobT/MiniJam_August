using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Health health;

    private void Update()
    {
        healthBar.fillAmount = health.currentHealth / 10f;
    }
}
