using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputComponent : MonoBehaviour
{
	private MovementComponent _playerMovement;
	private Vector2 _playerMovementInput;
	
	void Start()
    {
		_playerMovement = GetComponent<MovementComponent>();
	}

	private void FixedUpdate()
	{
		_playerMovement.SetMovementDirection(_playerMovementInput);
	}

	public void OnMovePlayer(InputAction.CallbackContext value)
	{
		_playerMovementInput = value.ReadValue<Vector2>();
	}
}
