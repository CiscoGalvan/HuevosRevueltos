using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayerPoint : MonoBehaviour
{
	[SerializeField]
	private Transform _winningPoint;

	[SerializeField]
	private Transform _cameraNewPosition;
	[SerializeField]
	private float _moveSpeed = 2f; // Velocidad de movimiento de la cámara
	[SerializeField]
	private float _rotationSpeed = 2f; // Velocidad de rotación progresiva

	private bool _isMoving = false;
	private bool _isRotating = false; // Nueva variable para controlar la rotación progresiva
	private GameObject _targetObject;
	[SerializeField]
	private float _cameraDistance;

	public void MoveObjectToWinningPoint(GameObject target)
	{
		_targetObject = target;
		_targetObject.transform.position = _winningPoint.position;
		_isMoving = true;
		_isRotating = false; // Asegurarse de que la rotación aún no comience
	}

	void Update()
	{
		if (_isMoving && _targetObject != null)
		{
			// Mover la cámara con Lerp hacia la nueva posición
			transform.position = Vector3.Lerp(transform.position, _cameraNewPosition.position, Time.deltaTime * _moveSpeed);

			// Si la cámara está lo suficientemente cerca de la posición final
			if (Vector3.Distance(transform.position, _cameraNewPosition.position) < _cameraDistance)
			{
				_isMoving = false; // Detener el movimiento
				_isRotating = true; // Activar rotación progresiva
			}
		}

		if (_isRotating && _targetObject != null)
		{
			// Obtener la rotación deseada hacia el objeto
			Quaternion targetRotation = Quaternion.LookRotation(_targetObject.transform.position - transform.position);

			// Interpolar suavemente hacia la rotación deseada
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);

			// Detener la rotación cuando esté lo suficientemente cerca de la rotación deseada
			if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
			{
				_isRotating = false;
			}
		}
	}
}
