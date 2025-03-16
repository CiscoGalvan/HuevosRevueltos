using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private GameOverScreen  gameOverScreen;
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject singletonObject = new GameObject("GameManager");
                _instance = singletonObject.AddComponent<GameManager>();
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
    
	public void Start()
    {
		if(SceneManager.GetActiveScene().name == "initialMenu")
            ft_AudioManager.Instance.PlayMusic(ft_AudioManager.ft_AudioType.Ambience, volume: 1.0f);
    }

    public void EndScene(GameObject g)
    {
        if (g.CompareTag("presa1"))
        {
            GameOver(true);
        }
        else if (g.CompareTag("presa2"))
        {
            GameOver(false);
        }
    }
    public void GameOver(bool isPlayer1) {
      
        gameOverScreen.initScreen(isPlayer1);
    }

    public void SetCastorMovement(bool status)
    {
        GameObject Player1 = GameObject.Find("Player1");
        GameObject Player2 = GameObject.Find("Player2");
        Player1.GetComponent<MovementComponent>().enabled = status;
        Player1.GetComponent<PlayerInputComponent>().enabled = status;
        Player2.GetComponent<PlayerInputComponent>().enabled = status;
        /*
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();
        for(int i = 0; i < allGameObjects.Length; i++)
        {
            allGameObjects[i].SetActive(status);
        }
        */
        //GameObject Fondo = GameObject.Find("Fondo");
        //GameObject Countdown = GameObject.Find("CountdownCanvas");
        //Fondo.SetActive(true);
        //Countdown.SetActive(true);
    }

	private void Update()
	{
        if(SceneManager.GetActiveScene().name == "initialMenu")
        {
            if((Keyboard.current.anyKey.wasPressedThisFrame) || (Gamepad.current != null && AnyGamepadButtonPressed()))
            {
				InitGame();
			}
        }
	}
    private bool AnyGamepadButtonPressed()
    {
        return Gamepad.current.buttonSouth.wasPressedThisFrame || 
            Gamepad.current.buttonNorth.wasPressedThisFrame || 
            Gamepad.current.buttonWest.wasPressedThisFrame || 
            Gamepad.current.buttonEast.wasPressedThisFrame || 
            Gamepad.current.startButton.wasPressedThisFrame ||
            Gamepad.current.selectButton.wasPressedThisFrame ||
            Gamepad.current.leftShoulder.wasPressedThisFrame ||
            Gamepad.current.rightShoulder.wasPressedThisFrame ||
            Gamepad.current.leftTrigger.wasPressedThisFrame ||
            Gamepad.current.rightTrigger.wasPressedThisFrame;
	}
	// M�todo para reiniciar el juego cargando la escena "Game"
	private void InitGame()
    {
        SceneManager.LoadScene("Game");  
    }
}
