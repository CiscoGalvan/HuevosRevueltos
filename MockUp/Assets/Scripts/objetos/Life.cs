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
    [SerializeField]
    private List<GameObject> _objetosHijos;
    [SerializeField]
    private List<int> _relacionVidaObjetos;
	// Start is called before the first frame update
	void Start()
    {
        isDead = false;
        currentHealth = initialHealth;
        SeeIfNeedChange();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
            Debug.Log(this.gameObject);
			GameManager.Instance.EndScene(this.gameObject);
		}
	}
	public void Damage(int amount)
    {
        if (amount <= 0 || isDead) return;
        currentHealth -= amount;
        ft_AudioManager.Instance.PlaySFX(ft_AudioManager.ft_AudioType.DamDmg, volume: 1.0f);
        SeeIfNeedChange();
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
        ft_AudioManager.Instance.PlaySFX(ft_AudioManager.ft_AudioType.DamHeal, volume: 1.0f);
		SeeIfNeedChange();
		if (currentHealth > 0)
        {
            currentHealth = initialHealth;
        }

    }
    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
    public bool GetifDead() => isDead;

    private void SeeIfNeedChange()
    {
		if (_relacionVidaObjetos == null || _relacionVidaObjetos.Count == 0) return;
		int closestIndex = 0;
		int closestDifference = Mathf.Abs(_relacionVidaObjetos[0] - currentHealth);
		// Buscar el �ndice del valor m�s cercano a currentHealth en _relacionVidaObjetos
		for (int i = 1; i < _relacionVidaObjetos.Count; i++)
		{
			int difference = Mathf.Abs(_relacionVidaObjetos[i] - currentHealth);
			if (difference < closestDifference)
			{
				closestDifference = difference;
				closestIndex = i;
			}
		}
		DeactivateAllOthers(closestIndex);
	}

    private void DeactivateAllOthers(int i)
    {
		if (_objetosHijos == null || _objetosHijos.Count == 0) return;
		for (int j = 0; j < _objetosHijos.Count; j++)
		{
			if (j == i)
			{
				_objetosHijos[j].SetActive(true); // Activa solo el objeto con el �ndice i
			}
            else
            {
				_objetosHijos[j].SetActive(false); // Activa solo el objeto con el �ndice i
			}
		}
	}
    
}


