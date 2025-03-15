using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveStickCollider : MonoBehaviour
{
    
    private enum HittingDirection
    {
        Left = 0,
        Right = 1
    }

	[Tooltip("Punto desde el que pivota el MovingGameObject")]
	[SerializeField]
    private Transform _pivot;

	[Tooltip("Objeto que va a moverse alrededor del castor")]
	[SerializeField]
    private GameObject _movingGameObject;
    
    private HittingDirection _direction;
    private ft_Pickup _pickUpObject;

    private bool _isMoving;
    private bool _inputDetected;

	[Tooltip("Tiempo entre un movimiento a un lado y al otro lado")]
	[SerializeField]
    private float _movimientoDuracion;

	[Tooltip("Grado desde donde empieza el movimiento de derecha")]
	[SerializeField] private float _gradoInicial = 0f;

	[Tooltip("Grado hasta el que llega el movimiento de derecha")]
	[SerializeField] private float _gradoGolpeDerecha = 45f;

	[Tooltip("Grado desde donde empieza el movimiento de izquierda")]
	[SerializeField] private float _gradoInicialGolpeIzquierda = -10f;

	[Tooltip("Grado hasta el que llega el movimiento de izquierda")]
	[SerializeField] private float _gradoGolpeIzquierda = -45f;

	private float _moveElapsedTime;
	private float _startRotation;
	private float _targetRotation;

	private Collider _movingObjectCollider;

	private float _resetComboTimer = 0f;

	[Tooltip("Tiempo necesario para que se resetee el combo de dos golpes")]
	[SerializeField]
	private float _timeNeededToResetCombo;
	// Start is called before the first frame update
	void Start()
    {
        _direction = HittingDirection.Right;
        _isMoving = false;
        _pickUpObject = GetComponent<ft_Pickup>();
		_pivot.rotation = Quaternion.Euler(0, _gradoInicial, 0);
		_movingObjectCollider = _movingGameObject.GetComponent<Collider>(); 
		_movingObjectCollider.enabled = false;
		_resetComboTimer = 0f;
	}

    // Update is called once per frame
    void Update()
    {
		//Debug.Log(_resetComboTimer);
		if (_isMoving)
		{
			_moveElapsedTime += Time.deltaTime;
			float t = Mathf.Clamp01(_moveElapsedTime / _movimientoDuracion);
			float newY = Mathf.Lerp(_startRotation, _targetRotation, t);
			_pivot.rotation = Quaternion.Euler(0, newY, 0);

			if (_moveElapsedTime >= _movimientoDuracion)
			{
				
				_pivot.rotation = Quaternion.Euler(0, _targetRotation, 0);
				_isMoving = false;
				_inputDetected = false;
				_direction = _direction == HittingDirection.Right ? HittingDirection.Left : HittingDirection.Right;
				SetMovingObjectCollider(false);
			}
		}
		else if (_inputDetected)
		{
			switch (_direction)
			{
				case HittingDirection.Left:
					_startRotation = _gradoInicialGolpeIzquierda;
					_targetRotation = _gradoGolpeIzquierda;
					break;
				case HittingDirection.Right:
					_startRotation = _gradoInicial;
					_targetRotation = _gradoGolpeDerecha;
					break;
			}
			_moveElapsedTime = 0f;
			_isMoving = true;
			//ESTO DEBE SER LANZADO POR LA ANIMACI�N.
			SetMovingObjectCollider(true);
		}
		else
		{
			
			if (_direction == HittingDirection.Left)
			{
				//Debug.Log(_resetComboTimer);
				_resetComboTimer += Time.deltaTime;
				if (_resetComboTimer >= _timeNeededToResetCombo)
				{
					_direction = HittingDirection.Right;
					_resetComboTimer = 0f;
				}
			}
		}
	}
    public void HitWithStick(InputAction.CallbackContext value)
    {
        if (!_pickUpObject.GetPickedUpObject()) return;
        if (value.performed) _inputDetected = true;
	}

	public void SetMovingObjectCollider(bool newValue)
	{
		//Cuidado por si se llama m�s veces de la cuenta
		_movingObjectCollider.enabled = newValue;
	}
}
