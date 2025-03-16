using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModifier : powerup
{
	// Start is called before the first frame update
	[Tooltip("Booleano que indica si la velocidad incrementa o disminuye")]
	[SerializeField] private bool increase = true;
	[Tooltip("Factor por el que se multiplicar� en caso de aumentar y dividir� la velocidad en caso de disminuir")]
	[SerializeField] private float speedFactor = 1.5f; 
    private MovementComponent playerMovement;
    private float originalSpeed;
    private float originalmaxSpeed;
    [SerializeField] private Color castorFrio;
    [SerializeField] private Color castorOrejasFrioPlayer1;
    [SerializeField] private Color castorOrejasFrioPlayer2;
    
    [SerializeField] private Color castorNormal;
    [SerializeField] private Color castorOrejasNormalPlayer1;
    [SerializeField] private Color castorOrejasNormalPlayer2;
    
    [SerializeField] private Color castorCalor;
    [SerializeField] private Color castorOrejasCalorPlayer1;
    [SerializeField] private Color castorOrejasCalorPlayer2;

    private Material materialCastor;
    private Material materialCastorOrejas;
    public override void Modifyobj(GameObject g)
    {
        playerMovement = g.gameObject.GetComponent<MovementComponent>();

        if (playerMovement != null)
        {
            // TIENE FRIO
            if (!increase)
            {
                if (g.gameObject.name == "Player1")
                {
                    foreach (Material mat in g.transform.GetChild(0).GetComponent<Renderer>().materials)
                    {
                        if (mat.name.Contains("marron"))
                        {
                            mat.color = castorFrio;
                        }

                        if (mat.name.Contains("marronOscuro"))
                        {
                            mat.color = castorOrejasFrioPlayer1;
                        }
                    }
                }
                else if (g.gameObject.name == "Player2")
                {
                    foreach (Material mat in g.transform.GetChild(0).GetComponent<Renderer>().materials)
                    {
                        if (mat.name.Contains("marron"))
                        {
                            mat.color = castorFrio;
                        }

                        if (mat.name.Contains("marronclaro"))
                        {
                            mat.color = castorOrejasFrioPlayer2;
                        }
                    }
                }
            }
            originalSpeed = playerMovement.getSpeed();
            originalmaxSpeed = playerMovement.getmaxSpeed();
        }
        else
        {
            Debug.LogError("SpeedModifier: No se encontr� el script PlayerMovement en el objeto.");
        }
        

        if (increase)
        {
            ft_AudioManager.Instance.PlaySFX(ft_AudioManager.ft_AudioType.Berry, volume: 1.0f);
            playerMovement.setSpeed(originalSpeed * speedFactor, increase) ;
            playerMovement.setmaxSpeed(originalmaxSpeed * speedFactor) ;
        }
        else
        {
            ft_AudioManager.Instance.PlaySFX(ft_AudioManager.ft_AudioType.Freeze, volume: 1.0f);
            playerMovement.setSpeed(originalSpeed / speedFactor, increase);
            playerMovement.setmaxSpeed(originalmaxSpeed / speedFactor);

        }

        List<MeshRenderer> meshes = this.gameObject.GetComponent<InitializeEmergentObject3D>().MeshRenderers;
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.enabled = false;
        }
    }

    public override void Resetobj()
    {
        if (playerMovement != null)
        {
            playerMovement.setSpeed(originalSpeed, increase);
            playerMovement.setmaxSpeed(originalmaxSpeed);
        }
        if (!increase)
        {
            if (playerMovement.gameObject.name == "Player1")
            {
                foreach (Material mat in playerMovement.transform.GetChild(0).GetComponent<Renderer>().materials)
                {
                    if (mat.name.Contains("marron"))
                    {
                        mat.color = castorNormal;
                    }

                    if (mat.name.Contains("marronOscuro"))
                    {
                        mat.color = castorOrejasNormalPlayer1;
                    }
                }
            }
            else if (playerMovement.gameObject.name == "Player2")
            {
                foreach (Material mat in playerMovement.transform.GetChild(0).GetComponent<Renderer>().materials)
                {
                    if (mat.name.Contains("marron"))
                    {
                        mat.color = castorNormal;
                    }

                    if (mat.name.Contains("marronclaro"))
                    {
                        mat.color = castorOrejasNormalPlayer2;
                    }
                }
            }
        }
    }
}
