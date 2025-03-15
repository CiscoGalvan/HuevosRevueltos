using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class KeyValuePairGameObjectInt
{
    public GameObject objeto = null;
    public float minTime = 0;
    public float maxTime = 0;
    public float remainingTimeZone1 = 0;
    public float remainingTimeZone2 = 0;
}

public class EmergentObjectsManager : MonoBehaviour
{
    [SerializeField]
    private BoxCollider ColliderZonaJugador1;
    [SerializeField]
    private BoxCollider ColliderZonaJugador2;
    [SerializeField]
    private float CooldownTimer = 10;
    [SerializeField]
    private float remainingTime;
    [SerializeField]
    private List<KeyValuePairGameObjectInt> ListaObjetos;

    private bool bFlagFirstObjects;

    // Start is called before the first frame update
    void Start()
    {
        remainingTime = CooldownTimer;
        bFlagFirstObjects = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else
        {
            if (bFlagFirstObjects)
            {
                foreach (KeyValuePairGameObjectInt lista in ListaObjetos)
                {
                    lista.remainingTimeZone1 = Random.Range(lista.minTime, lista.maxTime);
                    lista.remainingTimeZone2 = Random.Range(lista.minTime, lista.maxTime);
                }
                bFlagFirstObjects = false;
            }
            else
            {
                SetObjectTimersZone1();
                SetObjectTimersZone2();
            }
        }
    }

    // Generate an object in player 1 zone
    void GenerateZone1(GameObject objeto)
    {
        Instantiate(objeto, GetRandomPointInZone1(), Quaternion.identity);
    }

    // Generate an object in player 2 zone
    void GenerateZone2(GameObject objeto)
    {
        Instantiate(objeto, GetRandomPointInZone2(), Quaternion.identity);
    }

    // Returns a random point in the zone of player 1
    Vector3 GetRandomPointInZone1()
    {
        Vector3 returnValue = Vector3.zero;
        if(ColliderZonaJugador1 == null)
        {
            Debug.LogWarning("Random area player 1 not set");
        }
        else
        {
            returnValue = new Vector3(
                Random.Range(ColliderZonaJugador1.bounds.min.x, ColliderZonaJugador1.bounds.max.x),
                0,
                Random.Range(ColliderZonaJugador1.bounds.min.z, ColliderZonaJugador1.bounds.max.z));
        }
        return returnValue;
    }
    // Returns a random point in the zone of player 2
    Vector3 GetRandomPointInZone2()
    {
        Vector3 returnValue = Vector3.zero;
        if (ColliderZonaJugador2 == null)
        {
            Debug.LogWarning("Random area player 2 not set");
        }
        else
        {
            returnValue = new Vector3(
                Random.Range(ColliderZonaJugador2.bounds.min.x, ColliderZonaJugador2.bounds.max.x),
                0,
                Random.Range(ColliderZonaJugador2.bounds.min.z, ColliderZonaJugador2.bounds.max.z));
        }
        return returnValue;
    }

    void SetObjectTimersZone1()
    {
        foreach (KeyValuePairGameObjectInt lista in ListaObjetos)
        {
            if (lista.remainingTimeZone1 <= 0)
            {
                lista.remainingTimeZone1 = Random.Range(lista.minTime, lista.maxTime);
                GenerateZone1(lista.objeto);
            }
            else
            {
                lista.remainingTimeZone1 -= Time.deltaTime;
            }
        }
    }

    void SetObjectTimersZone2()
    {
        foreach (KeyValuePairGameObjectInt lista in ListaObjetos)
        {
            if (lista.remainingTimeZone2 <= 0)
            {
                lista.remainingTimeZone2 = Random.Range(lista.minTime, lista.maxTime);
                GenerateZone2(lista.objeto);
            }
            else
            {
                lista.remainingTimeZone2 -= Time.deltaTime;
            }
        }
    }
}