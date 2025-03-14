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

    [SerializeField]
    private Transform _pivot;
    [SerializeField]
    private GameObject _movingGameObject;
    
    private HittingDirection _direction;
    private ft_Pickup _pickUpObject;

    private bool _isMoving;
    private bool _inputDetected;
    [SerializeField]
    private float _movimientoDuracion;

	[SerializeField] private float _gradoInicial = 0f;
	[SerializeField] private float _gradoGolpeDerecha = 45f;
	[SerializeField] private float _gradoInicialGolpeIzquierda = -10f;
	[SerializeField] private float _gradoGolpeIzquierda = -45f;

	private float _moveElapsedTime;
	private float _startRotation;
	private float _targetRotation;

	private Collider _movingObjectCollider;
	// Start is called before the first frame update
	void Start()
    {
        _direction = HittingDirection.Right;
        _isMoving = false;
        _pickUpObject = GetComponent<ft_Pickup>();

		// Inicializamos la rotación del pivot en el ángulo inicial
		_pivot.rotation = Quaternion.Euler(0, _gradoInicial, 0);

		_movingObjectCollider = _movingGameObject.GetComponent<Collider>(); 
		_movingObjectCollider.enabled = false;
	}

    // Update is called once per frame
    void Update()
    {
		// Si se está realizando un movimiento, se actualiza la interpolación en cada frame
		if (_isMoving)
		{
			_moveElapsedTime += Time.deltaTime;
			float t = Mathf.Clamp01(_moveElapsedTime / _movimientoDuracion);
			float newY = Mathf.Lerp(_startRotation, _targetRotation, t);
			_pivot.rotation = Quaternion.Euler(0, newY, 0);

			if (_moveElapsedTime >= _movimientoDuracion)
			{
				// Finaliza el movimiento, se fija la rotación final y se alterna la dirección
				_pivot.rotation = Quaternion.Euler(0, _targetRotation, 0);
				_isMoving = false;
				_inputDetected = false;
				_direction = _direction == HittingDirection.Right ? HittingDirection.Left : HittingDirection.Right;
				SetMovingObjectCollider(false);
			}
		}
		// Si no se está moviendo y se ha detectado input, se inicia el movimiento
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
			//ESTO DEBE SER LANZADO POR LA ANIMACIÓN.
			SetMovingObjectCollider(true);
		}
	}
    public void HitWithStick(InputAction.CallbackContext value)
    {
        if (!_pickUpObject.GetPickedUpObject()) return;
        if (value.performed) _inputDetected = true;
	}

	public void SetMovingObjectCollider(bool newValue)
	{
		//Cuidado por si se llama más veces de la cuenta
		_movingObjectCollider.enabled = newValue;
	}
}
