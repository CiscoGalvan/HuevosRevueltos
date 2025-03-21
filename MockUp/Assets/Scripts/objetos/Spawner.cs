using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private BoxCollider _colliderCascadaOne;
	[SerializeField] private BoxCollider _colliderCascadaTwo;

	[Header("Spawn Settings")]
	[SerializeField] private float minSpawnTime = 3f; // Tiempo m�nimo entre spawns
	[SerializeField] private float maxSpawnTime = 7f; // Tiempo m�ximo entre spawns
	[SerializeField] private float paloProbability = 0.5f; // Probabilidad de generar un palo

	[Header("Prefabs")]
	[SerializeField] private List<GameObject> ListaLatas; // Lista de prefabs de latas
	[SerializeField] private List<GameObject> ListaPalos; // Lista de prefabs de palos

	private bool _canSpawnLata = true; // Controla si se puede generar una nueva lata
	[SerializeField] private BoxCollider _riverCollider;

	void Start()
	{
		StartCoroutine(SpawnRoutine());
	}

	IEnumerator SpawnRoutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime)); // Espera aleatoria

			GameObject objetoAInstanciar = GetRandomObject();

			if (objetoAInstanciar != null)
			{
				Vector3 spawnPosition = Random.Range(0, 2) == 0 ? GetRandomPointInZone1() : GetRandomPointInZone2();
				GameObject objeto = Instantiate(objetoAInstanciar, spawnPosition, Quaternion.identity);
				objeto.GetComponent<StickAndBottleController>()._riverCollider = _riverCollider;

				// Si es una lata, activar cooldown
				if (ListaLatas.Contains(objetoAInstanciar))
				{
					_canSpawnLata = false;
					StartCoroutine(ResetLataCooldown());
				}
			}
		}
	}

	// Retorna un objeto aleatorio (palo o lata seg�n probabilidad)
	GameObject GetRandomObject()
	{
		if (Random.value < paloProbability) // Probabilidad de generar un palo
		{
			return ListaPalos[Random.Range(0, ListaPalos.Count)];
		}
		else if (_canSpawnLata && ListaLatas.Count > 0) // Solo generar lata si est� permitido
		{
			return ListaLatas[Random.Range(0, ListaLatas.Count)];
		}

		return null; // No genera nada si no hay objetos v�lidos
	}

	// Retorna un punto aleatorio en la zona 1
	Vector3 GetRandomPointInZone1()
	{
		Vector3 boxSize = _colliderCascadaOne.size;
		Vector3 boxCenter = _colliderCascadaOne.center;
		float randomX = Random.Range(-boxSize.x / 2, boxSize.x / 2);
		float randomY = Random.Range(-boxSize.y / 2, boxSize.y / 2);
		float randomZ = Random.Range(-boxSize.z / 2, boxSize.z / 2);
		Vector3 localPoint = new Vector3(randomX, randomY, randomZ) + boxCenter;
		Vector3 worldPoint = _colliderCascadaOne.transform.TransformPoint(localPoint);

		return _colliderCascadaOne != null
			? worldPoint
			: Vector3.zero;
	}

	// Retorna un punto aleatorio en la zona 2
	Vector3 GetRandomPointInZone2()
	{
		Vector3 boxSize = _colliderCascadaTwo.size;
		Vector3 boxCenter = _colliderCascadaTwo.center;
		float randomX = Random.Range(-boxSize.x / 2, boxSize.x / 2);
		float randomY = Random.Range(-boxSize.y / 2, boxSize.y / 2);
		float randomZ = Random.Range(-boxSize.z / 2, boxSize.z / 2);
		Vector3 localPoint = new Vector3(randomX, randomY, randomZ) + boxCenter;
		Vector3 worldPoint = _colliderCascadaTwo.transform.TransformPoint(localPoint);

		return _colliderCascadaTwo != null
			? worldPoint
			: Vector3.zero;
	}

	// Corrutina para permitir la generaci�n de otra lata despu�s de un tiempo
	IEnumerator ResetLataCooldown()
	{
		yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
		_canSpawnLata = true;
	}
}
