using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private GameOverScreen  gameOverScreen;
    private static GameManager _instance;


    private GameObject _playerOne;
    private GameObject _playerTwo;
    private GameObject _spawner;
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
    
    public void QuitGame()
    {
        Application.Quit();
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
