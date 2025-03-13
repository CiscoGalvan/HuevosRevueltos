using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
	[SerializeField]
	private GameObject _playerOne;
	[SerializeField]
	private GameObject _playerTwo;


	private static ReferenceManager _instance;

	public static ReferenceManager Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject singletonObject = new GameObject("RefernceManager");
				_instance = singletonObject.AddComponent<ReferenceManager>();
				DontDestroyOnLoad(singletonObject);
			}
			return _instance;
		}
	}

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (_instance != this)
		{
			Destroy(gameObject);
		}
	}

	public GameObject GetPlayerOne() => _playerOne;
	public GameObject GetPlayerTwo() => _playerTwo;
}
