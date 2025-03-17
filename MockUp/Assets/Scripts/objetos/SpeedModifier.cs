using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModifier : powerup
{
    [Tooltip("Indica si se incrementa (true) o se disminuye (false) la velocidad")]
    [SerializeField] private bool increase = true;
    [Tooltip("Factor para multiplicar (o dividir) la velocidad")]
    [SerializeField] private float speedFactor = 1.5f; 

    private MovementComponent playerMovement;
    private float originalSpeed;
    private float originalMaxSpeed;

    [SerializeField] private Color castorFrio;
    [SerializeField] private Color castorOrejasFrioPlayer1;
    [SerializeField] private Color castorOrejasFrioPlayer2;
    
    [SerializeField] private Color castorNormal;
    [SerializeField] private Color castorOrejasNormalPlayer1;
    [SerializeField] private Color castorOrejasNormalPlayer2;
    
    [SerializeField] private Color castorCalor;
    [SerializeField] private Color castorOrejasCalorPlayer1;
    [SerializeField] private Color castorOrejasCalorPlayer2;

    public override void Modifyobj(GameObject g)
    {
        playerMovement = g.GetComponent<MovementComponent>();
        if (playerMovement == null)
        {
            Debug.LogError("SpeedModifier: No se encontró MovementComponent en el objeto.");
            return;
        }

        // Evitar que se active otro efecto mientras uno ya está en curso
        if (playerMovement.isSpeedModified)
        {
            Debug.Log("Ya hay un efecto de velocidad activo.");
            return;
        }
        playerMovement.isSpeedModified = true;

        originalSpeed = playerMovement.getSpeed();
        originalMaxSpeed = playerMovement.getmaxSpeed();

        if (!increase)
        {
            if (g.name == "Player1")
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
            else if (g.name == "Player2")
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

        if (increase)
        {
            ft_AudioManager.Instance.PlaySFX(ft_AudioManager.ft_AudioType.Berry, volume: 1.0f);
            playerMovement.setSpeed(originalSpeed * speedFactor, increase);
            playerMovement.setmaxSpeed(originalMaxSpeed * speedFactor);
        }
        else
        {
            ft_AudioManager.Instance.PlaySFX(ft_AudioManager.ft_AudioType.Freeze, volume: 1.0f);
            playerMovement.setSpeed(originalSpeed / speedFactor, increase);
            playerMovement.setmaxSpeed(originalMaxSpeed / speedFactor);
        }

        List<MeshRenderer> meshes = this.GetComponent<InitializeEmergentObject3D>().MeshRenderers;
        foreach (MeshRenderer mesh in meshes)
        {
            mesh.enabled = false;
        }
    }

    public override void Resetobj()
    {
        if (playerMovement != null)
        {
            playerMovement.ResetSpeed();

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
}
