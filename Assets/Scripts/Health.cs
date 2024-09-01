using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private bool isPlayer = false;
    [SerializeField] private bool isBoss = false;
    public int currentHealth;

    private void Start()
    {
        currentHealth = health;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0 && isPlayer) 
        {
            Ending.instance.DefeatUI();
            Destroy(gameObject);
        } else if(currentHealth < 0 && isBoss)
        {
            Ending.instance.VictoryUI();
            Destroy(gameObject);
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
    }
}
