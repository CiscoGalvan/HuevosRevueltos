using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ft_Spin : MonoBehaviour
{
    public GameObject player; // Asigna desde el Inspector el transform del objeto a rotar
    public float time = 1f;  // Duración de la rotación

    // Método para iniciar la rotación y el movimiento de la cámara usando Lerp
    public void Spin(Transform cameraTransform)
    {
        // Calcula la rotación deseada para que el jugador mire hacia la cámara
        Quaternion desiredRotation = Quaternion.LookRotation(cameraTransform.position - player.transform.position);
        Debug.Log("Desired rotation: " + desiredRotation.eulerAngles);
        StartCoroutine(SpinCoroutine(player, desiredRotation, time));
    }

    private IEnumerator SpinCoroutine(GameObject player, Quaternion desiredRotation, float time)
    {
        Quaternion startRotation = player.transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            // Interpolación suave de la rotación
            player.transform.rotation = Quaternion.Slerp(startRotation, desiredRotation, elapsedTime / time);
            // Actualiza el tiempo transcurrido
            elapsedTime += Time.deltaTime;
            // ********* CAMBIO *********
            // Se agrega Lerp para mover la cámara suavemente hacia la posición del jugador.
            // Se usa el valor de elapsedTime/time para interpolar.
            // Se define un offset para la cámara (ajusta según necesites).
            Vector3 offset = new Vector3(0, 2, -4);
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, player.transform.position + offset, elapsedTime / time);
            // *****************************
            yield return null;
        }

        player.transform.rotation = desiredRotation;
    }

    // (Opcional) Puedes mantener este método si quieres separar el movimiento de la cámara.
    public IEnumerator MoveCameraToPlayer(Transform playerTransform)
    {
        float duration = 1.5f;
        float elapsed = 0f;
        Vector3 startPos = Camera.main.transform.position;
        Vector3 offset = new Vector3(0, 2, -4);
        Vector3 endPos = playerTransform.position + offset;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            Camera.main.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }
        Camera.main.transform.position = endPos;
    }
}