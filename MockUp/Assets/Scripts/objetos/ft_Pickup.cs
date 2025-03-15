using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ft_Pickup : MonoBehaviour
{
	/*Player Animator reference*/
	[SerializeField]
	private Animator characterAnimator;
	/*Trigger Animation Name*/
	[SerializeField]
	private string pickupAnim = "Pickup";
	private GameObject stick;
	private bool _objectPickedUp = false;

	/*we dectect when a object with the tag "stick" collides*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("stick"))
		{
			stick = other.gameObject;
			Outline outline = other.gameObject.GetComponent<Outline>();
			outline.enabled = true;
		}
			
    }
	/*when the object with the tag stick gets out, we nullify the reference*/
	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("stick") && stick == other.gameObject)
		{
			stick = null;
			Outline outline = other.gameObject.GetComponent<Outline>();
			outline.enabled = false;
		}
			
	}

	/*checks if the item is at range to pickup and if we have an animation for it*/
	//void Update()
	//{
	//	if (stick != null && Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return))
	//	{
			//if (characterAnimator != null)
			//			characterAnimator.Play(pickupAnim);
			//		Destroy(stick);
			//stick = null;
	//	}
	//}

	public void WantToPickUp(InputAction.CallbackContext value)
	{
		if(stick && value.performed)
		{
			if (characterAnimator != null)
				characterAnimator.Play(pickupAnim);
			Destroy(stick);
			_objectPickedUp = true;
			stick = null;
		}
	}

	public bool GetPickedUpObject() => _objectPickedUp;
	public void SetPickedUpObject(bool newValue)
	{
		_objectPickedUp = newValue;
	} 
}