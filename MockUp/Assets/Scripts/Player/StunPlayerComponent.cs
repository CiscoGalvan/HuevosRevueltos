using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunPlayerComponent : MonoBehaviour
{
    private bool _playerIsStuned;

    private float _stunnedTime;
    private float _time;
    [SerializeField] private GameObject _cacerola;
    void Start()
    {
        _playerIsStuned = false;
        _stunnedTime = 0f;
        _time = 0f;
	}

    // Update is called once per frame
    void Update()
    {
       
        if (_playerIsStuned)
        {
			_time += Time.deltaTime;
            if(_time >= _stunnedTime)
            {
                _playerIsStuned = false; _time = 0f; _stunnedTime = 0f;
			}
        }
    }
	private void OnCollisionEnter(Collision collision)
	{
        DamageEmitter damageEmitter = collision.gameObject.GetComponent<DamageEmitter>();
        if(damageEmitter != null)
        {
            if (damageEmitter.GetHitted() && gameObject.layer == LayerMask.NameToLayer(damageEmitter.GetElementToCollide().ToString()) 
                &&
                (_cacerola != null && !_cacerola.activeSelf))
			{
                ft_AudioManager.Instance.PlaySFX(ft_AudioManager.ft_AudioType.Stun, volume: 1.0f);
			
                SetPlayerStunned(true, damageEmitter.GetStunnedTime());

                Destroy(collision.gameObject);
                //Eliminar objeto 
			}
		}
		
	}

	private void SetPlayerStunned(bool newValue,float stunTime)
    {
        _playerIsStuned = newValue;
        _stunnedTime = stunTime;
    }
    public bool GetPlayerIsStunned() => _playerIsStuned;
}
