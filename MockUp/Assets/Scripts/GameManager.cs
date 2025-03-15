using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private GameOverScreen  gameOverScreen;
   
    private Life life1;
    
    private Life life2;
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
    // Método para reiniciar el juego cargando la escena "Game"
    public void inittGame()
    {
        SceneManager.LoadScene("Game");
       
    }
}
