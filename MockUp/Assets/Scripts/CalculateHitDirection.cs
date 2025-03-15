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
		 _gamepad = Gamepad.current;
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (((1 << collision.gameObject.layer) & mask) != 0)
		{
			Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
			if (rb != null)
			{
				Debug.Log("Collided");

				Vector3 hitDirection;
				Vector3 hitPosition = collision.collider.ClosestPoint(collision.gameObject.transform.position);
				if (_gamepad != null)
				{
					VibrationManager.Instance.RumblePulse(_rumbleLowFrequency, _rumbleHighFrequency, _rumbleTime);
				}
				if (_particlePrefab != null)
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

					hitDirection = (collision.gameObject.transform.position - transform.position).normalized;
				}
				rb.velocity = hitDirection * _hitStrength;
			}
		}
	}

}
