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

    
    private GameObject _playerOne;
    private GameObject _playerTwo;
    private GameObject _spawner;
    private bool finishedGame = false;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject singletonObject = new GameObject("GameManager");
                _instance = singletonObject.AddComponent<GameManager>();
                //DontDestroyOnLoad(singletonObject);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "initialMenu")
            ft_AudioManager.Instance.PlayMusic(ft_AudioManager.ft_AudioType.Ambience, volume: 1.0f);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    public void EndScene(GameObject g)
    {
        Debug.Log("Fin de partida");
        if(!finishedGame) {
            finishedGame = true;
            if (g.CompareTag("presa1"))
            {
                GameOver(false);
            }
            else if (g.CompareTag("presa2"))
            {
                GameOver(true);
            }
        }
    }
    public void GameOver(bool isPlayer1)
    {
        ft_AudioManager.Instance.PlaySFX(ft_AudioManager.ft_AudioType.Win, volume: 1.0f);
		MoveToPlayerPoint playerPoint = Camera.main.gameObject.GetComponent<MoveToPlayerPoint>();
        Debug.Log(playerPoint);
		if (isPlayer1)
        {
			_playerOne = GameObject.Find("Player1");
			playerPoint.MoveObjectToWinningPoint(_playerOne);
		}
        else
		{
			_playerTwo = GameObject.Find("Player2");
			playerPoint.MoveObjectToWinningPoint(_playerTwo);
		}
        if (gameOverScreen != null) {
            Debug.Log("no nulo screen");
    		gameOverScreen.initScreen(isPlayer1);
        }
        else
         Debug.Log("SI nulo screen");
	}

    public void SetCastorMovement(bool status)
    {
        
        if (_playerOne == null)
        {
			_playerOne = GameObject.Find("Player1");
		}

        if (_playerOne != null)
        {
			_playerOne.GetComponent<MovementComponent>().enabled = status;
			_playerOne.GetComponent<PlayerInputComponent>().enabled = status;
		}

        if(_playerTwo == null)
        {
            _playerTwo = GameObject.Find("Player2");
			
		}
       

        if(_playerTwo != null){
			_playerTwo.GetComponent<MovementComponent>().enabled = status;
			_playerTwo.GetComponent<PlayerInputComponent>().enabled = status;
		}

        if(_spawner == null)
        {
			_spawner = GameObject.Find("Spawner");
		
		}

        if(_spawner != null)
        {
			_spawner.GetComponent<Spawner>().enabled = status;
		}
    }

	private void Update()
	{
        if(SceneManager.GetActiveScene().name == "initialMenu")
        {

			if (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame)
			{
				QuitGame();
			}
			else if ((Keyboard.current.anyKey.wasPressedThisFrame) || (Gamepad.current != null && AnyGamepadButtonPressed()))
            {
				InitGame();
			}  
        }
        else if(SceneManager.GetActiveScene().name == "CreditsScene")
        {
			if (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame)
			{
                GoToMenu();
			}
		}
        if (Input.GetKey(KeyCode.T))
        {

            GameOver(false);

		}
	}
    private bool AnyGamepadButtonPressed()
    {
        return Gamepad.current.buttonSouth.wasPressedThisFrame || 
            Gamepad.current.buttonNorth.wasPressedThisFrame || 
            Gamepad.current.buttonWest.wasPressedThisFrame || 
            Gamepad.current.buttonEast.wasPressedThisFrame || 
            Gamepad.current.selectButton.wasPressedThisFrame ||
            Gamepad.current.leftShoulder.wasPressedThisFrame ||
            Gamepad.current.rightShoulder.wasPressedThisFrame ||
            Gamepad.current.leftTrigger.wasPressedThisFrame ||
            Gamepad.current.rightTrigger.wasPressedThisFrame;
	}
	// M�todo para reiniciar el juego cargando la escena "Game"
	private void InitGame()
    {
        ft_AudioManager.Instance.PlayMusic(ft_AudioManager.ft_AudioType.Music, 1.0f);
        SceneManager.LoadScene("GameFinal");  
    }

    public void GoToMenu()
    {
		SceneManager.LoadScene("initialMenu"); 
	}
}
