using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; }

	[Header("Audio Clips")]
	[SerializeField] private List<AudioClip> soundTracks; // Lista de sonidos

	private Dictionary<string, AudioClip> soundLibrary = new Dictionary<string, AudioClip>();
	private AudioSource audioSource;
	private string currentScene;
	private bool hasPlayedSoundInScene = false; // Controla si ya se ejecutó un sonido

	[Header("Audio Settings")]
	[Range(0, 1)][SerializeField] private float volume = 0.5f;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		// Añadir un AudioSource si no existe
		audioSource = GetComponent<AudioSource>();
		if (audioSource == null)
		{
			audioSource = gameObject.AddComponent<AudioSource>();
		}

		audioSource.volume = volume;

		// Llenar el diccionario con los sonidos
		foreach (var clip in soundTracks)
		{
			soundLibrary[clip.name] = clip;
		}

		// Suscribirse al cambio de escenas
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		if (currentScene != scene.name)
		{
			currentScene = scene.name;
			hasPlayedSoundInScene = false; // Resetear para la nueva escena
		}
	}

	// 🎵 Reproduce un sonido solo una vez por escena
	public void PlaySoundOnce(string soundName)
	{
		if (!hasPlayedSoundInScene && soundLibrary.ContainsKey(soundName))
		{
			audioSource.clip = soundLibrary[soundName];
			audioSource.Play();
			hasPlayedSoundInScene = true; // Evita que se reproduzca de nuevo en la misma escena
		}
	}

	// 🎚 Cambia el volumen
	public void SetVolume(float newVolume)
	{
		volume = Mathf.Clamp01(newVolume);
		audioSource.volume = volume;
	}

	private void OnDestroy()
	{
		// Desuscribirse para evitar errores
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}
