using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumibleobjController : MonoBehaviour
{
    [SerializeField] private bool needTime = true;
    [SerializeField] private float effectDuration = 5f; // Tiempo que dura el efecto
    private powerup targetComponent;


    private void OnCollisionEnter(Collision other)
    {
        targetComponent =this.GetComponent<powerup>();
        if (other.gameObject.GetComponent<MovementComponent>() == null) return;
        if(needTime)
            StartCoroutine(ApplyEffect(other.gameObject));
        else
        {
            Apply(other.gameObject);
        }
    }

    private IEnumerator ApplyEffect(GameObject g)
    {
        targetComponent?.Modifyobj(g);
        yield return new WaitForSeconds(effectDuration);
        targetComponent?.Resetobj();
        Destroy(gameObject); // Destruir el consumible después de su uso
    }
    private void Apply(GameObject g)
    {
        targetComponent?.Modifyobj(g);
        Destroy(gameObject);


    }
}

