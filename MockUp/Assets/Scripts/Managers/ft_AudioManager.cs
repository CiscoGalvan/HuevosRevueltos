using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[System.Serializable]
public class ft_Sound
{
	public ft_AudioManager.ft_AudioType audioType;
	public string eventPath;
	public float volume = 1;
}
public class ft_AudioManager : MonoBehaviour
{
	private static ft_AudioManager _instance;
	public static ft_AudioManager Instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject go = new GameObject("AudioManager");
				_instance = go.AddComponent<ft_AudioManager>();
				DontDestroyOnLoad(go);
			}
			return _instance;
		}
	}

	public enum ft_AudioType
	{
		Music,
		Ambience,
		Hit_Player1,
		Hit_Player2,
		Stun,
		Pan,
		DamHeal,
		DamDmg,
		Freeze,
		Berry,
		Start,
		Win,
		Rebound,
		Splash,
		Sink
	}
	public List<ft_Sound> soundList = new List<ft_Sound>();
	private Dictionary<ft_AudioType, string> soundDictionary;
	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return ;
		}
		soundDictionary = new Dictionary<ft_AudioType, string>();
		foreach (ft_Sound sound in soundList)
		{
			if (!soundDictionary.ContainsKey(sound.audioType))
				soundDictionary.Add(sound.audioType, sound.eventPath);
			else
				Debug.LogWarning("AudioManager: soundDictionary already contains " + sound.audioType);
		}
	}

	public void PlayMusic(ft_AudioType type, float volume = 1)
	{
		if (soundDictionary.ContainsKey(type))
		{
			string eventPath = soundDictionary[type];
			if (volume >= 0)
			{
				FMOD.Studio.EventInstance musicInstance = RuntimeManager.CreateInstance(eventPath);
				musicInstance.setParameterByName("Volume", volume);
				musicInstance.start();
				musicInstance.release();
			}
			else
				Debug.LogWarning("PlayMusic: Music is null or volume is less/equal than 0");
		}
		else
			Debug.LogWarning("PlayMusic: soundDictionary doesn't contain " + type);
	}
	public void PlayAmbience(ft_AudioType type, float volume = 1)
	{
		if (soundDictionary.ContainsKey(type))
		{
			string eventPath = soundDictionary[type];
			if (volume >= 0)
			{
				FMOD.Studio.EventInstance musicInstance = RuntimeManager.CreateInstance(eventPath);
				musicInstance.setParameterByName("Volume", volume);
				musicInstance.start();
				musicInstance.release();
			}
			else
				Debug.LogWarning("PlayAmbience: ambience is null or volume is less/equal than 0");
		}
		else
			Debug.LogWarning("PlayMusic: soundDictionary doesn't contain " + type);
	}
	public void PlaySFX(ft_AudioType type, float volume = 1)
	{
		if (soundDictionary.ContainsKey(type))
		{
			string eventPath = soundDictionary[type];
			if (volume >= 0)
			{
				FMOD.Studio.EventInstance musicInstance = RuntimeManager.CreateInstance(eventPath);
				musicInstance.setParameterByName("Volume", volume);
				musicInstance.start();
				musicInstance.release();
			}
			else
				Debug.LogWarning("PlaySFX: sfx is null or volume is less/equal than 0");
		}
		else
			Debug.LogWarning("PlayMusic: soundDictionary doesn't contain " + type);
	}
}
