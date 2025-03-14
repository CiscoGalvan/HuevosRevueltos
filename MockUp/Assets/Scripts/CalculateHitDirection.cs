using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateHitDirection : MonoBehaviour
{
    [SerializeField]
    LayerMask mask;



    [SerializeField]
    private float _hitStrength;
	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log("A");

		//if (((1 << collision.gameObject.layer) & mask) != 0)
		//{
		
		//	ContactPoint contact = collision.contacts[0];
		//	Vector3 normalColision = contact.normal;

		//	Debug.Log("Normal de la colisión: " + normalColision);
		//}
	}
	private void OnCollisionStay(Collision collision)
	{
		Debug.Log("B");
	}
	
	// Start is called before the first frame update
	void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
