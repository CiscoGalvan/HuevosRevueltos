using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ft_Pickup : MonoBehaviour
{
	public Animator characterAnimator;
	public string pickupAnim = "Pickup";
	private GameObject stick;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("stick"))
			stick = other.gameObject;
    }
	
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("stick"))
			stick = null;
	}
	
	void Update()
	{
		if (stick != null && Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
		{
			if (characterAnimator != null)
				characterAnimator.Play(pickupAnim);
			Destroy(stick);
			stick = null;
		}
	}
}