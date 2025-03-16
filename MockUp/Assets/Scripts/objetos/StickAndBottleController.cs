using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class StickAndBottleController : MonoBehaviour
{
	public enum FaseMovimiento
	{
		Cascada,
		Rio
	}

	private int _randomSide;
	private CapsuleCollider _capsuleCollider;
	private Rigidbody _rb; // Referencia al Rigidbody

	[SerializeField] public BoxCollider _riverCollider;
	[SerializeField] private float _maxSpeed = 5f; // Velocidad máxima
	[SerializeField] private float _steeringStrength = 2f; // Fuerza de Steering

	private Vector3 _targetPosition;
	private Vector3 _targetPositionCascada;
	private Vector3 _currentVelocity;
	public FaseMovimiento fase;


	[SerializeField]
	private float _livingTime = 20.0f;
	private float _time;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>(); // Obtener el Rigidbody
		_randomSide = Random.Range(0, 2);
		fase = FaseMovimiento.Cascada;
		_capsuleCollider = GetComponent<CapsuleCollider>();
		SetTargetPosition();
		if (IsLeftSpawned())
		{
			// Está en el lado izquierdo
			_targetPositionCascada = new Vector3(transform.position.x, _riverCollider.bounds.max.y + _capsuleCollider.radius,
				_riverCollider.bounds.min.z);
		}
		else
		{
			// Está en el lado derecho
			_targetPositionCascada = new Vector3(transform.position.x, _riverCollider.bounds.max.y + _capsuleCollider.radius,
				_riverCollider.bounds.max.z);
		}
	}

	private void Update()
	{
		_time += Time.deltaTime;


		if(_time >= _livingTime)
		{
			Destroy(this.gameObject);
		}
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
		// Calcular la velocidad deseada limitada a la máxima permitida
		_currentVelocity = (_targetPositionCascada - transform.position).normalized * _maxSpeed;

		// Mover el objeto utilizando el Rigidbody
		_rb.MovePosition(_rb.position + _currentVelocity * Time.deltaTime);

		// Rotar hacia la dirección del movimiento
		if (_currentVelocity.magnitude > 0.1f)
		{
			_rb.MoveRotation(Quaternion.LookRotation(_currentVelocity.normalized));
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
		_targetPosition = new Vector3(targetX, 2 + _capsuleCollider.radius, randomZ);
	}

	private void ApplySteering()
	{
		// Calcular la dirección deseada hacia el objetivo
		Vector3 desiredVelocity = (_targetPosition - transform.position).normalized * _maxSpeed;

		// Calcular la fuerza de steering (diferencia entre la velocidad deseada y la actual)
		Vector3 steeringForce = (desiredVelocity - _currentVelocity) * _steeringStrength * Time.deltaTime;

		// Aplicar la fuerza de steering a la velocidad actual
		_currentVelocity += steeringForce;

		// Limitar la velocidad a la máxima permitida
		_currentVelocity = Vector3.ClampMagnitude(_currentVelocity, _maxSpeed);
		_currentVelocity.y = 0;

		// Mover el objeto utilizando el Rigidbody
		_rb.MovePosition(_rb.position + _currentVelocity * Time.deltaTime);

		// Rotar hacia la dirección del movimiento
		if (_currentVelocity.magnitude > 0.1f)
		{
			_rb.MoveRotation(Quaternion.LookRotation(_currentVelocity.normalized));
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name == "Corriente")
		{
			fase = FaseMovimiento.Rio;
			// Ajustar la posición utilizando el Rigidbody
			_rb.position = new Vector3(transform.position.x, _targetPosition.y, transform.position.z);
		}
	}
}
