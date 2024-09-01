using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int health;
    public int currentHealth;

    [Header("Identification")]
    [SerializeField] private bool isPlayer = false;
    [SerializeField] private bool isBoss = false;
    [SerializeField] private bool isMinion = false;

    [Header("UI/Cutscene")]
    [SerializeField] private GameObject victoryTimeline;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject boss;

    [Header("Audio")]
    [SerializeField] private AudioClip[] bossDamage;

    private void Start()
    {
        currentHealth = health;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(isBoss)
        {
            AudioManager.instance.PlaySFX(bossDamage[Random.Range(0, 1)], 0.5f);
        }

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
        } else if(currentHealth < 0 && isMinion)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
    }
}
