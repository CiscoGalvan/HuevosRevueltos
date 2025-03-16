using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickAndBottleController : MonoBehaviour
{
	private int _randomSide;
	[SerializeField] private BoxCollider _riverCollider;
	[SerializeField] private float _maxSpeed = 5f; // Velocidad m�xima
	[SerializeField] private float _steeringStrength = 2f; // Fuerza de Steering

	private Vector3 _targetPosition;
	private Vector3 _currentVelocity;

	private void Start()
	{
		_randomSide = Random.Range(0, 2);
		SetTargetPosition();
	}

	private void Update()
	{
		ApplySteering();
	}

	private void SetTargetPosition()
	{
		if (_riverCollider == null)
		{
			Debug.LogError("No se ha asignado un BoxCollider en StickAndBottleController.");
			return;
		}

		// Obtener los l�mites del BoxCollider
		Bounds bounds = _riverCollider.bounds;

		// Determinar la posici�n objetivo en X
		float targetX = (_randomSide == 0) ? bounds.min.x : bounds.max.x;

		// Determinar una posici�n aleatoria en Z dentro del BoxCollider
		float randomZ = Random.Range(bounds.min.z, bounds.max.z);

		// Definir la posici�n de destino con Y en 0
		_targetPosition = new Vector3(targetX, 0, randomZ);
	}

	private void ApplySteering()
	{
		// Direccion deseada hacia el objetivo
		Vector3 desiredVelocity = (_targetPosition - transform.position).normalized * _maxSpeed;

		// Fuerza de steering (diferencia entre velocidad deseada y actual)
		Vector3 steeringForce = (desiredVelocity - _currentVelocity) * _steeringStrength * Time.deltaTime;

		// Aplicar la fuerza de steering a la velocidad actual
		_currentVelocity += steeringForce;

		// Limitar la velocidad a la m�xima permitida
		_currentVelocity = Vector3.ClampMagnitude(_currentVelocity, _maxSpeed);

		// Mover el objeto
		transform.position += _currentVelocity * Time.deltaTime;

		// Rotar hacia la direcci�n del movimiento
		if (_currentVelocity.magnitude > 0.1f)
		{
			transform.rotation = Quaternion.LookRotation(_currentVelocity.normalized);
		}
	}
}
