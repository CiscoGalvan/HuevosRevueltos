using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
	private Rigidbody _rb;

	[SerializeField] private float _acceleration = 15f;  // Aumenta la velocidad progresivamente  
	[SerializeField] private float _maxSpeed = 15f;       // Velocidad máxima del personaje  
	[SerializeField] private float _friction = 3f;       // Cuánta resistencia hay al detenerse  
	[SerializeField] private float _speed = 10f; // Multiplicador para la entrada del jugador

	// Start is called before the first frame update
	void Start()
	{
		_rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SetMovementDirection(Vector2 direction)
	{
		Debug.Log(direction);
		if (direction.magnitude > 0)
		{
			Vector3 forceDirection = new Vector3(direction.x * _speed, 0, direction.y * _speed);
			_rb.AddForce(forceDirection * _acceleration);
			_rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxSpeed);
		}
		else
		{
			_rb.velocity = Vector3.Lerp(_rb.velocity, Vector3.zero, _friction * Time.deltaTime);
		}
	}
}
