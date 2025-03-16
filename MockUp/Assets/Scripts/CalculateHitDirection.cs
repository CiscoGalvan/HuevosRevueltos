using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CalculateHitDirection : MonoBehaviour
{
	//Necesario para ver que Player le pega a la lata
	private enum NumberOfPlayer
	{
		PlayerOne = 1,
		PlayerTwo = 2
	}

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
	public AudioClip HitSfx;
	[SerializeField]
	private float hitVolume = 1f;
	[Tooltip("Necesario para saber que jugador ha golpeado la lata")]
	[SerializeField]
	private NumberOfPlayer _playerNumber;


	// Par�metro adicional para controlar el spin
	[SerializeField]
	private float spinStrength = 5f; 

	[SerializeField]
	private float _rumbleTime;
	private void Start()
	{
		 _gamepad = Gamepad.current;
	}
	private void OnCollisionEnter(Collision collision)
	{
		DamageEmitter damageEmitter = collision.gameObject.GetComponent<DamageEmitter>();

		if(damageEmitter != null)
		{
			Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
			if (rb != null)
			{
				Vector3 hitDirection;
				Vector3 hitPosition = collision.collider.ClosestPoint(collision.gameObject.transform.position);
				if (_gamepad != null)
				{
					VibrationManager.Instance.RumblePulse(_rumbleLowFrequency, _rumbleHighFrequency, _rumbleTime);
				}
				if (_particlePrefab != null)
				{
					_particleGameObject = Instantiate(_particlePrefab, hitPosition, Quaternion.identity);
					Destroy(_particleGameObject, 1.5f);
					Debug.Log("generacion de particulas");
				}

				// Si la lata no ha sido lanzada, la marco como lanzada y seteo su objetivo
				if (!damageEmitter.GetHitted())
				{
					damageEmitter.SetElementToCollide((int)_playerNumber == 1 ? 2 : 1);
					damageEmitter.SetHittedObject(true);
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

				
                // Aseg�rate de que el Rigidbody tenga configurado un valor bajo de "Angular Drag"
                rb.angularDrag = 0.05f;  // Esto deber�a permitir un giro suave.

                // Aplicar giro (torque) para el "spin"
                Vector3 spinDirection = Vector3.Cross(hitDirection, Vector3.up); // Direcci�n perpendicular para el giro
                rb.AddTorque(spinDirection * spinStrength, ForceMode.Impulse);
			}
		}
	}
}

