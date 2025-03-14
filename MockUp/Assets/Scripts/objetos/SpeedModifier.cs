using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModifier : obj
{
    // Start is called before the first frame update
    [SerializeField] private bool increase = true; // Factor de multiplicaci�n o divisi�n
    [SerializeField] private float speedMultiplier = 1.5f; // Factor de multiplicaci�n o divisi�n
    private MovementComponent playerMovement;
    private float originalSpeed;

    private void Start()
    {
       
    }

    public override void Modifyobj(GameObject g)
    {
        playerMovement = g.gameObject.GetComponent<MovementComponent>();

        if (playerMovement != null)
        {
            originalSpeed = playerMovement.getSpeed();
        }
        else
        {
            Debug.LogError("SpeedModifier: No se encontr� el script PlayerMovement en el objeto.");
        }
        

        if (increase)
        {
            playerMovement.setSpeed(originalSpeed * speedMultiplier) ;
        }
        else
        {
            playerMovement.setSpeed(originalSpeed / speedMultiplier);

        }
    }

    public override void Resetobj()
    {
        if (playerMovement != null)
        {
            playerMovement.setSpeed(originalSpeed);
        }
    }
}
