using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] 
    private int maxHealth = 100;
    [SerializeField] 
    private int initialHealth = 75;
    private int currentHealth;


    private bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        currentHealth = initialHealth;
    }

    public void Damage(int amount)
    {
        if (amount <= 0 || isDead) return;


        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            isDead=true;
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0) return;

        currentHealth += amount;

        if (currentHealth > 0)
        {
            currentHealth = initialHealth;
        }

    }
    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
    public bool GetifDead() => isDead;
    
}


