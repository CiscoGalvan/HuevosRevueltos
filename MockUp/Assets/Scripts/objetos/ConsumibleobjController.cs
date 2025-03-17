using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumibleobjController : MonoBehaviour
{
    [SerializeField] private bool needTime = true;
    [SerializeField] private float effectDuration = 2.5f; // Duración del efecto en segundos

    private powerup targetComponent;
    // Bandera para evitar múltiples activaciones
    private bool effectApplied = false;

    private void OnCollisionEnter(Collision other)
    {
        // Si ya se aplicó el efecto, no se hace nada.
        if (effectApplied)
            return;

        targetComponent = this.GetComponent<powerup>();
        if (other.gameObject.GetComponent<MovementComponent>() == null)
            return;

        effectApplied = true; // Marcamos que el efecto ya fue activado

        if (needTime)
            StartCoroutine(ApplyEffect(other.gameObject));
        else
            Apply(other.gameObject);
    }

    private IEnumerator ApplyEffect(GameObject g)
    {
        var spawner = this.GetComponent<InitializeEmergentObject3D>();
        spawner?.SetEffectActive(true); // Activar bandera

        targetComponent?.Modifyobj(g);
        yield return new WaitForSeconds(effectDuration);

        targetComponent?.Resetobj();
        spawner?.SetEffectActive(false); // Desactivar bandera
        Destroy(gameObject);
    }

    private void Apply(GameObject g)
    {
        targetComponent?.Modifyobj(g);
        Destroy(gameObject);
    }
    private void OnDestroy()
{
    Debug.Log("El objeto fue destruido.");
}

}
