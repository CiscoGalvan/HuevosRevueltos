using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cazo : powerup
{
   
    [SerializeField]

    // Start is called before the first frame update
    public override void Modifyobj(GameObject g)
    {
        Transform cacerola = g.transform.Find("cacerola");
        if (cacerola != null)
        {
            cacerola.gameObject.SetActive(true);
            Debug.Log("Cacerola activada.");
        }
        else
        {
            Debug.LogError("No se encontró el objeto 'cacerola' dentro de Player2.");
        }

    }

    public override void Resetobj()
    {
       
    }
}
