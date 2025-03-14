using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumibleobj : MonoBehaviour
{
    [SerializeField] private float effectDuration = 5f; // Tiempo que dura el efecto
    private obj targetComponent;

    private void OnCollisionEnter(Collision other)
    {
        targetComponent = other.gameObject.GetComponent<obj>();
        if (targetComponent == null) return;

        StartCoroutine(ApplyEffect(other.gameObject));
    }

    private IEnumerator ApplyEffect(GameObject g)
    {
        targetComponent?.Modifyobj(g);
        yield return new WaitForSeconds(effectDuration);
        targetComponent?.Resetobj();
        Destroy(gameObject); // Destruir el consumible después de su uso
    }
}

