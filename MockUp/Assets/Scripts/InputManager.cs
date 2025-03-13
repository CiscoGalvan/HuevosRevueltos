using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
        #region PlayerOneInput
        _playerOneMovementInput.x = Input.GetAxis("HorizontalP1");
        _playerOneMovementInput.y = Input.GetAxis("VerticalP1");
        _playerOneMovement.SetMovementDirection(_playerOneMovementInput);
        #endregion



        #region PlayerTwoInput
        _playerTwoMovementInput.x = Input.GetAxis("HorizontalP2");
        _playerTwoMovementInput.y = Input.GetAxis("VerticalP2");
	
		_playerTwoMovement.SetMovementDirection(_playerTwoMovementInput);
        #endregion
    }
}
