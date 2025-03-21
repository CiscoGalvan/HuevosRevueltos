﻿using System.Collections;
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

    [SerializeField] private GameObject freezeEffect;
    [SerializeField] private GameObject freezeAura;
    [SerializeField] private GameObject speedEffect;

    [SerializeField] private GameObject SpeedPoint;
    [SerializeField] private GameObject AuraPoint;

    private GameObject _particleGameObject1;
    private GameObject _particleGameObject2;
    private GameObject _particleGameObject3;

    private Vector3 _currentVelocity = Vector3.zero;

    private float baseSpeed;
    private float baseMaxSpeed;

    public bool isSpeedModified = false;

    private StunPlayerComponent _stunPlayerComponent;

	private Rigidbody _rb;

	private void Start()
	{
		_stunPlayerComponent = GetComponent<StunPlayerComponent>();
		_rb = GetComponent<Rigidbody>(); // Obtener el Rigidbody
		baseSpeed = _speed;
		baseMaxSpeed = _maxSpeed;
	}

	public void SetMovementDirection(Vector2 direction)
	{
		if (direction.magnitude > 0 && !_stunPlayerComponent.GetPlayerIsStunned())
		{
			Vector3 targetVelocity = new Vector3(direction.x, 0, direction.y) * _speed;
			_currentVelocity = Vector3.Lerp(_currentVelocity, targetVelocity, _acceleration * Time.deltaTime);
			_currentVelocity = Vector3.ClampMagnitude(_currentVelocity, _maxSpeed);
		}
		else
		{
			_currentVelocity = Vector3.Lerp(_currentVelocity, Vector3.zero, _friction * Time.deltaTime);
		}

		_rb.velocity = _currentVelocity; // Usar Rigidbody para el movimiento

		if (_currentVelocity.magnitude > 0.05f)
		{
			Quaternion targetRotation = Quaternion.LookRotation(_currentVelocity.normalized);
			float rotationOffset = (gameObject.layer == LayerMask.NameToLayer("PlayerOne")) ? -90f : 90f;
			Quaternion adjustedRotation = targetRotation * Quaternion.Euler(0, rotationOffset, 0);
			transform.rotation = adjustedRotation;
		}
	}


	// Este método modifica la velocidad y muestra efectos gráficos
	public void setSpeed(float s, bool increase)
    {
        _speed = s;

        if (!increase)
        {
            _particleGameObject1 = Instantiate(freezeEffect, SpeedPoint.transform.position, freezeEffect.transform.rotation);
            _particleGameObject1.transform.SetParent(SpeedPoint.transform, worldPositionStays: true);
            Destroy(_particleGameObject1, 2.5f);
            _particleGameObject2 = Instantiate(freezeAura, AuraPoint.transform.position, freezeAura.transform.rotation);
            _particleGameObject2.transform.SetParent(AuraPoint.transform, worldPositionStays: true);
            Destroy(_particleGameObject2, 2.5f);
        }
        else
        {
            _particleGameObject3 = Instantiate(speedEffect, SpeedPoint.transform.position, speedEffect.transform.rotation);
            _particleGameObject3.transform.SetParent(SpeedPoint.transform);
            Destroy(_particleGameObject3, 2.5f);
        }
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
    public void ResetSpeed()
    {
        _speed = baseSpeed;
        _maxSpeed = baseMaxSpeed;
        isSpeedModified = false;
    }
}
