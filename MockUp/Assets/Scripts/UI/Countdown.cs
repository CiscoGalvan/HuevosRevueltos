using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    private float time;
    [SerializeField]
    private GameObject Three;

    [SerializeField]
    private GameObject Two;

    [SerializeField]
    private GameObject One;


    [SerializeField]
    private GameObject Fight;

    private Canvas parentCanvas;
    // Start is called before the first frame update
    void Awake()
    {
        parentCanvas = GetComponentInParent<Canvas>();
    }
    private void Start()
    {
        time = 0;
        GameManager.Instance.SetCastorMovement(false);
    }

    private void SetThree()
    {
        Three.SetActive(true);
        Two.SetActive(false);
        One.SetActive(false);
        Fight.SetActive(false);
    }
    private void SetTwo()
    {
        Three.SetActive(false);
        Two.SetActive(true);
        One.SetActive(false);
        Fight.SetActive(false);
    }
    private void SetOne()
    {
        Three.SetActive(false);
        Two.SetActive(false);
        One.SetActive(true);
        Fight.SetActive(false);
    }

    private void SetFight()
    {
        Three.SetActive(false);
        Two.SetActive(false);
        One.SetActive(false);
        Fight.SetActive(true);
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time < 1) SetThree();
        else if (time < 2) SetTwo();
        else if (time < 3) SetOne();
        else if (time < 4) SetFight();
        else if (time > 4)
        {
            GameManager.Instance.SetCastorMovement(true);

            parentCanvas.enabled = false;
            this.enabled = false;

        }
          
    }
}
