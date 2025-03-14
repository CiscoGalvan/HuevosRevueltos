using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ft_Current : MonoBehaviour
{
	public float RiverCurrentSpeed = 25f;
	private float weakSpeedFactor = 0.2f;
	public float centerZoneThreshold = 0.3f;
	private float acceleration = 5f;
	private float riverLeft;
	private float riverRight;

	/*Initializes the boundaries of the river area using the BoxCollider.*/
	private void Start()
	{
		BoxCollider box = GetComponent<BoxCollider>();
		if (box != null)
		{
			Vector3 worldCenter = transform.TransformPoint(box.center);
			Vector3 worldSize = Vector3.Scale(box.size, transform.lossyScale);
			riverLeft = worldCenter.x - worldSize.x / 2f;
			riverRight = worldCenter.x + worldSize.x / 2f;
		}
	}

	/*Applies a uniform current force to objects with a Rigidbody within the river area.
	In the weak zones, a lower current allows free movement.
	In the center zone, a high current force prevents the player from passing.*/
	private void OnTriggerStay(Collider other)
	{
		Rigidbody rb = other.attachedRigidbody;
		if (rb == null)
			return ;
		float posX = other.transform.position.x;
		float centerX = (riverLeft + riverRight) / 2f;
		float halfWidth = (riverRight - riverLeft) / 2f;
		float offset = posX - centerX;
		float normalizedDistance = Mathf.Abs(offset) / halfWidth;
		float targetSpeed = 0f;
		if (normalizedDistance <= centerZoneThreshold)
			targetSpeed = RiverCurrentSpeed;
		else
			targetSpeed = RiverCurrentSpeed * weakSpeedFactor;
		float direction = (offset >= 0f) ? 1f : -1f;
		float desiredVelocityX = direction * targetSpeed;
		float newVelX = Mathf.MoveTowards(rb.velocity.x, desiredVelocityX, acceleration * Time.deltaTime);
		rb.velocity = new Vector3(newVelX, rb.velocity.y, rb.velocity.z);
	}
}

