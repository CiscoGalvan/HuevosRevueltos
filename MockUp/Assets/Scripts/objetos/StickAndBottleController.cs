using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
public class StickAndBottleController : MonoBehaviour
{
	private enum FaseMovimiento
	{
		Cascada,
		Rio
	}
	
	private int _randomSide;
	private BoxCollider _boxCollider;
	[SerializeField] private BoxCollider _riverCollider;
	[SerializeField] private float _maxSpeed = 5f; // Velocidad máxima
	[SerializeField] private float _steeringStrength = 2f; // Fuerza de Steering

	private Vector3 _targetPosition;
	private Vector3 _targetPositionCascada;
	private Vector3 _currentVelocity;
	private FaseMovimiento fase;

	private void Start()
	{
		_randomSide = Random.Range(0, 2);
		fase = FaseMovimiento.Cascada;
		_boxCollider = GetComponent<BoxCollider>();
		_boxCollider.isTrigger = true;
		SetTargetPosition();
		if (IsLeftSpawned())
		{
			//Está en el lado izquierdo
			_targetPositionCascada = new Vector3(transform.position.x, _riverCollider.bounds.max.y + _boxCollider.size.y/2,
				_riverCollider.bounds.min.z);
		}
		else
		{
			//Está en el lado derecho
			_targetPositionCascada = new Vector3(transform.position.x, _riverCollider.bounds.max.y + _boxCollider.size.y/2,
				_riverCollider.bounds.max.z);
		}
	}

	private void Update()
	{
		switch (fase)
		{
			case FaseMovimiento.Cascada:
				ReachRiver();
				break;
			case FaseMovimiento.Rio:
				ApplySteering();
				break;
		}
	}

	private void ReachRiver()
	{
		// Limitar la velocidad a la máxima permitida
		_currentVelocity = (_targetPositionCascada - transform.position).normalized * _maxSpeed;

		// Mover el objeto
		transform.position += _currentVelocity * Time.deltaTime;

		// Rotar hacia la dirección del movimiento
		if (_currentVelocity.magnitude > 0.1f)
		{
			transform.rotation = Quaternion.LookRotation(_currentVelocity.normalized);
		}
	}

	private bool IsLeftSpawned()
	{
		if (Vector3.Distance(transform.position, new Vector3(_riverCollider.center.x, _riverCollider.bounds.max.y, _riverCollider.bounds.min.z))
		    <
		    Vector3.Distance(transform.position, new Vector3(_riverCollider.center.x, _riverCollider.bounds.max.y, _riverCollider.bounds.max.z)))
		{
			return true;
		}

		return false;
	}

	private void SetTargetPosition()
	{
		if (_riverCollider == null)
		{
			Debug.LogError("No se ha asignado un BoxCollider en StickAndBottleController.");
			return;
		}

		// Obtener los límites del BoxCollider
		Bounds bounds = _riverCollider.bounds;

		// Determinar la posición objetivo en X
		float targetX = (_randomSide == 0) ? bounds.min.x : bounds.max.x;

		// Determinar una posición aleatoria en Z dentro del BoxCollider
		float randomZ = Random.Range(bounds.min.z, bounds.max.z);

		// Definir la posición de destino con Y en 0
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

		// Limitar la velocidad a la máxima permitida
		_currentVelocity = Vector3.ClampMagnitude(_currentVelocity, _maxSpeed);
		_currentVelocity.y = 0;

		// Mover el objeto
		transform.position += _currentVelocity * Time.deltaTime;

		// Rotar hacia la dirección del movimiento
		if (_currentVelocity.magnitude > 0.1f)
		{
			transform.rotation = Quaternion.LookRotation(_currentVelocity.normalized);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Corriente")
		{
			fase = FaseMovimiento.Rio;
			transform.position = new Vector3(transform.position.x, _targetPosition.y, transform.position.z);
		}
	}
}
