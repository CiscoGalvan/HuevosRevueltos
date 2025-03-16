using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModifier : powerup
{
	// Start is called before the first frame update
	[Tooltip("Booleano que indica si la velocidad incrementa o disminuye")]
	[SerializeField] private bool increase = true;
	[Tooltip("Factor por el que se multiplicará en caso de aumentar y dividirá la velocidad en caso de disminuir")]
	[SerializeField] private float speedFactor = 1.5f; 
    private MovementComponent playerMovement;
    private float originalSpeed;
    private float originalmaxSpeed;

   
    public override void Modifyobj(GameObject g)
    {
        playerMovement = g.gameObject.GetComponent<MovementComponent>();

        if (playerMovement != null)
        {
            originalSpeed = playerMovement.getSpeed();
            originalmaxSpeed = playerMovement.getmaxSpeed();
        }
        else
        {
            Debug.LogError("SpeedModifier: No se encontró el script PlayerMovement en el objeto.");
        }
        

        if (increase)
        {
            playerMovement.setSpeed(originalSpeed * speedFactor) ;
            playerMovement.setmaxSpeed(originalmaxSpeed * speedFactor) ;
        }
        else
        {
            playerMovement.setSpeed(originalSpeed / speedFactor);
            playerMovement.setmaxSpeed(originalmaxSpeed / speedFactor);

        }
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<Collider>().enabled = false;
    }

    public override void Resetobj()
    {
        if (playerMovement != null)
        {
            playerMovement.setSpeed(originalSpeed);
            playerMovement.setmaxSpeed(originalmaxSpeed);
        }
    }
}
