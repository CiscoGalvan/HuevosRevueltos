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
	// Start is called before the first frame update

	// Variable privada para almacenar el umbral calculado
	private float threshold;
	void Start()
    {
        isDead = false;
        currentHealth = initialHealth;
		// Calculamos el umbral solo una vez al inicio
		threshold = maxHealth / (float)_objetosHijos.Count;

		// Activamos el último objeto (la fase inicial es la última)
		if (_objetosHijos.Count > 0)
		{
			_objetosHijos[_objetosHijos.Count - 1].SetActive(true);
		}
	}

    public void Damage(int amount)
    {
        if (amount <= 0 || isDead) return;
        currentHealth -= amount;
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
        //Que alguien revise este codigo y lo haga por dios santo.


		//// Recorremos la lista de objetos hijos para ver si debemos activar alguno
		//for (int i = 0; i < _objetosHijos.Count; i++)
		//{
		//	// Comprobamos si la vida actual ha cruzado un umbral
		//	if (currentHealth <= threshold * (i + 1))
		//	{
		//		// Desactivamos los objetos anteriores
		//		for (int j = 0; j < i; j++)
		//		{
		//			if (_objetosHijos[j].activeSelf)
		//			{
  //                      Debug.Log("i");
		//				_objetosHijos[j].SetActive(false);
		//			}
		//		}

		//		// Activamos el objeto correspondiente al umbral
		//		if (!_objetosHijos[i].activeSelf)
		//		{
		//			_objetosHijos[i].SetActive(true);
		//		}
		//	}
		//}
	}
    
}


