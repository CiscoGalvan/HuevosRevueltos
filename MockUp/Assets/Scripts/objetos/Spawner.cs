using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private BoxCollider _colliderCascadaOne;
	[SerializeField] private BoxCollider _colliderCascadaTwo;

	[Header("Spawn Settings")]
	[SerializeField] private float minSpawnTime = 3f; // Tiempo mínimo entre spawns
	[SerializeField] private float maxSpawnTime = 7f; // Tiempo máximo entre spawns
	[SerializeField] private float paloProbability = 0.5f; // Probabilidad de generar un palo

	[Header("Prefabs")]
	[SerializeField] private List<GameObject> ListaLatas; // Lista de prefabs de latas
	[SerializeField] private GameObject _paloPrefab; // Prefab del palo

	private bool _canSpawnLata = true; // Controla si se puede generar una nueva lata

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
				Instantiate(objetoAInstanciar, spawnPosition, Quaternion.identity);

				// Si es una lata, activar cooldown
				if (ListaLatas.Contains(objetoAInstanciar))
				{
					_canSpawnLata = false;
					StartCoroutine(ResetLataCooldown());
				}
			}
		}
	}

	// Retorna un objeto aleatorio (palo o lata según probabilidad)
	GameObject GetRandomObject()
	{
		if (Random.value < paloProbability) // Probabilidad de generar un palo
		{
			return _paloPrefab;
		}
		else if (_canSpawnLata && ListaLatas.Count > 0) // Solo generar lata si está permitido
		{
			return ListaLatas[Random.Range(0, ListaLatas.Count)];
		}

		return null; // No genera nada si no hay objetos válidos
	}

	// Retorna un punto aleatorio en la zona 1
	Vector3 GetRandomPointInZone1()
	{
		return _colliderCascadaOne != null
			? new Vector3(
				Random.Range(_colliderCascadaOne.bounds.min.x, _colliderCascadaOne.bounds.max.x),
				0,
				Random.Range(_colliderCascadaOne.bounds.min.z, _colliderCascadaOne.bounds.max.z))
			: Vector3.zero;
	}

	// Retorna un punto aleatorio en la zona 2
	Vector3 GetRandomPointInZone2()
	{
		return _colliderCascadaTwo != null
			? new Vector3(
				Random.Range(_colliderCascadaTwo.bounds.min.x, _colliderCascadaTwo.bounds.max.x),
				0,
				Random.Range(_colliderCascadaTwo.bounds.min.z, _colliderCascadaTwo.bounds.max.z))
			: Vector3.zero;
	}

	// Corrutina para permitir la generación de otra lata después de un tiempo
	IEnumerator ResetLataCooldown()
	{
		yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
		_canSpawnLata = true;
	}
}
