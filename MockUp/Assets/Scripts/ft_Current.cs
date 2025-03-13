using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverCurrentDiverging : MonoBehaviour
{
	public float maxCurrentForce = 10f;
	private float boxLeft;
	private float boxRight;

	private void Start()
	{
		BoxCollider box = GetComponent<BoxCollider>();
		if (box != null)
		{
			Vector3 boxCenter = transform.TransformPoint(box.center);
			Vector3 boxSize = Vector3.Scale(box.size, transform.lossyScale);
			boxLeft = boxCenter.x - boxSize.x / 2f;
			boxRight = boxCenter.x + boxSize.x / 2f;
		}
		else
			Debug.LogWarning("there is no box collider in" + gameObject.name);
	}

	private void OnTriggerStay(Collider other)
	{
		Rigidbody rb = other.GetComponent<Rigidbody>();
		float objectX = other.transform.position.x;
		float centerX = (boxLeft + boxRight) / 2f;
		float halfWidth = (boxRight - boxLeft) / 2f;
		float offset = objectX - centerX;
		float normalizedDistance = Mathf.Abs(offset) / halfWidth;
		float forceMagnitude = maxCurrentForce * (1f - normalizedDistance);
		float direction = (offset >= 0f) ? 1f : -1f;
		Vector3 force = new Vector3(direction * forceMagnitude, 0, 0);
		rb.AddForce(force, ForceMode.Force);
	}
}
