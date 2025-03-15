using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
			Destroy(gameObject);
		musicSource = gameObject.AddComponent<AudioSource>();
		sfxSource = gameObject.AddComponent<AudioSource>();
		AmbienceSource = gameObject.AddComponent<AudioSource>();
		musicSource.loop = true;
		AmbienceSource.loop = true;
	}

	public void PlayMusic(AudioClip musicClip, float volume = 1)
	{
		if (musicClip != null || volume >= 0)
		{
			musicSource.clip = musicClip;
			musicSource.volume = volume;
			musicSource.Play();
		}
		else
			Debug.LogWarning("PlayMusic: musicClip is null or volume is less/equal than 0");
	}
	public void StopMusic()
	{
		musicSource.Stop();
	}
	public void PlayAmbience(AudioClip ambienceClip, float volume = 1)
	{
		if (ambienceClip != null || volume >= 0)
		{
			AmbienceSource.clip = ambienceClip;
			AmbienceSource.volume = volume;
			AmbienceSource.Play();
		}
		else
			Debug.LogWarning("PlayAmbience: ambienceClip is null or volume is less/equal than 0");
	}
	public void StopAmbience()
	{
		AmbienceSource.Stop();
	}
	public void PlaySFX(AudioClip sfxClip, float volume = 100)
	{
		if (sfxClip != null || volume >= 0)
			sfxSource.PlayOneShot(sfxClip, volume);
		else
			Debug.LogWarning("PlaySFX: sfxClip is null or volume is less/equal than 0");
	}
}
