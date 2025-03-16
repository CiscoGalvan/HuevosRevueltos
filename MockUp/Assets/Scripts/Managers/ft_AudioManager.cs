using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Unity.VisualScripting;

[Serializable]
public class ft_Sound
{
	public ft_AudioManager.ft_AudioType audioType;
	public string eventPath;
	public float volume = 1;
}

public class ft_AudioManager : MonoBehaviour
{
	private FMOD.Studio.EventInstance currentMusicInstance;
	private FMOD.Studio.EventInstance currentSFXInstance;
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
		Start2,
		Start3,
		Start4,
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
			return;
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

	public void StopMusic()
	{
		if (currentMusicInstance.isValid())
		{
			currentMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			currentMusicInstance.release();
		}
	}

	public void StopSFX()
	{
		if (currentSFXInstance.isValid())
		{
			currentSFXInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			currentSFXInstance.release();
		}
	}

	public void PlayMusic(ft_AudioType type, float volume = 1)
	{
		StopMusic();
		if (soundDictionary.ContainsKey(type))
		{
			string eventPath = soundDictionary[type];
			if (volume >= 0)
			{
				currentMusicInstance = RuntimeManager.CreateInstance(eventPath);
				currentMusicInstance.setParameterByName("Volume", volume);
				currentMusicInstance.start();
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
				FMOD.Studio.EventInstance ambienceInstance = RuntimeManager.CreateInstance(eventPath);
				ambienceInstance.setParameterByName("Volume", volume);
				ambienceInstance.start();
				ambienceInstance.release();
			}
			else
				Debug.LogWarning("PlayAmbience: ambience is null or volume is less/equal than 0");
		}
		else
			Debug.LogWarning("PlayAmbience: soundDictionary doesn't contain " + type);
	}

	public void PlaySFX(ft_AudioType type, float volume = 1)
	{
		StopSFX();
		if (soundDictionary.ContainsKey(type))
		{
			string eventPath = soundDictionary[type];
			if (volume >= 0)
			{
				currentSFXInstance = RuntimeManager.CreateInstance(eventPath);
				currentSFXInstance.setParameterByName("Volume", volume);
				currentSFXInstance.start();
			}
			else
				Debug.LogWarning("PlaySFX: sfx is null or volume is less/equal than 0");
		}
		else
			Debug.LogWarning("PlaySFX: soundDictionary doesn't contain " + type);
	}

	void Update(){
		if(Input.GetKey(KeyCode.Space)){
			ft_AudioManager.Instance.PlaySFX(ft_AudioType.Hit_Player1, volume: 1.0f);
		}
	}
}
