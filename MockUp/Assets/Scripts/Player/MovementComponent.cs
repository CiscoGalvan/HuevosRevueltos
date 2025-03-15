using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
	[Tooltip("Aceleración del movimiento del jugador")]
	[SerializeField] private float _acceleration = 15f;

	[Tooltip("Velocidad máxima que va a alcanzar el jugador")]
	[SerializeField] private float _maxSpeed = 15f;

	[Tooltip("Fricción a la que se ve sometido el movimiento del jugador")]
	[SerializeField] private float _friction = 3f;

	[Tooltip("Factor de multiplicación aplicado al input.")]
	[SerializeField] private float _speed = 10f; // Multiplicador para la entrada del jugador

	private Vector3 _currentVelocity = Vector3.zero;

	public void SetMovementDirection(Vector2 direction)
	{
		if (direction.magnitude > 0)
		{
			Vector3 targetVelocity = new Vector3(direction.x, 0, direction.y) * _speed;
			_currentVelocity = Vector3.Lerp(_currentVelocity, targetVelocity, _acceleration * Time.deltaTime);
			_currentVelocity = Vector3.ClampMagnitude(_currentVelocity, _maxSpeed);
		}
		else
		{
			_currentVelocity = Vector3.Lerp(_currentVelocity, Vector3.zero, _friction * Time.deltaTime);
		}

		transform.Translate(_currentVelocity * Time.deltaTime, Space.World);
	}
	public void setSpeed(float s)
	{
        _speed = s;	

	}
    public float getSpeed()
    {
        return _speed;

    }
    public float getmaxSpeed()
    {
        return _maxSpeed;

    }
    public void setmaxSpeed(float s)
    {
        _maxSpeed = s;

    }
}
