using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    private GameOverScreen  gameOverScreen;
    [SerializeField] 
    private Life life1;
    [SerializeField] 
    private Life life2;
    private bool gameEnded = false;
    void Update()
    {
        if(!gameEnded) {
            if(life1.GetifDead()) {
                GameOver(true);
            }
            else if(life2.GetifDead()) {
                GameOver(false);
            }
        }
    }
    public void GameOver(bool isPlayer1) {
        gameEnded = true;
        gameOverScreen.initScreen(isPlayer1);
    }
}
