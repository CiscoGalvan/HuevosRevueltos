using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class powerup : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void Modifyobj(GameObject g);

    public abstract void Resetobj();
}
