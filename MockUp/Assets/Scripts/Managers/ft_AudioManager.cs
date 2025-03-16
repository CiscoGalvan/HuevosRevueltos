using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ft_Sound
{
	public ft_AudioManager.ft_AudioType audioType;
	public AudioClip Sound;
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

	private AudioSource musicSource;
	private AudioSource sfxSource;
	private AudioSource AmbienceSource;

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
	private Dictionary<ft_AudioType, AudioClip> soundDictionary;
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
		musicSource = gameObject.AddComponent<AudioSource>();
		sfxSource = gameObject.AddComponent<AudioSource>();
		AmbienceSource = gameObject.AddComponent<AudioSource>();
		musicSource.loop = true;
		AmbienceSource.loop = true;
		soundDictionary = new Dictionary<ft_AudioType, AudioClip>();
		foreach (ft_Sound sound in soundList)
		{
			if (!soundDictionary.ContainsKey(sound.audioType))
				soundDictionary.Add(sound.audioType, sound.Sound);
			else
				Debug.LogWarning("AudioManager: soundDictionary already contains " + sound.audioType);
		}
	}

	public void PlayMusic(ft_AudioType type, float volume = 1)
	{
		if (soundDictionary.ContainsKey(type))
		{
			AudioClip musicClip = soundDictionary[type];
			if (musicClip != null || volume >= 0)
			{
				musicSource.clip = musicClip;
				musicSource.volume = volume;
				musicSource.Play();
			}
			else
				Debug.LogWarning("PlayMusic: musicClip is null or volume is less/equal than 0");
		}
		else
			Debug.LogWarning("PlayMusic: soundDictionary doesn't contain " + type);
	}
	public void StopMusic()
	{
		musicSource.Stop();
	}
	public void PlayAmbience(ft_AudioType type, float volume = 1)
	{
		if (soundDictionary.ContainsKey(type))
		{
			AudioClip ambienceClip = soundDictionary[type];
			if (ambienceClip != null || volume >= 0)
			{
				AmbienceSource.clip = ambienceClip;
				AmbienceSource.volume = volume;
				AmbienceSource.Play();
			}
			else
				Debug.LogWarning("PlayAmbience: ambienceClip is null or volume is less/equal than 0");
		}
		else
			Debug.LogWarning("PlayMusic: soundDictionary doesn't contain " + type);
	}
	public void StopAmbience()
	{
		AmbienceSource.Stop();
	}
	public void PlaySFX(ft_AudioType type, float volume = 100)
	{
		if (soundDictionary.ContainsKey(type))
		{
			AudioClip sfxClip = soundDictionary[type];
			if (sfxClip != null || volume >= 0)
				sfxSource.PlayOneShot(sfxClip, volume);
			else
				Debug.LogWarning("PlaySFX: sfxClip is null or volume is less/equal than 0");
		}
		else
			Debug.LogWarning("PlayMusic: soundDictionary doesn't contain " + type);
	}
}