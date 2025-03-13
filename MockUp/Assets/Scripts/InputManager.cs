using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    private MovementComponent _playerOneMovement;
    private MovementComponent _playerTwoMovement;

    private Vector2 _playerOneMovementInput;
    private Vector2 _playerTwoMovementInput;
    // Start is called before the first frame update

    void Start()
    {
        _playerOneMovement = ReferenceManager.Instance.GetPlayerOne().GetComponent<MovementComponent>();
		_playerTwoMovement = ReferenceManager.Instance.GetPlayerTwo().GetComponent<MovementComponent>(); 
    }

	private void FixedUpdate()
	{
		#region PlayerOneInput
		_playerOneMovement.SetMovementDirection(_playerOneMovementInput);
		#endregion
		#region PlayerTwoInput
		//_playerTwoMovement.SetMovementDirection(_playerTwoMovementInput);
		#endregion

	}
	public void OnMovePlayerOne(InputAction.CallbackContext value)
	{
        _playerOneMovementInput = value.ReadValue<Vector2>();	
	}
	public void OnMovePlayerTwo(InputAction.CallbackContext value)
	{
		//_playerTwoMovementInput = value.ReadValue<Vector2>();
	}
}
