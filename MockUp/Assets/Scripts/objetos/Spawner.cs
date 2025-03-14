using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // creación de enemigos
    [SerializeField]
    List<GameObject> _prefabsobjects;
    [SerializeField]
    float amountoftime = 1.0f;
    float currenttime;
    
    //direccion de los enemigos
    [SerializeField]
    private Transform pointA;

    [SerializeField]
    private Transform pointB;

    [SerializeField]
    private float minForce = 1.0f;

    [SerializeField]
    private float maxForce = 5.0f;
    // Update is called once per frame
    void Update()
    {
        currenttime += Time.deltaTime;
        if (currenttime >= amountoftime)
        {
            currenttime -= amountoftime;
            //random de la lista 
            if (_prefabsobjects.Count > 0)
            {
                int randomIndex = Random.Range(0, _prefabsobjects.Count); //objeto random
                GameObject newObj = Instantiate(_prefabsobjects[randomIndex], transform.position, Quaternion.identity); //creación

                Rigidbody rb = newObj.GetComponent<Rigidbody>();
                if (rb != null)
                {

                    // Dirección aleatoria entre pointA y pointB
                    Vector3 direction = (Vector3.Lerp(pointA.position, pointB.position, Random.value) - transform.position).normalized;

                    // Fuerza aleatoria dentro del rango
                    float randomForce = Random.Range(minForce, maxForce);

                    rb.AddForce(direction * randomForce, ForceMode.Impulse);
                }
            }
        }
    }
}

