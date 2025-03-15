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

    [SerializeField] 
    private int collisionDamage = 10;


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
            GameManager.Instance.EndScene(this.gameObject);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Piedras") || 
            collision.gameObject.layer == LayerMask.NameToLayer("Palo"))
        {
            Damage(collisionDamage);
        }
    }
    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
    public bool GetifDead() => isDead;
    
}


