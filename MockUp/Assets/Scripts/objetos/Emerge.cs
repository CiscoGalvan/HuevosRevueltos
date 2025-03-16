using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emerge : MonoBehaviour
{
    void Emerger()
    {
        if (GetComponentInParent<InitializeEmergentObject2D>() != null)
        {
            GetComponentInParent<InitializeEmergentObject2D>().ActivateObject();
        }
        else if(GetComponentInParent<InitializeEmergentObject3D>() != null)
        {
            GetComponentInParent<InitializeEmergentObject3D>().ActivateObject();
        }
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
