using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEmitter : MonoBehaviour
{
    [SerializeField]
    private int amountofDamage = 10;

    enum collisionobj
    {
        Presa=0,
        Palo=1
    }
    [SerializeField]
    collisionobj elementToCollide = collisionobj.Presa;
    
    private void OnCollisionEnter(Collision collision)
    {
        Life _life = collision.gameObject.GetComponent<Life>();
        if (_life != null && collision.gameObject.layer == LayerMask.NameToLayer(elementToCollide.ToString()))
        {
           
            _life.Damage(amountofDamage);
        }
    }
}
