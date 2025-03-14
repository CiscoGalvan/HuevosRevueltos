using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateHitDirection : MonoBehaviour
{
    [SerializeField]
    LayerMask mask;



    [SerializeField]
    private float _hitStrength;
	
	private Vector3 previousPosition;
	private Vector3 currentVelocity;
	private void Start()
	{
		previousPosition = transform.position;
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
