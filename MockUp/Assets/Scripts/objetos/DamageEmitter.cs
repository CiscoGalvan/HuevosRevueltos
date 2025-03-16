using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEmitter : MonoBehaviour
{
    [SerializeField]
    private int _amountOfDamage = 10;

    
	[Tooltip("Tiempo que el jugador pasar� stunned en caso de que este objeto colisione con �l. Segundos")]
	[SerializeField]
	private float _stunnedTime;


    public bool _wasHitted = false;
    public enum KindOfObjectToCollideWith
    {
        Presa=0,
        PlayerOne=1,
        PlayerTwo=2,
    }
    [SerializeField]
    KindOfObjectToCollideWith _elementToCollide = KindOfObjectToCollideWith.Presa;
    
    private void OnCollisionEnter(Collision collision)
    {
        Life _life = collision.gameObject.GetComponent<Life>();
        if (_life != null )
        {
            if(_amountOfDamage > 0)
            {
				_life.Damage(_amountOfDamage);
			}
            else
            {
                _life.Heal(-_amountOfDamage);
            }

			Destroy(gameObject);
        }
    }

    public void SetElementToCollide(int value)
    {
        _elementToCollide = (KindOfObjectToCollideWith)value;
    }
    public KindOfObjectToCollideWith GetElementToCollide() => _elementToCollide;
    public float GetStunnedTime() => _stunnedTime;
    public void SetHittedObject(bool newValue)
    {
        _wasHitted = newValue;
    }
    public bool GetHitted() => _wasHitted;

    public int GetDamageAmount() => _amountOfDamage;
}
