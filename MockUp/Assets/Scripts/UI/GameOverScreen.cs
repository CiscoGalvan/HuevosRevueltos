using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI text;
    private Canvas parentCanvas;

    void Awake()
    {
        parentCanvas = GetComponentInParent<Canvas>();
    }

    public void initScreen(bool isPlayer1)
    {
        if(isPlayer1)
            text.text = "Jugador 1 gana !!!";
        else
            text.text = "Jugador 2 gana !!!";
        if (parentCanvas != null)
        {
            parentCanvas.gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void restartButton()
    {
        SceneManager.LoadScene("CreditsScene");
    }
}
