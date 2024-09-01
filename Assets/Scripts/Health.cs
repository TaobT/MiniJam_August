using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private bool isPlayer = false;
    [SerializeField] private bool isBoss = false;
    [SerializeField] private GameObject victoryTimeline;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject boss;
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
            Destroy(boss);
            Destroy(gameObject);
        } else if(currentHealth < 0 && isBoss)
        {
            victoryTimeline.SetActive(true);
            Destroy(player);
            Destroy(gameObject);
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
    }
}
