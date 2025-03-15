using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CalculateHitDirection : MonoBehaviour
{
    [SerializeField]
    LayerMask mask;



    [SerializeField]
    private float _hitStrength;
	
	private Vector3 previousPosition;
	private Vector3 currentVelocity;
	[SerializeField]
	private GameObject _particlePrefab;

	private GameObject _particleGameObject;

	private Gamepad _gamepad;

	[SerializeField]
	private float _rumbleLowFrequency;


	[SerializeField]
	private float _rumbleHighFrequency;



	[SerializeField]
	private float _rumbleTime;
	private void Start()
	{
		previousPosition = transform.position; _gamepad = Gamepad.current;
	}

	private void FixedUpdate()
	{
		
		Vector3 currentPosition = transform.position;
		currentVelocity = (currentPosition - previousPosition) / Time.fixedDeltaTime;
		previousPosition = currentPosition;
	}
	private void OnTriggerEnter(Collider other)
	{
	
		if (((1 << other.gameObject.layer) & mask) != 0)
		{
			Rigidbody rb = other.GetComponent<Rigidbody>();
			if (rb != null)
			{
				
				Vector3 hitDirection;
				Vector3 hitPosition = other.ClosestPoint(other.gameObject.transform.position);
				if(_gamepad != null)
				{
					VibrationManager.Instance.RumblePulse(_rumbleLowFrequency, _rumbleHighFrequency,_rumbleTime);
				}
				if(_particlePrefab != null)
				{
					_particleGameObject = Instantiate(_particlePrefab, hitPosition, Quaternion.identity);
					Destroy(_particleGameObject, 1);
				}
	
				if (currentVelocity.magnitude > 0.1f)
				{
					hitDirection = currentVelocity.normalized;
				}
				else
				{
					
					hitDirection = (other.transform.position - transform.position).normalized;
				}
				rb.velocity = hitDirection * _hitStrength;
			}
		}
	} 
}
