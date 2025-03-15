using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VibrationManager : MonoBehaviour
{
	private static VibrationManager _instance;
	private Gamepad _gamepad;
	private Coroutine _stopRumbleAfterTimeCoroutine;
	public static VibrationManager Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject singletonObject = new GameObject("RefernceManager");
				_instance = singletonObject.AddComponent<VibrationManager>();
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

	public void RumblePulse(float lowFrequency,float highFrequency,float duration)
	{
		_gamepad = Gamepad.current;
		if (_gamepad != null)
		{
			_gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
			_stopRumbleAfterTimeCoroutine = StartCoroutine(StopRumble(duration));
		}
	}

	private IEnumerator StopRumble(float duration)
	{
		float elapsedTime = 0f;
		while(elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			yield return null; 
		}
		_gamepad.SetMotorSpeeds(0f, 0f);
	}

}
